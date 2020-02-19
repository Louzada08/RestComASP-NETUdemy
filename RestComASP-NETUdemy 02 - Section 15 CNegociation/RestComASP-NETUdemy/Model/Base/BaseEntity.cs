using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace RestComASPNETUdemy.Model.Base
{
  public class BaseEntity
  {
    public long? Id { get; set; }
  }
}
