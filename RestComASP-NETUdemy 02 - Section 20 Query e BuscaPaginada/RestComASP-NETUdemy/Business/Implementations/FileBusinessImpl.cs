using RestComASPNETUdemy.Model;
using System.IO;

namespace RestComASPNETUdemy.Business.Implementations
{
  public class FileBusinessImpl : IFileBusiness
  {
    public byte[] GetPDFFile() {
      string path = Directory.GetCurrentDirectory();
      var fulPath = path + "\\Other\\NFSE-Modelo-Conceitual versao 2-02.pdf";
      return File.ReadAllBytes(fulPath);
    }
  }
}
