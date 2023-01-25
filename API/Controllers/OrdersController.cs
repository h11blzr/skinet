using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IGenericRepo<Order> _orderRepo;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController(IGenericRepo<Order> orderRepo,
                                IOrderService orderService,
                                IMapper mapper)
        {
            _mapper = mapper;
            _orderRepo = orderRepo;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
            
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<OrderToReturnDto>>> GetOrderForUser([FromQuery]ProductSpecParams ordersParams)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var ordersForUser = await _orderService.GetOrderForUserAsync(email);

            var spec = new OrdersWithItemsAndOrderingSpecification(ordersParams);

            var countSpec = new OrdersWithFiltersForCountSpecification(ordersParams);

            var totalItems = await _orderRepo.CountAsync(countSpec);
            
            var ordersToReturn = await _orderRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(ordersToReturn);

            return Ok(new Pagination<OrderToReturnDto>(ordersParams.PageIndex, ordersParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Order, OrderToReturnDto>(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}