using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Domain.OrderAggregate
{
    public class Order : Entity<long>, IAggregateRoot
    {
        public string BuyerId { get; private set; }
        public string BuyerName { get; private set; }
        public Address Address { get; private set; }
        public int ItemCount { get; private set; }

        protected Order() { }

        public Order(string userId, string userName, int itemCount, Address address)
        {
            BuyerId = userId;
            BuyerName = userName;
            Address = address;
            ItemCount = itemCount;
        }
    }
}
