using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestComASPNETUdemy.Model;
using RestComASPNETUdemy.Services;

namespace RestComASP_NETUdemy.Controllers
{
  [ApiVersion("1")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class PersonsController : Controller
  {
    private IPersonService ipersonService;

    public PersonsController(IPersonService personService) {

      ipersonService = personService;
  }

    // GET api/values
    [HttpGet]
    public IActionResult Get() {
      return Ok(ipersonService.FindAll());
    }

    // GET api/values/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(long id) {
      var person = ipersonService.FindById(id);
      if (person == null) return NotFound();
      return Ok(person);
    }

    // POST api/values
    [HttpPost]
    public IActionResult Post([FromBody] Person person) {
      if (person == null) return BadRequest();
      return new ObjectResult(ipersonService.Create(person));
    }

    // PUT api/values/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put([FromBody] Person person) {
      if (person == null)
        return BadRequest();
      if(!ipersonService.Exist(person.Id))
        return NotFound();

      return new ObjectResult(ipersonService.Update(person));
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public IActionResult Delete(long id) {
      ipersonService.Delete(id);
      return NoContent();
    }

  }
}
