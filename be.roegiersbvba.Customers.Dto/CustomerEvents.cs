using System;
using System.Collections.Generic;
using System.Text;

namespace be.roegiersbvba.Customers.Dto
{
    public class CustomerEvents : IDto
    {
        public List<object> Events { get; set; }
    }
}
