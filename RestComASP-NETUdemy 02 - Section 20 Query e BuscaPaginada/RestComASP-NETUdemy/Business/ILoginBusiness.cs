using RestComASPNETUdemy.Model;

namespace RestComASPNETUdemy.Business
{
  public interface ILoginBusiness
  {
    object FindByLogin(User user);
  }
}
