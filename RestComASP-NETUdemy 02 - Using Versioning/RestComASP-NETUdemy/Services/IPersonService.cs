using RestComASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestComASPNETUdemy.Services
{
  public interface IPersonService
  {
    Person Create(Person person);
    Person FindById(long? id);
    List<Person> FindAll();
    Person Update(Person person);
    void Delete(long id);
    bool Exist(long? id);
  }
}
