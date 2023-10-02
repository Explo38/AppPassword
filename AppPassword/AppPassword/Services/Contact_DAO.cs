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
        private const string API = "http://10.0.2.2:8080"; // L'URL de base de l'API.

        public Contact_DAO()
        {
            // Initialisation du client HTTP.
            _httpClient = new HttpClient();
        }

        // Obtient un utilisateur par son email.
        public async Task<Contact> GetUserByEmail(string email)
        {
            var users = await GetAllUser();
            return users.FirstOrDefault(u => u.email == email);
        }

        // Récupère tous les utilisateurs.
        public async Task<List<Contact>> GetAllUser()
        {
            var response = await _httpClient.GetAsync($"{API}/getAllusers"); // Effectue une requête GET pour obtenir tous les utilisateurs.
            var jsonString = await response.Content.ReadAsStringAsync(); // Lit la réponse en tant que chaîne de caractères.
            return JsonSerializer.Deserialize<List<Contact>>(jsonString); // Désérialise la chaîne en une liste d'utilisateurs.
        }

        // Ajoute un nouvel utilisateur.
        public async Task AddUser(Contact user)
        {
            var jsonData = JsonSerializer.Serialize(user); // Sérialise l'objet utilisateur en chaîne JSON.
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // Crée un contenu HTTP à partir de la chaîne JSON.
            try
            {
                await _httpClient.PostAsync($"{API}/users", content); // Effectue une requête POST pour ajouter un utilisateur.
            }
            catch (Exception exception) // Attrape toute exception qui pourrait survenir.
            {
                var t = exception.Message; // Stocke le message d'erreur (à utiliser pour le débogage ou la journalisation).
            }
        }

        // Met à jour un utilisateur existant.
        public async Task EditUser(Contact user)
        {
            var jsonData = JsonSerializer.Serialize(user); // Sérialise l'objet utilisateur en chaîne JSON.
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // Crée un contenu HTTP à partir de la chaîne JSON.
            await _httpClient.PutAsync($"{API}/Edit_Users/{user.Id}", content); // Effectue une requête PUT pour mettre à jour un utilisateur.
        }

        // Supprime un utilisateur par son ID.
        public async Task DeleteUser(int userId)
        {
            await _httpClient.DeleteAsync($"{API}/del_users/{userId}"); // Effectue une requête DELETE pour supprimer un utilisateur.
        }
    }
}

