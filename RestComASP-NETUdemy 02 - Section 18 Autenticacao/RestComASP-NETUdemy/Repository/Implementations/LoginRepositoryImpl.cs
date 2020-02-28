using RestComASPNETUdemy.Model;
using RestComASPNETUdemy.Model.Context;
using System;
using System.Linq;

namespace RestComASPNETUdemy.Repository.Implementations
{
  public class LoginRepositoryImpl : ILoginRepository
  {
    private MySQLContext mySQLContext;

    public LoginRepositoryImpl(MySQLContext context) {

      mySQLContext = context;
    }

    public User FindByLogin(string login) {

      return mySQLContext.Users.SingleOrDefault(u => u.Login.Equals(login));
    }

  }
}
