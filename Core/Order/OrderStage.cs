using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Order
{
    public class OrderStage : IEntity
    {
        public Guid Id { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public OrderAction OrderAction { get; private set; }

        public OrderActionStatus OrderActionStatus { get; protected set; }

        private OrderStage() { }

        public OrderStage(OrderAction orderAction)
        {
            Id = new Guid();
            StartDate = DateTime.Now;
            OrderAction = orderAction;
            OrderActionStatus = OrderActionStatus.Pending;
        }

        public void OrderCompleted()
        {
            this.OrderActionStatus = OrderActionStatus.Finished;
            this.EndDate = DateTime.Now;
        }

    }
}
