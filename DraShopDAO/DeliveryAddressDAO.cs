using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public class DeliveryAddressDAO: IDeliveryAddressDAO
    {
        DataHelper dh = new DataHelper();

        public DeliveryAddress GetDeliveryAddress(string _id)
        {
            string sqlQuery = 
                "select * from dra_delivery_address where _id ='" + _id + "'";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return ToList(dt).Count > 0 ? ToList(dt)[0] : null;
        }

        public List<DeliveryAddress> GetDeliveryAddresses(string customer_id)
        {
            string sqlQuery = customer_id == "" ? "select * from dra_delivery_address" :
               "select * from dra_delivery_address where customer_id ='" + customer_id + "'";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return ToList(dt);
        }

        public void CreateDeliveryAddress(DeliveryAddress address)
        {
            dh.StoreReader("CreateDeliveryAddress", address._id, address.customer_id,
                address.customer_name, address.phone_number, address.province, address.district,
                address.commune, address.specific_address, address.type_address, address.status);
        }

        public void UpdateDeliveryAddress(DeliveryAddress address)
        {
            dh.StoreReader("UpdateDeliveryAddress", address._id, address.customer_id,
                address.customer_name, address.phone_number, address.province, address.district,
                address.commune, address.specific_address, address.type_address, address.status);
        }

        public void DeleteDeliveryAddress(string _id)
        {
            string sqlQuery = "delete from dra_delivery_address where _id='" + _id + "'";
            dh.ExcuteNonQuery(sqlQuery);
        }

        private List<DeliveryAddress> ToList(DataTable dt)
        {
            List<DeliveryAddress> list = new List<DeliveryAddress>();
            foreach (DataRow row in dt.Rows)
            {
                int status = row[9].ToString() == "True" ? 1 : 0;
                DeliveryAddress item = new DeliveryAddress(
                    row[0].ToString(), row[1].ToString(),
                    row[2].ToString(), row[3].ToString(),
                    row[4].ToString(), row[5].ToString(),
                    row[6].ToString(), row[7].ToString(),
                    row[8].ToString(), status);
                list.Add(item);
            }
            return list;
        }
    }
}
