using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;
using DraShopDAO;

namespace DraShopBUS
{
    public class ClientBUS: IClientBUS
    {
        ICustomerDAO customerDAO = new CustomerDAO();
        ICartDAO cartDAO = new CartDAO();
        ICartDetailDAO cartDetailDAO = new CartDetailDAO();
        IDeliveryAddressDAO deliveryAddressDAO = new DeliveryAddressDAO();

        // Register customer
        public bool Register(Customer customer)
        {
            if (CheckUniqueUsername(customer.username)) return false;
            string id = GenerateCustomerId();
            customer._id = id;
            customer.status = 0;
            customer.information = null;
            customerDAO.Register(customer);
            return true;
        }
        // Login customer
        public Customer Login(string username, string password)
        {
            return customerDAO.Login(username, password);
        }
        // Get all customer
        public List<Customer> GetCustomers()
        {
            return customerDAO.GetCustomers();
        }
        // Get cart by customer id
        public Cart GetCart(string customer_id)
        {
            return cartDAO.GetCart(customer_id);
        }
        // Get all cart
        public List<Cart> GetCarts()
        {
            return cartDAO.GetCarts();
        }
        // Get list cart detail
        public List<CartDetail> GetCartDetails(string cart_id)
        {
            return cartDetailDAO.GetCartDetails(cart_id);
        }
        // Create cart
        public CartDetail CreateCart(Cart cart, CartDetail cartDetail)
        {
            if(!CheckCart(cart.customer_id))
            {
                string _id = GenerateCartId();
                cart._id = _id;
                cartDetail.cart_id = _id;
                cart.ListCartDetail = null;
                cartDAO.CreateCart(cart);
                CreateCartDetail(cartDetail);
            }
            else
            {
                Cart c = GetCart(cart.customer_id);
                CartDetail cd = CheckCartDetail(cartDetail.product_id, c._id);
                cartDetail.cart_id = c._id;
                if(cd == null)
                {
                    CreateCartDetail(cartDetail);
                }
                else
                {
                    cartDetail._id = cd._id;
                    UpdateCartDetail(cartDetail);
                }
            }

            return cartDetail;
        }
        // Create cart detail
        public void CreateCartDetail(CartDetail cartDetail)
        {
            if (cartDetail == null) return;
            cartDetail._id = GenerateCartDetailId();
            cartDetailDAO.CreateCartDetail(cartDetail);
        }
        // Generate customer id
        private string GenerateCustomerId()
        {
            Random ran = new Random();
            int number = ran.Next(1, 100);
            string id = "" + number;
            List<Customer> list = GetCustomers();
            if (list == null) return id;
            for(int i = 0; i < list.Count; i++)
            {
                if (list[0]._id.Trim() == id)
                {
                    number = ran.Next(1, 100);
                    id = "" + number;
                    i = -1;
                }
            }
            return id;
        }
        // Update Cart Detail
        public void UpdateCartDetail(CartDetail cartDetail)
        {
            cartDetailDAO.UpdateCartDetail(cartDetail);
        }
        // Delete Cart Detail
        public void DeleteCartDetail(string _id)
        {
            cartDetailDAO.DeleteCartDetail(_id);
        }
        
        /** Delivery address */
        public List<DeliveryAddress> GetDeliveryAddresses(string customer_id)
        {
            return deliveryAddressDAO.GetDeliveryAddresses(customer_id);
        }

        // Create delivery address
        public DeliveryAddress CreateDeliveryAddress(DeliveryAddress address)
        {
            address._id = GenerateAddressId();
            deliveryAddressDAO.CreateDeliveryAddress(address);
            return address;
        }

        public void UpdateDeliveryAddress(DeliveryAddress address)
        {
            deliveryAddressDAO.UpdateDeliveryAddress(address);
        }

        public void DeleteDeliveryAddress(string _id)
        {
            deliveryAddressDAO.DeleteDeliveryAddress(_id);
        }

        // Generate delivery id
        private string GenerateAddressId()
        {
            Random ran = new Random();
            List<DeliveryAddress> deliveryAddresses = deliveryAddressDAO.GetDeliveryAddresses("");
            string id = "" + ran.Next(0, 1000);
            for(int i = 0; i < deliveryAddresses.Count; i++)
            {
                if(id == deliveryAddresses[i]._id.Trim())
                {
                    id = "" + ran.Next(0, 1000);
                    i = -1;
                }
            }

            return id;
        }

        // Generate cart id
        private string GenerateCartId()
        {
            Random ran = new Random();
            int number = ran.Next(1, 100);
            string id = "" + number;
            List<Cart> list = GetCarts();
            if (list == null) return id;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[0]._id.Trim() == id)
                {
                    number = ran.Next(1, 100);
                    id = "" + number;
                    i = -1;
                }
            }
            return id;
        }
        // Generate cart detail id
        private string GenerateCartDetailId()
        {
            Random ran = new Random();
            int number = ran.Next(1, 100);
            string id = number + "";
            List<CartDetail> list = GetCartDetails("");
            if (list == null) return id;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[0]._id.Trim() == id)
                {
                    number = ran.Next(1, 100);
                    id = "" + number;
                    i = -1;
                }
            }
            return id;
        }

        private bool CheckUniqueUsername(string username)
        {
            List<Customer> list = GetCustomers();
            if (list == null) return false;
            foreach(var item in list)
            {
                if (item.username.Trim() == username) return true;
            }
            return false;
        }

        private bool CheckCart(string customer_id)
        {
            Cart cart = GetCart(customer_id);
            if (cart == null) return false;
            return true;
        }
        private CartDetail CheckCartDetail(string product_id, string cart_id)
        {
            List<CartDetail> list = GetCartDetails(cart_id);
            foreach(var item in list)
            {
                if (item.product_id == product_id) return item;
            }
            return null;
        }
    }
}
