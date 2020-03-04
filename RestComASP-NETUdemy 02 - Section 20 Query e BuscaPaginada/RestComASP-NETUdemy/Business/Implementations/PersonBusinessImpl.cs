using RestComASPNETUdemy.Data.Converters;
using RestComASPNETUdemy.Data.VO;
using RestComASPNETUdemy.Repository.Generic;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace RestComASPNETUdemy.Business.Implementations
{
  public class PersonBusinessImpl : IPersonBusiness
  {
    private IPersonRepository iRepository;
    private readonly PersonConverter converter;

    public PersonBusinessImpl(IPersonRepository repository) {
      
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

    public PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page) {

      page = page > 0 ? page - 1 : 0;
      string query = @"select * from persons p where 1 = 1 ";
      if (!string.IsNullOrEmpty(name)) {
        query += $"and p.FirstName like '%{name}%'";
      }

      switch (sortDirection.ToLower()) {
        case "asc":
        case "desc":
          break;
        default:
          sortDirection = "asc";
          break;
      }

      query += $" order by p.FirstName {sortDirection} limit {pageSize} offset {page}";

      string countQuery = @"select count(*) from persons p where 1 = 1 ";
      if (!string.IsNullOrEmpty(name)) countQuery = countQuery + $" and p.FirstName like '%{name}%'";


      var persons = converter.ParseList(iRepository.FindWithPageSearch(query));

      int totalResults = iRepository.GetCount(countQuery);

      return new PagedSearchDTO<PersonVO> {

        CurrentPage = page + 1,
        List = persons,
        PageSize = pageSize,
        SortDirections = sortDirection,
        TotalResults = totalResults
      };
    }

    public PersonVO Update(PersonVO person) {
      if (!iRepository.Exists(person.Id))
        return null;
      var personEntity = converter.Parse(person);
      personEntity = iRepository.Update(personEntity);
      return converter.Parse(personEntity);
    }

    public List<PersonVO> FindByName(string firstName, string lastName) {
      return converter.ParseList(iRepository.FindByName(firstName, lastName));
    }
  }
}
