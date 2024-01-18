using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces.LoginInterfaceInfrastructure;

namespace NovelNest.Infrastructure.Repositories.LoginRepositories
{
    public class LoginRepository : ILoginInterface
    {
        private readonly NovelNestDataContext _context;

        public LoginRepository(NovelNestDataContext context)
        {
            _context = context;
        }


        void ILoginInterface.LoginSuccesful(string username, string password)
        {
            //try
            //{
            //    var user = _context.UserEntities.FirstOrDefault(u => u.UserName == username);

            //    if (user is null)
            //    {
            //        return;
            //    }

            //    if (user is not null)
            //    {
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Fehler in LoginSuccesful Klasse " + ex.Message);
            //}
        }
    }
}
