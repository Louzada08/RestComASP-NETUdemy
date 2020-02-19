using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestComASPNETUdemy.Model;
using RestComASPNETUdemy.Business;
using RestComASPNETUdemy.Data.VO;

namespace RestComASP_NETUdemy.Controllers
{
  [ApiVersion("1")]
  [Route("api/[controller]/v{version:apiVersion}")]
  public class PersonsController : Controller
  {
    private IPersonBusiness ipersonBusiness;

    public PersonsController(IPersonBusiness personBusiness) {

      ipersonBusiness = personBusiness;
  }

    // GET api/values
    [HttpGet]
    public IActionResult Get() {
      return Ok(ipersonBusiness.FindAll());
    }

    // GET api/values/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(long id) {
      var person = ipersonBusiness.FindById(id);
      if (person == null) return NotFound();
      return Ok(person);
    }

    // POST api/values
    [HttpPost]
    public IActionResult Post([FromBody] PersonVO person) {
      if (person == null) return BadRequest();
      return new ObjectResult(ipersonBusiness.Create(person));
    }

    // PUT api/values/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put([FromBody] PersonVO person) {
      
      if (person == null) return BadRequest();
      var updatePerson = ipersonBusiness.Update(person);
      if (updatePerson == null) return NoContent();
      return new ObjectResult(ipersonBusiness.Update(updatePerson));
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public IActionResult Delete(long id) {
      ipersonBusiness.Delete(id);
      return NoContent();
    }

  }
}
