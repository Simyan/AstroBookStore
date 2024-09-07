using MediatR;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class StartOrderCommand : IRequest<bool>
    {
        public InitiateOrderDto InitiateOrderDto { get; set; }
        public StartOrderCommand(InitiateOrderDto dto) => InitiateOrderDto = dto;
    }
}
