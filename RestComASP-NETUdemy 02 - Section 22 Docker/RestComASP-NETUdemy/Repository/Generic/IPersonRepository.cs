using RestComASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestComASPNETUdemy.Repository.Generic
{
  public interface IPersonRepository : IRepository<Person>
  {
    List<Person> FindByName(string firstName, string lastName);
  }
}
