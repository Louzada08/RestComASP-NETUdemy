using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestComASPNETUdemy.Business;
using RestComASPNETUdemy.Model;
using Swashbuckle.Swagger.Annotations;

namespace RestComASP_NETUdemy.Controllers
{
  [ApiVersion("1")]
  [Route("api/[controller]/v{version:apiVersion}")]
  public class FileController : Controller
  {
    private IFileBusiness ifileBusiness;

    public FileController(IFileBusiness fileBusiness) {

      ifileBusiness = fileBusiness;
  }

    // POST api/values
    [HttpGet]
    [SwaggerResponse((200), Type = typeof(byte[]))]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [Authorize("Bearer")]
    public IActionResult GetPDFFile() {

      byte[] buffer = ifileBusiness.GetPDFFile();
      if(buffer != null) {

        HttpContext.Response.ContentType = "application/pdf";
        HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
        HttpContext.Response.Body.Write(buffer, 0, buffer.Length);
      }
      return new ContentResult();
    }

  }
}
