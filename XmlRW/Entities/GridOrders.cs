using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlRW.Entities
{
   public class GridOrders
    {
        public string CustomerRef { get; set; }
        public string DelFrom { get; set; }
        public string DelTo { get; set; }
        public DateTime IssueDate { get; set; }
        public string DelStore { get; set; }
        public string DelNumber { get; set; }
        public string BuyerRef { get; set; }
        public string OurRef { get; set; }
        public string OurName { get; set; }
        public string RowId { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public bool WillFlow { get; set; }
    }
}
