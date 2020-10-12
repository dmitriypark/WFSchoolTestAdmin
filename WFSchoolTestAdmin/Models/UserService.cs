using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WFSchoolTestAdmin.Models
{
    public class UserService
    {

        const string Url = "http://pakdmitriy1989-001-site1.htempurl.com/api/admin";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }


        public async Task<IEnumerable<User>> Get(string login, string password)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/" + login + "/" + password);
            return JsonConvert.DeserializeObject<IEnumerable<User>>(result);
        }


        


        

        
        public async Task<User> Add(string login, string password, User user)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url + "/" + login + "/" + password,
                new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());
        }

        // удаляем друга
        public async Task<Boolean> Delete(string login, string password, int id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + "/" + login + "/" + password + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
