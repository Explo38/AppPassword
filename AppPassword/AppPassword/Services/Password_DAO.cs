using System;
using System.Collections.Generic;
using System.Text;
using AppPassword.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppPassword.Services
{
    class Password_DAO
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "http://cyrianb.alwaysdata.net";

        public Password_DAO()
        {
            _httpClient = new HttpClient();
        }


        // Récupérer toutes les entrées de mots de passe
        public async Task<List<PasswordEntry>> GetAllPasswordEntries()
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}/getAllpassword_entries");
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<PasswordEntry>>(jsonString);
        }

        // Ajouter une entrée de mot de passe
        public async Task AddPasswordEntry(PasswordEntry passwordEntry)
        {
            var jsonData = JsonSerializer.Serialize(passwordEntry);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{ApiBaseUrl}/passwords", content);
        }

        // Modifier une entrée de mot de passe
        public async Task EditPasswordEntry(PasswordEntry passwordEntry)
        {
            var jsonData = JsonSerializer.Serialize(passwordEntry);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{ApiBaseUrl}/Edit_passwords/{passwordEntry.Id}", content);
        }

        // Supprimer une entrée de mot de passe par ID
        public async Task DeletePasswordEntry(int passwordEntryId)
        {
            await _httpClient.DeleteAsync($"{ApiBaseUrl}/del_password/{passwordEntryId}");
        }

    }
}
