using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Models
{
    public class CartItem
    {
        public int _quantity { get; set; }
        public Sản_phẩm _product { get; set; }
    }

    public class Cart
    {
        private readonly List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public bool AddProductCart(Sản_phẩm product, int quantity = 1)
        {
            var item = Items.FirstOrDefault(s => s._product.ID_sản_phẩm == product.ID_sản_phẩm);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _quantity = quantity,
                    _product = product
                });
                return true;
            }

            var tempQ = quantity + item._quantity;
            if (tempQ > product.số_lượng_tồn)
            {
                item._quantity = (int)product.số_lượng_tồn;
                return false;
            }
            item._quantity = tempQ;
            return true;
        }

        public int TotalQuantity()
        {
            return items.Sum(s => s._quantity);
        }

        public decimal TotalMoney()
        {
            var total = items.Sum(s => s._quantity * s._product.Giá_tiền);
            return (decimal)total;
        }

        public void UpdateQuantity(int id, int new_quantity)
        {
            var item = Items.FirstOrDefault(s => s._product.ID_sản_phẩm == id);
            if (item != null)
                if (new_quantity == 0)
                {
                    RemoveCartItem(id);
                    return;
                }
            item._quantity = new_quantity;
        }

        public void RemoveCartItem(int id)
        {
            items.RemoveAll(s => s._product.ID_sản_phẩm == id);
        }

        public void ClearCart()
        {
            items.Clear();
        }
    }
}