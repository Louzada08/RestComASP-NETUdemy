using RestComASPNETUdemy.Model;
using RestComASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestComASPNETUdemy.Services.Implementations
{
  public class PersonServiceImpl : IPersonService
  {
    private MySQLContext mySQLContext;

    public PersonServiceImpl(MySQLContext context) {
      mySQLContext = context;

      if (mySQLContext.Persons.Count() == 0) {
        mySQLContext.Persons.AddRange(
            new Person {
              FirstName = "Maria Vilma",
              LastName = "Louzada",
              Address = "110 Norte, Al. 25, Lote 2",
              Gender = "Feminine"
            },
            new Person {
              FirstName = "ANITA VAITEROSKI",
              LastName = "DE LIMA",
              Address = "Rua Armando Salles de Oliveira",
              Gender = "Feminine"
            },
            new Person {
              FirstName = "GUILHERME HENRIQUE",
              LastName = "BARBOSA DE MATOS",
              Address = "Avenida Maranhão",
              Gender = "Male"
            },
            new Person {
              FirstName = "JOSLEI ELISEU",
              LastName = "LIEBL",
              Address = "Rua Adolfo Konder",
              Gender = "Male"
            },
            new Person {
              FirstName = "LUCAS ALEF SOUZA",
              LastName = "DA SILVA",
              Address = "Avenida Curuá-Una",
              Gender = "Male"
            });
        mySQLContext.SaveChanges();
      }
    }

    public Person Create(Person person) {
      try {
        mySQLContext.Add(person);
        mySQLContext.SaveChanges();
      }
      catch (Exception ex) {
        throw ex;
      }
      return person;
    }

    public void Delete(long id) {
      var result = mySQLContext.Persons.SingleOrDefault(p => p.Id.Equals(id));
      try {
        if (result != null) mySQLContext.Persons.Remove(result);
        mySQLContext.SaveChanges();
      }
      catch (Exception ex) {
        throw ex;
      }
    }

    public List<Person> FindAll() {

      return mySQLContext.Persons.ToList();
    }

    public Person FindById(long? id) {

      return mySQLContext.Persons.SingleOrDefault(p => p.Id.Equals(id));
    }

    public Person Update(Person person) {

      var result = mySQLContext.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
      try {
        mySQLContext.Entry(result).CurrentValues.SetValues(person);
        mySQLContext.SaveChanges();
      }
      catch (Exception ex) {
        throw ex;
      }
      return person;
    }

    public bool Exist(long? id) {
      return mySQLContext.Persons.Any(p => p.Id.Equals(id));
    }
  }
}
