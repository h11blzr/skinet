using Core.Entities;

namespace Core.Interfaces
{
    public interface IBasketRepo
    {
        Task<CustomerBasket> GetBaskedAsync(string basketId);

        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);

        Task<bool> DeleteBasketAsync(string basketId);
    }
}