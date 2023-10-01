using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppPassword.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace AppPassword.Services
{
    public class Contact_DAO
    {
        private readonly HttpClient _httpClient;
        private const string API = "http://cyrianb.alwaysdata.net";

       
        public Contact_DAO()
        {
            _httpClient = new HttpClient();

        }

        public async Task<Contact> GetUserByEmail(string email)
        {
            var users = await GetAllUser();
            return users.FirstOrDefault(u => u.email == email);
        }

        // recuperer tout les users
        public async Task<List<Contact>> GetAllUser()
        {
            var response = await _httpClient.GetAsync($"{API}/getAllusers");
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Contact>>(jsonString);
        }

        // ajouter un user 
        public async Task AddUser(Contact user)
        {
            var jsonData = JsonSerializer.Serialize(user);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            try
            {
                await _httpClient.PostAsync($"{API}/users", content);
            }
            catch(Exception exception)
            {
                var t = exception.Message;
            }
            
        }


        // Editer un users 
        public async Task EditUser(Contact user)
        {
            var jsonData = JsonSerializer.Serialize(user);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{API}/Edit_Users/{user.Id}", content);
        }


        // Supprimer un users 
        public async Task DeleteUser(int userId)
        {
            await _httpClient.DeleteAsync($"{API}/del_users/{userId}");
        }

    }
}
