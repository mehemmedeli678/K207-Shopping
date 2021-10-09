using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Models
{
    public class Order:BaseEntity
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string OrderCode { get; set; }
        public int PaymentMetod { get; set; }
        public decimal totalamount { get; set; }
        public decimal Discount { get; set; }
        public decimal DeliveryChange { get; set; }
        public DateTime PlacedOn { get; set; }
        public string TlansactionID { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
        public virtual List<OrderHistory> OrderHistory { get; set; }
    }
}
