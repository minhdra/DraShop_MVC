using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class ImportBillDetail
    {
        public string import_bill_detail_id { get; set; }
        public string import_bill_id { get; set; }
        public string product_id { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }

        public ImportBillDetail() { }

        public ImportBillDetail(string _id,
            string import_bill_id, string product_id,
            int quantity, double price)
        {
            this.import_bill_detail_id = _id;
            this.import_bill_id = import_bill_id;
            this.product_id = product_id;
            this.quantity = quantity;
            this.price = price;
        }
    }
}
