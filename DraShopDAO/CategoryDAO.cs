using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;
using System.Data;

namespace DraShopDAO
{
    public class CategoryDAO : ICategoryDAO
    {
        DataHelper dh = new DataHelper();
        public List<Category> GetCategories()
        {
            DataTable dt = dh.GetDataTable("Select * from dra_category");
            return toList(dt);
        }

        public List<Category> toList(DataTable dt)
        {
            List<Category> list = new List<Category>();
            foreach (DataRow row in dt.Rows)
            {
                Category category = new Category(row[0].ToString(), row[1].ToString(), row[2].ToString());
                list.Add(category);
            }
            return list;
        }
    }
}
