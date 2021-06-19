using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.BL.Servises
{
    public interface IAdminStatisticsService
    {
        public Task<int> GetCustomersNumber();
        public int GetViewedProductsNumber();
        public int GetSavedProductsNumber();
        public double GetrateOfSViewedProducts();
        public Task<int> GetSellersNumber();
        public int GetSolidItemsNumber();
        public int GetProductsNumber();
    }
}
