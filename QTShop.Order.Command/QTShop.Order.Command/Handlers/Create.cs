
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using QTShop.Order.Command.Repositories;

namespace QTShop.Order.Command.Handlers
{
    public class Create
    {
        public class Command : IRequest
        {
            public string BasketId { get; set; }
            public IEnumerable<Item> Items { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
               
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IOrdersRepository _ordersRepository;

            public Handler(IOrdersRepository ordersRepository)
            {
                _ordersRepository = ordersRepository;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
               await _ordersRepository.CreateOrder(new OrdersRepository.CreateOrderRequest
                {
                    BasketId = request.BasketId,
                    Items = request.Items.Select(i => new OrdersRepository.Item
                    {
                        ProductId = i.ProductId,
                        Price = i.Price,
                        Quantity = i.Quantity
                    })
                });
                return Unit.Value;
            }
        }
    }

    public class Item
    {
        public string ProductId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}