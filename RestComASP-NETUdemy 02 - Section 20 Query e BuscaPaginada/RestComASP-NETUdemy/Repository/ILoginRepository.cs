using RestComASPNETUdemy.Model;

namespace RestComASPNETUdemy.Repository
{
  public interface ILoginRepository
  {
    User FindByLogin(string login);
  }
}
