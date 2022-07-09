using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class ImportBill
    {
        public string import_bill_id { get; set; }
        public string supplier_id { get; set; }
        public DateTime date_create { get; set; }
        public double total { get; set; }

        public ImportBill () { }

        public ImportBill (string import_bill_id,
            string supplier_id, DateTime date_create,
            double total)
        {
            this.import_bill_id = import_bill_id;
            this.supplier_id = supplier_id;
            this.date_create = date_create;
            this.total = total;
        }
    }
}
