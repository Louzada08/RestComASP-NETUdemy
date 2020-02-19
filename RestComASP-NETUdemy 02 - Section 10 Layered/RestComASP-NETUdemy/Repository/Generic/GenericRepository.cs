using Microsoft.EntityFrameworkCore;
using RestComASPNETUdemy.Model.Base;
using RestComASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestComASPNETUdemy.Repository.Generic
{
  public class GenericRepository<T> : IRepository<T> where T : BaseEntity
  {
    private readonly MySQLContext mySQLContext;
    private DbSet<T> dataset;

    public GenericRepository(MySQLContext context) {

      mySQLContext = context;
      dataset = mySQLContext.Set<T>();
    }

    public T Create(T item) {
      try {
        dataset.Add(item);
        mySQLContext.SaveChanges();
      }
      catch (Exception ex) {
        throw ex;
      }
      return item;
    }

    public void Delete(long id) {
      var result = dataset.SingleOrDefault(p => p.Id.Equals(id));
      try {
        if (result != null) dataset.Remove(result);
        mySQLContext.SaveChanges();
      }
      catch (Exception ex) {
        throw ex;
      }
    }

    public bool Exist(long? id) {
      return dataset.Any(p => p.Id.Equals(id));
    }

    public List<T> FindAll() {
      return dataset.ToList();
    }

    public T FindById(long? id) {
      return dataset.SingleOrDefault(p => p.Id.Equals(id));
    }

    public T Update(T item) {
      try {
        dataset.Add(item);
        mySQLContext.SaveChanges();
      }
      catch (Exception ex) {
        throw ex;
      }
      return item;
    }
  }
}
