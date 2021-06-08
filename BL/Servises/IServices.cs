using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.BL.Servises
{
    public interface IServices<T>
    {

        public Task<IEnumerable<T>> GetAllAsync();      // Get All
        public Task<T> GetByIdAsync(int id);            // Get By Id
        public Task<T> UpdateAsync(int id,T NewObject); // Update
        public void DeleteAsync(int id);                // Delete
        public Task<T> CreateAsync(T OblectToCreate);   //Post

    }
}
