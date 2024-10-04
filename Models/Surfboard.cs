using SurfsupEmil.Models.Validations;
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
        [TotalPriceGreaterThanZero]
        public double PriceOfPurchase { get; set; }
        public string? Equipment { get; set; }
        public List<Order>? Orders { get; set; }
        [Timestamp] public Byte[] RowVersion { get; set; }
        public Surfboard() // IMPLICIT CONSTRUCTOR MADE EXPLICIT. I DON'T KNOW IF THIS IS STUPID
        {
        }
        public Surfboard(int id, string name, double length, double width, double thickness, double volume, SurfboardType type, double priceOfPurchase, string equipment)
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
        }
        public Surfboard(string name, double length, double width, double thickness, double volume, SurfboardType type, double priceOfPurchase, string equipment)
            : this(0, name, length, width, thickness, volume, type, priceOfPurchase, equipment)
        {
        }
    }
}
