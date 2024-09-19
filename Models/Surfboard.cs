using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfsupEmil.Models
{
    public enum SurfboardType
    {
        Shortboard,
        Longboard,
        Fish,
        Funboard,
        SUP
    }
    public class Surfboard
    {
        public int SurfboardId { get; set; }
        [Required] public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public double Volume { get; set; }
        [Required] public SurfboardType Type { get; set; }
        public double PriceOfPurchase { get; set; }
        public string? Equipment { get; set; }
        public double? HourlyPrice { get; set; }
        [NotMapped]
        public List<Order>? Orders { get; set; }
        public Surfboard() // IMPLICIT CONSTRUCTOR MADE EXPLICIT. I DON'T KNOW IF THIS IS STUPID
        {
        }
        public Surfboard(int id, string name, double length, double width, double thickness, double volume, SurfboardType type, double priceOfPurchase, string equipment, double hourlyPrice)
        {
            SurfboardId = id;
            Name = name;
            Length = length;
            Width = width;
            Thickness = thickness;
            Volume = volume;
            Type = type;
            PriceOfPurchase = priceOfPurchase;
            Equipment = equipment;
            HourlyPrice = hourlyPrice;
        }
        public Surfboard(string name, double length, double width, double thickness, double volume, SurfboardType type, double priceOfPurchase, string equipment, double hourlyPrice)
            : this(0, name, length, width, thickness, volume, type, priceOfPurchase, equipment, hourlyPrice)
        {
        }
    }
}
