using AppPassword.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppPassword.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppPassword.Services
{
    internal class ApiService
    {

        private readonly string BaseUrl = "http://10.0.2.2"; // Changez cela par l'URL de votre API

        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            using (var client = GetHttpClient())
            {
                var response = await client.GetStringAsync($"{BaseUrl}/getAllusers");
                return JsonConvert.DeserializeObject<List<Contact>>(response);
            }
        }

        public async Task<List<PasswordEntry>> GetAllPasswordEntries()
        {
            using (var client = GetHttpClient())
            {
                var response = await client.GetStringAsync($"{BaseUrl}/getAllpassword_entries");
                return JsonConvert.DeserializeObject<List<PasswordEntry>>(response);
            }
        }


    }
}
