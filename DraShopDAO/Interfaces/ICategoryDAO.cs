﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public partial interface ICategoryDAO
    {
        List<Category> GetCategories();
    }
}
