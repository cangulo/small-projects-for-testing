using System.Threading.Tasks;

namespace cangulo.DbMigrationsHandler
{
    public interface IDbUpdatesHandler
    {
        Task UpdateDb();
    }
}