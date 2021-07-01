﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.BL.Servises
{
    public interface IAdminStatisticsService
    {
        public Task<int> GetCustomersNumber();
        public Task<int> GetViewedProductsNumber();
        public Task<int> GetSavedProductsNumber();
        public Task<double> GetrateOfSViewedProducts();
        public Task<int> GetSellersNumber();
        public Task<int> GetSolidItemsNumber();
        public Task<int> GetProductsNumber();
        public Task<int> GetNumberOfSolidProductsAsync();
        public Task<int> GetNumberOfOrdersInStatusAsync(int statusNumber);
    }
}
