using Core.Entities.OrderAggregate;

namespace Core.Specification
{
    public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
    {
        public OrdersWithItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrdersWithItemsAndOrderingSpecification(int id, string email) : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }

        public OrdersWithItemsAndOrderingSpecification(ProductSpecParams orderSpecParams) 
            : base(x =>
                (string.IsNullOrEmpty(orderSpecParams.Search)
                    || x.ShipToAddress.Zipcode.ToLower().Contains(orderSpecParams.Search)
                    || x.ShipToAddress.State.ToLower().Contains(orderSpecParams.Search)
                    || x.ShipToAddress.City.ToLower().Contains(orderSpecParams.Search)
                    || x.ShipToAddress.Street.ToLower().Contains(orderSpecParams.Search)
                    || x.ShipToAddress.FirstName.ToLower().Contains(orderSpecParams.Search)
                    || x.ShipToAddress.LastName.ToLower().Contains(orderSpecParams.Search))
            )
        {
            ApplyPaging(orderSpecParams.PageSize * (orderSpecParams.PageIndex - 1), orderSpecParams.PageSize);

            if (!string.IsNullOrEmpty(orderSpecParams.Sort))
            {
                switch (orderSpecParams.Sort)
                {
                    case "orderDate":
                        if (orderSpecParams.Reversed)
                        {
                            AddOrderBy(o => o.OrderDate);
                            break;
                        }
                        AddOrderByDescending(o => o.OrderDate);
                        break;
                    case "totalCost":
                        if (orderSpecParams.Reversed)
                        {
                            AddOrderBy(o => o.Subtotal);
                            break;
                        }
                        AddOrderByDescending(o => o.Subtotal);
                        break;
                    default:
                        AddOrderBy(o => o.OrderDate);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(o => o.OrderDate);
            }
        }
    }
}