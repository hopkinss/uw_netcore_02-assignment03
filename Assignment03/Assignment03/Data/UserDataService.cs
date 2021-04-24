using Assignment03.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment03.DataProvider
{
    public class UserDataService : IUserDataService
    {
        public IEnumerable<User> GetAllUsers()
        {
            yield return new User("Dave", "1DavePwd");
            yield return new User("Steve", "2StevePwd");
            yield return new User("Lisa", "3LisaPwd");
        }
    }
}
