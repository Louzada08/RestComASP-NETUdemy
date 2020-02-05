using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace RestComASP_NETUdemy.Controllers
{
  [Route("api/[controller]")]
  public class CalculatorController : Controller
  {
    // GET api/values
    [HttpGet]
    public IEnumerable<string> Get() {
      return new string[] { "value1", "value2" };
    }

    // GET api/values/5/5
    [HttpGet("{firstNumber}/{secondNumber}")]
    public IActionResult Sum(string firstNumber, string secondNumber) {

      if (IsNumeric(firstNumber) && IsNumeric(secondNumber)) {

        var sum = CovertToDecimal(firstNumber) + CovertToDecimal(secondNumber);
        return Ok(sum.ToString());
      }
      return BadRequest("Invalid Input");
    }

    private decimal CovertToDecimal(string number) {
      decimal decimalValue;
      if(decimal.TryParse(number, out decimalValue)) {

        return decimalValue;
      }
      return 0;
    }

    private bool IsNumeric(string number) {
      
      decimal numberValue;
      bool isNumber = decimal.TryParse(number, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out numberValue);
      return isNumber;
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody]string value) {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value) {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id) {
    }
  }
}
