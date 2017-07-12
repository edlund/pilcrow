using System.Globalization;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilcrow.Db;
using Pilcrow.Db.Models;
using Pilcrow.Db.Models.Globalization;
using Pilcrow.Db.Repositories;
using Pilcrow.Tests.Core;

namespace Pilcrow.Tests.Projects.Db.Models.Globalization
{
    public class Hero : Entity, IAutoMappable
    {
        public Translatable<string> Name { get; set; } = new Translatable<string>();
        
        public int Level { get; set; }
    }
    
    public interface IHeroRepository : IRepository<Hero>
    {
    }
    
    public class HeroRepository : Repository<Hero>, IHeroRepository
    {
        public HeroRepository(IContext context) : base(context)
        {}
    }
    
    [TestClass]
    public class TranslatableTests : IntegrationTest
    {
        private IHeroRepository _heroRepository;
        
        protected override void InitializeTestMethod()
        {
            _heroRepository = new HeroRepository(DbContext);
        }
        
        protected override void CleanupTestMethod()
        {
            _heroRepository = null;
        }
        
        [TestMethod]
        public void CurrentCultureTest()
        {
            Hero hero = new Hero();
            
            hero.Name.Value = "Frog";
            
            Translatable.CurrentCulture = new CultureInfo("ja-JP");
            hero.Name.Value = "カエル";
            Assert.AreEqual("カエル", hero.Name.Value);
            Assert.AreEqual("Frog", hero.Name["en-US"]);
            
            Translatable.CurrentCulture = new CultureInfo("en-US");
            hero.Name["sv-SE"] = "Groda";
            Assert.AreEqual("Frog", hero.Name.Value);
            Assert.AreEqual("カエル", hero.Name["ja-JP"]);
            Assert.AreEqual("Groda", hero.Name["sv-SE"]);
        }
        
        [TestMethod]
        public void FallbackCultureTest()
        {
            Hero hero = new Hero();
            
            hero.Name.Value = "Frog";
            
            Translatable.CurrentCulture = new CultureInfo("ja-JP");
            Assert.AreEqual("Frog", hero.Name.Value);
        }
        
        [TestMethod]
        public void LanguageTest()
        {
            Hero hero = new Hero();
            
            hero.Name["en-US"] = "Frog";
            hero.Name["sv-SE"] = "Groda";
            
            Assert.AreEqual("Frog", hero.Name["en"]);
            Assert.AreEqual("Groda", hero.Name["sv"]);
        }
        
        [TestMethod]
        public void CloseCultureTest()
        {
            Hero hero = new Hero();
            
            hero.Name["en-US"] = "Frog";
            
            Assert.AreEqual("Frog", hero.Name["en-GB"]);
        }
        
        [TestMethod]
        public void RepositoryTest()
        {
            Hero hero = new Hero();
            
            hero.Name["en-US"] = "Frog";
            hero.Name["ja-JP"] = "カエル";
            hero.Name["sv-SE"] = "Groda";
            
            _heroRepository.Create(hero);
            Assert.IsTrue(_heroRepository.ValidateObjectId(hero.Id));
            
            hero = _heroRepository.FindOne(x => x.Name["en-US"] == "Frog").Object;
            Assert.IsNotNull(hero);
            
            hero = _heroRepository.FindOne(x => x.Name["sv-SE"] == "Groda").Object;
            Assert.IsNotNull(hero);
        }
    }
}
