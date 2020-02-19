using RestComASPNETUdemy.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestComASPNETUdemy.Data.VO
{
  public class PersonVO
  {
    public long? Id { get; set; }
    [Column("FirstName")]
    public string FirstName { get; set; }
    [Column("LastName")]
    public string LastName { get; set; }
    [Column("Address")]
    public string Address { get; set; }
    [Column("Gender")]
    public string Gender { get; set; }
  }
}
