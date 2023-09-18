using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppPassword.Services
{
    public interface IContactStore<T>
    {
        Task<bool> AddContactAsync(T contact);
        Task<bool> UpdateContactAsync(T contact);
        Task<bool> DeleteContactAsync(string id);
        Task<T> GetContactAsync(string id);

        Task<IEnumerable<T>> GetContactAsync(bool forceRefresh = false); 



    }
}
