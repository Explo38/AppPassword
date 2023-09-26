using AppPassword.Models;
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
    internal class ServiceWebAPI
    {
        private readonly string BaseUrl = "http://http://localhost:8080/"; // Changez cela par l'URL de votre API

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

        // Vous pouvez ajouter d'autres méthodes pour POST, PUT, DELETE ici
    }
}

 