using BusinessObject;

namespace DataAccess.Repositories
{
    public interface ICartRepo
    {
        public List<CartItem> GetCartItems();
        public CartItem GetCartItem(int productID);
        public bool AddItem(int productID);
        public bool RemoveItem(int productID);
        public bool EditItem(int productID, int quantity);
        public bool Pay();
    }
}
