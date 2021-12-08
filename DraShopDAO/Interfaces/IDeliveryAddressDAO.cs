using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public interface IDeliveryAddressDAO
    {
        List<DeliveryAddress> GetDeliveryAddresses(string customer_id);
        DeliveryAddress GetDeliveryAddress(string _id);
        void CreateDeliveryAddress(DeliveryAddress address);
        void UpdateDeliveryAddress(DeliveryAddress address);
        void DeleteDeliveryAddress(string _id);

    }
}
