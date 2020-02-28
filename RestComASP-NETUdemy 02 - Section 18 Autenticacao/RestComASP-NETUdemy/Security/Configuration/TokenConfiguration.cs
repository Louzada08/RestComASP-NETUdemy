using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace RestComASPNETUdemy.Security.Configuration
{
  public class TokenConfiguration
  {
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int Seconds { get; set; }
  }
}
