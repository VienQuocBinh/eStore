using BusinessObject;

namespace DataAccess
{
    public class BaseDAO
    {
        public static string ConnectionString { get; set; }

        protected EStoreContext context = new EStoreContext(connectionString: ConnectionString);

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
