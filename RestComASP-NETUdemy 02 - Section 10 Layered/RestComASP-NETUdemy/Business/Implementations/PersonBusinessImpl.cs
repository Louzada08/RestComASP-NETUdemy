using RestComASPNETUdemy.Model;
using RestComASPNETUdemy.Repository.Generic;
using System.Collections.Generic;

namespace RestComASPNETUdemy.Business.Implementations
{
  public class PersonBusinessImpl : IPersonBusiness
  {
    private IRepository<Person> iRepository;

    public PersonBusinessImpl(IRepository<Person> repository) {
      
      iRepository = repository;
    }

    public Person Create(Person person) {
      //Se tiver regras incluir aqui
      return iRepository.Create(person);
    }

    public void Delete(long id) {

      iRepository.Delete(id);
    }

    public List<Person> FindAll() {

      return iRepository.FindAll();
    }

    public Person FindById(long? id) {

      return iRepository.FindById(id);
    }

    public Person Update(Person person) {
      if (!iRepository.Exist(person.Id))
        return null;
      return iRepository.Update(person);
    }

  }
}
