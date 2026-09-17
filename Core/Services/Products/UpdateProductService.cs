using BusinessEntities;
using Common;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(Product product, string name, string description, decimal price, int stockQuantity)
        {
            product.SetName(name);
            product.SetDescription(description);
            product.SetPrice(price);
            product.SetStockQuantity(stockQuantity);
        }
    }
}
