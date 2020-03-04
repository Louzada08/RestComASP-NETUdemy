using RestComASPNETUdemy.Model.Base;
using System.Collections.Generic;

namespace RestComASPNETUdemy.Repository.Generic
{
  public interface IRepository<T> where T : BaseEntity
  {
    T Create(T item);
    T FindById(long? id);
    List<T> FindWithPageSearch(string query);
    List<T> FindAll();
    T Update(T item);
    void Delete(long id);
    bool Exists(long? id);
    int GetCount(string query);
  }
}
