
using Dac.Net.Core;
using Dac.Net.Db;
using Dac.Net.Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace Dac.Net.Test.Db
{
    public class PgSqlTest
    {
        private readonly ITestOutputHelper _output;
        public PgSqlTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ConnectTest()
        {
            var server = Utility.LoadServers("TestData/servers.yml")["pgsql"];
            var sql = new PgSql(server);
            var res = sql.Connect();
            Assert.True(res);
        }
        
        [Fact]
        public void ExtractTest()
        {
            var server = Utility.LoadServers("TestData/servers.yml")["pgsql"];
            var sql = new PgSql(server);
            var res = sql.Connect();
            var db = sql.Extract();
            var yaml = Utility.DataBaseToYaml(db);
            _output.WriteLine(yaml);
            Assert.NotEmpty(db.Tables);
        }
        
        
        [Fact]
        public void CreateTest()
        {
            var server = Utility.LoadServers("TestData/servers.yml")["pgsql"];
            var db = Utility.LoadDataBase("TestData/pgsql.yml");

            var sql = new PgSql(server);
            var res = sql.Connect();
            var query = sql.Create(db, false);
            _output.WriteLine(query);
            Assert.False(string.IsNullOrWhiteSpace(query));
        }
        
        [Fact]
        public void ReCreateTest()
        {
            var server = Utility.LoadServers("TestData/servers.yml")["pgsql"];
            var db = Utility.LoadDataBase("TestData/pgsql.yml");

            var sql = new PgSql(server);
            var res = sql.Connect();
            var query = sql.ReCreate(db, false);
            _output.WriteLine(query);
            Assert.False(string.IsNullOrWhiteSpace(query));
        }
        
        [Fact]
        public void DiffTest()
        {
            var server = Utility.LoadServers("TestData/servers.yml")["pgsql"];
            var db = Utility.LoadDataBase("TestData/pgsql.yml");
            Utility.TrimDataBaseProperties(db);
                
            var sql = new PgSql(server);
            var res = sql.Connect();
            var diff = sql.Diff(db);
            Assert.False(diff.HasDiff);
        }
        
        [Fact]
        public void UpdateTest()
        {
            var server = Utility.LoadServers("TestData/servers.yml")["pgsql"];
            var db = Utility.LoadDataBase("TestData/pgsql.yml");
            Utility.TrimDataBaseProperties(db);
            
            var sql = new PgSql(server);
            var res = sql.Connect();
            var query = sql.Update(db, false, false);
            _output.WriteLine(query ?? "");
            Assert.False(string.IsNullOrWhiteSpace(query));
        }
        
        [Fact]
        public void DropTest()
        {
            var server = Utility.LoadServers("TestData/servers.yml")["pgsql"];
            var db = Utility.LoadDataBase("TestData/pgsql.yml");
            Utility.TrimDataBaseProperties(db);
            
            var sql = new PgSql(server);
            var res = sql.Connect();
            var query = sql.Drop(db, true);
            _output.WriteLine(query);
            Assert.False(string.IsNullOrWhiteSpace(query));
        }
        
        [Fact]
        public void QueryTest()
        {
            var server = Utility.LoadServers("TestData/servers.yml")["pgsql"];
            var db = Utility.LoadDataBase("TestData/pgsql.yml");
            
            var sql = new PgSql(server);
            var res = sql.Connect();
            var query = sql.Query(db);
            _output.WriteLine(query);
            Assert.False(string.IsNullOrWhiteSpace(query));
        }
    }
}