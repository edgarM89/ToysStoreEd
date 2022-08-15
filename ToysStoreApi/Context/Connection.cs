using Microsoft.EntityFrameworkCore;

namespace ToysStoreApi.Contexts
{
    public class Connection:DbContext
    {
        
        public Connection(DbContextOptions<Connection> options) : base(options)
        { }
    }
}
