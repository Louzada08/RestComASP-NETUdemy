using RestComASPNETUdemy.Data.Converters;
using RestComASPNETUdemy.Data.VO;
using RestComASPNETUdemy.Model;
using RestComASPNETUdemy.Repository.Generic;
using System.Collections.Generic;

namespace RestComASPNETUdemy.Business.Implementations
{
  public class PersonBusinessImpl : IPersonBusiness
  {
    private IRepository<Person> iRepository;
    private readonly PersonConverter converter;

    public PersonBusinessImpl(IRepository<Person> repository) {
      
      iRepository = repository;
      converter = new PersonConverter();
    }

    public PersonVO Create(PersonVO person) {
      var personEntity = converter.Parse(person);
      personEntity = iRepository.Create(personEntity);
      return converter.Parse(personEntity);
    }

    public void Delete(long id) {

      iRepository.Delete(id);
    }

    public List<PersonVO> FindAll() {

      return converter.ParseList(iRepository.FindAll());
    }

    public PersonVO FindById(long? id) {

      return converter.Parse(iRepository.FindById(id));
    }

    public PersonVO Update(PersonVO person) {
      if (!iRepository.Exists(person.Id))
        return null;
      var personEntity = converter.Parse(person);
      personEntity = iRepository.Update(personEntity);
      return converter.Parse(personEntity);
    }

  }
}
