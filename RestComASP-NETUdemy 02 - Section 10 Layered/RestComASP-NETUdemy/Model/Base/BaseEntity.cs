using System.Runtime.Serialization;

namespace RestComASPNETUdemy.Model.Base
{
  [DataContract]
  public class BaseEntity
  {
    public long Id { get; set; }
  }
}
