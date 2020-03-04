using RestComASPNETUdemy.Model;
using RestComASPNETUdemy.Model.Context;
using RestComASPNETUdemy.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestComASPNETUdemy.Repository.Implementations
{
  public class PersonRepositoryImpl : GenericRepository<Person>, IPersonRepository
  {
    public PersonRepositoryImpl(MySQLContext context) : base(context) { }

    public List<Person> FindByName(string firstName, string lastName) {

      if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName)) {

        return _context.Persons.Where(p => p.FirstName.ToUpper().Contains(firstName.ToUpper()) && p.LastName.ToUpper().Contains(lastName.ToUpper())).ToList();
      }
      else if (string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName)) {
        return _context.Persons.Where(p => p.LastName.ToUpper().Contains(lastName.ToUpper())).ToList();
      }
      else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName)) {
        return _context.Persons.Where(p => p.FirstName.ToUpper().Contains(firstName.ToUpper())).ToList();
      }
      else {
        return _context.Persons.ToList();
      }

    }
  }
}
