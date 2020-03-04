using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestComASPNETUdemy.Business;
using RestComASPNETUdemy.Data.VO;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;

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
    [SwaggerResponse((200), Type = typeof(PersonVO))]
    [Authorize("Bearer")]
    public IActionResult Get() {
      return Ok(ipersonBusiness.FindAll());
    }

    // GET api/values
    [HttpGet("find-by-name")]
    [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
    [Authorize("Bearer")]
    public IActionResult GetByName([FromQuery] string firstName, [FromQuery] string lastName) {
      return new OkObjectResult(ipersonBusiness.FindByName(firstName, lastName));
    }

    // GET api/person/v1
    [HttpGet("find-with-paged-serch/{sortDirection}/{pageSize}/{page}")]
    [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [Authorize("Bearer")]
    public IActionResult GetPageSearch([FromQuery] string name, string sortDirection, int pageSize, int page) {
      return new OkObjectResult(ipersonBusiness.FindWithPagedSearch(name, sortDirection, pageSize, page));
    }

    // GET api/values/5
    [HttpGet("{id}")]
    [SwaggerResponse((200), Type = typeof(PersonVO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize("Bearer")]
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
    [SwaggerResponse((202), Type = typeof(PersonVO))]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize("Bearer")]
    public IActionResult Put([FromBody] PersonVO person) {
      
      if (person == null) return BadRequest();
      var updatePerson = ipersonBusiness.Update(person);
      if (updatePerson == null) return NoContent();
      return new ObjectResult(ipersonBusiness.Update(updatePerson));
    }

    // PUT api/values/5
    [HttpPatch]
    [SwaggerResponse((202), Type = typeof(PersonVO))]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize("Bearer")]
    public IActionResult Patch([FromBody] PersonVO person) {

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
