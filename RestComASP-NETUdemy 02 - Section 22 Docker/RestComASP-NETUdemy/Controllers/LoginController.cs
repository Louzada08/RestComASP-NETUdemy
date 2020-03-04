using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestComASPNETUdemy.Business;
using RestComASPNETUdemy.Model;

namespace RestComASP_NETUdemy.Controllers
{
  [ApiVersion("1")]
  [Route("api/[controller]/v{version:apiVersion}")]
  public class LoginController : Controller
  {
    private ILoginBusiness iloginBusiness;

    public LoginController(ILoginBusiness loginBusiness) {

      iloginBusiness = loginBusiness;
  }

    // POST api/values
    [AllowAnonymous]
    [HttpPost]
    public object Post([FromBody] User user) {
      if (user == null) return BadRequest();
      return iloginBusiness.FindByLogin(user);
    }

  }
}
