using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Data.SqlClient;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace TaskAPI.Models
{
    public class FluentHibernateHelper
    {
        public static string cnnSt = @" Encrypt=False";
        public static NHibernate.ISession TaskSession()
        {
            string connectionString = cnnSt;
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).Driver<MicrosoftDataSqlClientDriver>().ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Task>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false))
                .BuildSessionFactory();

            return sessionFactory.OpenSession();

        }
    }
}
