using J6.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.BL.Servises
{
    public interface IUserSavedBagServices
    {
        public Task SetItemToSavedBagAsync(int UserId, int ProductId);
        public Task<ICollection<Product>> GetSavedProductsAsync(int UserId);
        public Task<bool> DeleteSavedItemAsync(int UserId, int ProductId);
        public Task<bool> IsSaved(int UserId, int ProductId);

    }
}
