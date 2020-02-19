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

    // GET api/values/sum/5/5
    [HttpGet("sum/{firstNumber}/{secondNumber}")]
    public IActionResult Sum(string firstNumber, string secondNumber) {

      if (IsNumeric(firstNumber) && IsNumeric(secondNumber)) {

        var sum = CovertToDecimal(firstNumber) + CovertToDecimal(secondNumber);
        return Ok(sum.ToString());
      }
      return BadRequest("Invalid Input");
    }

    // GET api/values/subtraction/5/5
    [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
    public IActionResult Subtraction(string firstNumber, string secondNumber) {

      if (IsNumeric(firstNumber) && IsNumeric(secondNumber)) {

        var sum = CovertToDecimal(firstNumber) - CovertToDecimal(secondNumber);
        return Ok(sum.ToString());
      }
      return BadRequest("Invalid Input");
    }

    // GET api/values/division/5/5
    [HttpGet("division/{firstNumber}/{secondNumber}")]
    public IActionResult Division(string firstNumber, string secondNumber) {

      if (IsNumeric(firstNumber) && IsNumeric(secondNumber)) {

        var divis = CovertToDecimal(firstNumber) / CovertToDecimal(secondNumber);
        return Ok(divis.ToString());
      }
      return BadRequest("Invalid Input");
    }

    // GET api/values/multiplication/5/5
    [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
    public IActionResult Multiplication(string firstNumber, string secondNumber) {

      if (IsNumeric(firstNumber) && IsNumeric(secondNumber)) {

        var mult = CovertToDecimal(firstNumber) * CovertToDecimal(secondNumber);
        return Ok(mult.ToString());
      }
      return BadRequest("Invalid Input");
    }

    // GET api/values/mean/5/5
    [HttpGet("mean/{firstNumber}/{secondNumber}")]
    public IActionResult Mean(string firstNumber, string secondNumber) {

      if (IsNumeric(firstNumber) && IsNumeric(secondNumber)) {

        var mean = (CovertToDecimal(firstNumber) + CovertToDecimal(secondNumber)) / 2;
        return Ok(mean.ToString());
      }
      return BadRequest("Invalid Input");
    }

    // GET api/values/square-root/5
    [HttpGet("square-root/{number}")]
    public IActionResult SquareRoot(string number) {

      if (IsNumeric(number)) {

        var squareRoot = Math.Sqrt((double)CovertToDecimal(number));
        return Ok(squareRoot.ToString());
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
