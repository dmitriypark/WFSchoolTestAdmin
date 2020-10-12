using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFSchoolTestAdmin.Models
{
    public class ApplicationViewModel
    {
        public IEnumerable<User> Users { get; set; }


        public async Task GetUsers(string login, string password)
        {
            UserService userService = new UserService();
            Users = await userService.Get(login, password);

        }


        public async Task AddUser (string login, string password,User user)
        {
            UserService userService = new UserService();
            await userService.Add(login, password, user);
        }


        public async Task<Boolean> DeleteUser(string login, string password, int id)
        {
            bool success;
            UserService userService = new UserService();
            success = await userService.Delete(login, password, id);
            return success;
        }
    }
}
