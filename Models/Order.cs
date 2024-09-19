using System.ComponentModel.DataAnnotations;

namespace SurfsupEmil.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required] public DateTime? OrderDate { get; set; } = DateTime.Now; //THE DATE OF THE ORDER IS NOT PASSED TO THE USER BUT NEEDED BY DEVELOPMENT
        public DateTime? PickupDate { get; set; } = null; // REMOVED REQUIRED FIELD BECAUSE IT WAS DIFFICULT TO ERROR HANDLE
        public DateTime? ReturnDate { get; set; } = null;
        public double TotalPrice { get; set; }
        [Required] public List<Surfboard> Surfboards { get; set; } = new List<Surfboard>();

        public Order()
        {
        }
        public Order(int orderId, DateTime orderDate, DateTime pickupDate, DateTime returnDate, List<Surfboard> surfboards)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            PickupDate = pickupDate;
            ReturnDate = returnDate;
            Surfboards = new List<Surfboard>(surfboards);
            TotalPrice = surfboards.Sum(x => x.PriceOfPurchase);
        }
        public Order(DateTime orderDate, DateTime pickupDate, DateTime returnDate, double totalPrice, List<Surfboard> surfboards) //OVERLOADED CONSTRUCTOR W/O ID-PARAM
            : this(0, orderDate, pickupDate, returnDate, surfboards)
        {
        }
        public void AddSurfboard(Surfboard surfboard)
        {
            Surfboards.Add(surfboard);
            TotalPrice += surfboard.PriceOfPurchase;
        }
    }
}
