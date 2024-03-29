﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public interface IProductColorDAO
    {
        List<ProductColor> GetProductColorsByProduct(string product_id);
        List<ProductColor> GetProductColors();
        void AddProductColor(ProductColor color);
        void UpdateProductColor(ProductColor color);
        void DeleteProductColor(string _id);
    }
}
