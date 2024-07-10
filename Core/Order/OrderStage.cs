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
        /*Note: This class can be expanded to better encapsulate some key details, for instance action related specific information. Perhaps we can turn this into an abstract class and implement specific concrete classes inheriting from it - like ProcessOrderStage which can hold references to delivery and payment.*/
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
