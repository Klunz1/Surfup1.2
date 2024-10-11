using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfsupEmil.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required] public DateTime? OrderDate { get; set; } = DateTime.Now; //THE DATE OF THE ORDER IS NOT PASSED TO THE USER BUT NEEDED BY DEVELOPMENT
        public DateTime? PickupDate { get; set; } = null; // REMOVED REQUIRED FIELD BECAUSE IT WAS DIFFICULT TO ERROR HANDLE
        public DateTime? ReturnDate { get; set; } = null;
        public double TotalPrice => Surfboards.Sum(x => x.PriceOfPurchase);

        public string UserEmail { get; set; }
        public List<Surfboard> Surfboards { get; set; }

        public Order()
        {
            Surfboards = new List<Surfboard>();
        }
        public Order(int orderId, DateTime orderDate, DateTime pickupDate, DateTime returnDate)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            PickupDate = pickupDate;
            ReturnDate = returnDate;
            Surfboards = new List<Surfboard>();
            //TotalPrice = Surfboards.Sum(x => x.PriceOfPurchase);
        }
        public Order(DateTime orderDate, DateTime pickupDate, DateTime returnDate, double totalPrice) //OVERLOADED CONSTRUCTOR W/O ID-PARAM
            : this(0, orderDate, pickupDate, returnDate)
        {
        }
        public void AddSurfboard(Surfboard surfboard)
        {
            Surfboards.Add(surfboard);
            //TotalPrice += surfboard.PriceOfPurchase;
        }
    }
}
