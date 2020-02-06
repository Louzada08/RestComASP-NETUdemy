using RestComASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RestComASPNETUdemy.Services.Implementations
{
  public class PersonServiceImpl : IPersonService
  {
    private int count;

    public Person Create(Person person) {
      return person;
    }

    public void Delete(long id) {
      throw new NotImplementedException();
    }

    public List<Person> FindAll() {
      List<Person> persons = new List<Person>();
      for (int i = 0; i < 8; i++) {
        Person person = MockPerson(i);
        persons.Add(person);
      }
      return persons;
    }

    private Person MockPerson(int i) {
      return new Person {
        Id = IncrementAndGet(),
        FirstName = "Pessoa Name "+i,
        LastName = "Lastname " + i,
        Address = "Palmas " + i,
        Gender = "Male " + i
      };
    }

    private long IncrementAndGet() {
      return Interlocked.Increment(ref count);
    }

    public Person FindById(long id) {
      return new Person {
        Id = 1,
        FirstName = "Anderson Luiz",
        LastName = "Louzada",
        Address = "110 Norte, Al.23, Lt.50",
        Gender = "Male"
      };
    }

    public Person Update(Person person) {
        return person;
    }
  }
}
