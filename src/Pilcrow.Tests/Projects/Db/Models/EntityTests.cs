using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilcrow.Db.Models;

namespace Pilcrow.Tests.Projects.Db.Models
{
    public class Widget : Entity, IAutoMappable
    {
        public string Name { get; set; }
    }
    
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void EqualityTest()
        {
            var a = new Widget();
            var b = new Widget();
            Assert.AreEqual(a, b);
            
            a.Id = "5952aa977d58012948373900";
            b.Id = "5952aa977d58012948373900";
            Assert.AreEqual(a, b);
            
            b.Id = "5952aa977d58012948373901";
            Assert.AreNotEqual(a, b);
            
            b.Id = "";
            Assert.AreNotEqual(a, b);
        }
    }
}
