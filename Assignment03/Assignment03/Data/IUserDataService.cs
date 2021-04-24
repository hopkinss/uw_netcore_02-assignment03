using Assignment03.Models;
using System.Collections.Generic;

namespace Assignment03.DataProvider
{
    public interface IUserDataService
    {
        IEnumerable<User> GetAllUsers();
    }
}