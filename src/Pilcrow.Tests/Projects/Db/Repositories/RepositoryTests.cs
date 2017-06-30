using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilcrow.Db;
using Pilcrow.Db.Models;
using Pilcrow.Db.Repositories;
using Pilcrow.Mvc;
using Pilcrow.Tests.Core;
using Pilcrow.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pilcrow.Tests.Projects.Db.Repositories
{
    public abstract class Animal : Entity, IAutoMappable
    {
        public string Name { get; set; }
        
        public int Age { get; set; }
    }
    
    public abstract class Vertebrate : Animal
    {
    }
    
    public abstract class Mammal : Vertebrate
    {
        public bool Cuddly { get; set; }
    }
    
    public class Dog : Mammal
    {
        public enum Types
        {
            CompanionDog,
            GuideDog,
            PoliceDog,
            RescueDog,
            ServiceDog
        };
        
        public Types Type { get; set; }
    }
    
    public class Cat : Mammal
    {
    }
    
    public interface IAnimalRepository : IRepository<Animal>
    {
    }
    
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        public AnimalRepository(IContext context) : base(context)
        {}
    }
    
    [TestClass]
    public class RepositoryTests : IntegrationTest
    {
        private IAnimalRepository _animalRepository;
        
        private Faker _faker;
        
        private Random _random;
        
        protected override void InitializeTestMethod()
        {
            _animalRepository = new AnimalRepository(DbContext);
            _faker = new Faker();
            _random = new Random();
        }
        
        protected override void CleanupTestMethod()
        {
            _random = null;
            _faker = null;
            _animalRepository = null;
        }
        
        private Cat MakeCat()
        {
            return new Faker<Cat>()
                .RuleFor(cat => cat.Name, f => f.Name.FirstName())
                .RuleFor(cat => cat.Age, f => f.Random.Int(min: 0, max: 18))
                .RuleFor(cat => cat.Cuddly, f => f.Random.Bool());
        }
        
        private Dog MakeDog()
        {
            return new Faker<Dog>()
                .RuleFor(dog => dog.Name, f => f.Name.FirstName())
                .RuleFor(dog => dog.Age, f => f.Random.Int(min: 0, max: 18))
                .RuleFor(dog => dog.Cuddly, f => f.Random.Bool())
                .RuleFor(dog => dog.Type, f => f.PickRandom<Dog.Types>());
        }
        
        private List<Animal> MakeAnimals(Func<Animal> maker, int min, int max)
        {
            return RepositoryHelper.CreateObjects(min, max, _animalRepository, maker);
        }
        
        [TestMethod]
        public void CreateTest()
        {
            var dog = MakeDog();
            _animalRepository.Create(dog);
            Assert.IsTrue(_animalRepository.ValidateObjectId(dog.Id));
            Assert.IsTrue(dog.ModificationTime > DateTime.MinValue);
        }
        
        [TestMethod]
        public void UpdateTest()
        {
            var dog = MakeDog();
            _animalRepository.Create(dog);
            
            dog.Age = 8;
            dog.Type = Dog.Types.ServiceDog;
            _animalRepository.Update(dog);
            
            var findOneResult = _animalRepository.FindOne(dog.Id);
            Assert.IsTrue(findOneResult.Success);
            Assert.IsNotNull(findOneResult.Object);
            Assert.IsInstanceOfType(findOneResult.Object, typeof(Dog));
            dog = findOneResult.Object as Dog;
            Assert.AreEqual(8, dog.Age);
            Assert.AreEqual(Dog.Types.ServiceDog, dog.Type);
        }
        
        [TestMethod]
        public void DeleteTest()
        {
            var dog = MakeDog();
            _animalRepository.Create(dog);
            _animalRepository.Delete(dog);
            Assert.IsNull(_animalRepository.FindOne(dog.Id).Object);
        }
        
        [TestMethod]
        public void FindOneTest()
        {
            var dogs = MakeAnimals(MakeDog, 8, 32);
            var dog = _faker.PickRandom(dogs);
            
            var nameStart = dog.Name.Substring(0, _random.Next(1, dog.Name.Length));
            var findOneResult = _animalRepository.FindOne(
                x => x.Name.StartsWith(nameStart) && x.Age == dog.Age
            );
            Assert.IsNotNull(findOneResult.Object);
            Assert.IsInstanceOfType(findOneResult.Object, typeof(Dog));
            Assert.AreEqual(dog.Id, findOneResult.Object.Id);
        }
        
        [TestMethod]
        public void FindOnePolymorphicTest()
        {
            var dog = MakeDog();
            _animalRepository.Create(dog);
            
            var findOneResult = _animalRepository.FindOne(
                x => ((Dog)x).Type == dog.Type
            );
            Assert.IsNotNull(findOneResult.Object);
            Assert.IsInstanceOfType(findOneResult.Object, typeof(Dog));
            Assert.AreEqual(dog.Id, findOneResult.Object.Id);
        }
        
        [TestMethod]
        public void FindManyTest()
        {
            var cats = MakeAnimals(MakeCat, 4, 8);
            var dogs = MakeAnimals(MakeDog, 4, 8);
            
            var findManyResult = _animalRepository.FindMany(x => x is Cat);
            Assert.IsTrue(findManyResult.Success);
            Assert.IsTrue(findManyResult.Objects.All(x => x is Cat));
            Assert.AreEqual(cats.Count, findManyResult.Count());
        }
        
        [TestMethod]
        public void FindManySkipTest()
        {
            MakeAnimals(MakeDog, 8, 8);
            var findManyDogsResult = _animalRepository.FindMany(x => true);
            var linqSkip = findManyDogsResult.Objects.Skip(4);
            var dbSkip = findManyDogsResult.Skip(4).Objects;
            Assert.IsTrue(linqSkip.SequenceEqual(dbSkip));
        }
        
        [TestMethod]
        public void FindManyTakeTest()
        {
            MakeAnimals(MakeDog, 8, 8);
            var findManyDogsResult = _animalRepository.FindMany(x => true);
            var linqTake = findManyDogsResult.Objects.Take(4);
            var dbTake = findManyDogsResult.Take(4).Objects;
            Assert.IsTrue(linqTake.SequenceEqual(dbTake));
        }
        
        [TestMethod]
        public void FindManySortByAscendingTest()
        {
            var sortedCats = MakeAnimals(MakeCat, 32, 64).OrderBy(x => x.Age);
            var dbSortedCats = _animalRepository
                .FindMany(x => true)
                .SortByAscending(x => x.Age)
                .Objects;
            
            Assert.IsTrue(sortedCats.SequenceEqual(dbSortedCats));
        }
        
        [TestMethod]
        public void FindManySortByDescendingTest()
        {
            var sortedCats = MakeAnimals(MakeCat, 32, 64).OrderByDescending(x => x.Age);
            var dbSortedCats = _animalRepository
                .FindMany(x => true)
                .SortByDescending(x => x.Age)
                .Objects;
            
            Assert.IsTrue(sortedCats.SequenceEqual(dbSortedCats));
        }
    }
}
