﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopBUS
{
    public interface IManageAdminBUS
    {
        Admin GetAdmin(string username, string password);
        List<Admin> GetAdmins();
    }
}
