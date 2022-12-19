using Core.Entities.OrderAggregate;

namespace Core.Specification
{
    public class OrdersWithFiltersForCountSpecification : BaseSpecification<Order>
    {
        public OrdersWithFiltersForCountSpecification(ProductSpecParams orderSpecParams)
            : base(x =>
                (string.IsNullOrEmpty(orderSpecParams.Search)
                    || x.ShipToAddress.Zipcode.ToLower().Contains(orderSpecParams.Search)
                    || x.ShipToAddress.State.ToLower().Contains(orderSpecParams.Search)
                    || x.ShipToAddress.City.ToLower().Contains(orderSpecParams.Search)
                    || x.ShipToAddress.Street.ToLower().Contains(orderSpecParams.Search)
                    || x.ShipToAddress.FirstName.ToLower().Contains(orderSpecParams.Search)
                    || x.ShipToAddress.LastName.ToLower().Contains(orderSpecParams.Search))
            )
        { }
    }
}