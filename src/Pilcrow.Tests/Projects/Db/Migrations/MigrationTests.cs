using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Pilcrow.Db;
using Pilcrow.Db.Migrations;
using Pilcrow.Db.Models;
using Pilcrow.Db.Repositories;
using Pilcrow.Tests.Core;

namespace Pilcrow.Tests.Projects.Db.Migrations
{
    public class Widget : Entity, IAutoMappable
    {
        public string Name { get; set; }
    }
    
    public interface IWidgetRepository : IRepository<Widget>
    {
    }
    
    public class WidgetRepository : Repository<Widget>, IWidgetRepository
    {
        public WidgetRepository(IContext context) : base(context)
        {}
    }
    
    public class _20170731_01_InitialDataMigration : Migration
    {
        public override void Execute()
        {
            GetCollection("gadget").InsertOne(new BsonDocument
            {
                { "name", "Transmogrifier" }
            });
        }
    }
    
    public class _20170731_02_RenameGadgetMigration : Migration
    {
        public override void Execute()
        {
            Context.Database.RenameCollection(
                "gadget",
                Repository.CollectionName(typeof(Widget))
            );
        }
    }
    
    public class _20170731_03_CapitalizeWidgetNameFieldMigration : Migration
    {
        public override void Execute()
        {
            WalkCollection(
                GetCollection<Widget>(),
                widget =>
                {
                    widget["Name"] = widget["name"];
                    widget.Remove("name");
                }
            );
        }
    }
    
    [TestClass]
    public class MigrationTests : IntegrationTest
    {
        private IWidgetRepository _widgetRepository;
        
        protected override void InitializeTestMethod()
        {
            _widgetRepository = new WidgetRepository(DbContext);
        }
        
        protected override void CleanupTestMethod()
        {
            _widgetRepository = null;
        }
        
        [TestMethod]
        public void ExecuteTest()
        {
            var widget = _widgetRepository.FindOne(x => x.Name == "Transmogrifier").Object;
            Assert.IsNotNull(widget);
        }
    }
}
