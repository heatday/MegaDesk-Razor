using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MegaDesk_Razor.Models
{
    public class Quote
    {
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string? CustomerName { get; set; }
        public DateTime Date { get; set; } 
        public int DeliveryTypeId { get; set; }
        public int MaterialId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid width.")]
        public int Width { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid depth.")]
        public int Depth { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid drawer count.")]
        public int DrawerCount { get; set; }

        public DeliveryType? DeliveryType { get; set; }

        public Material? Material { get; set; }

        public double Price => CalculatePrice();
        public Quote()
        {
            Date = DateTime.Now; 
        }

        private double CalculatePrice()
        {
            double price = 0;
            double surfaceArea = Width * Depth;
            double materialCost = 0;
            double rushCost = 0;

            // Assign the SurfaceMaterial based on the material of the desk

            if (DeliveryType != null)
            {
                switch (DeliveryType.Type)
                {
                    case "3 Days":
                        if (surfaceArea < 1000)
                            rushCost = 60;
                        else if (surfaceArea >= 1000 && surfaceArea < 2000)
                            rushCost = 70;
                        else
                            rushCost = 80;
                        break;
                    case "5 Days":
                        if (surfaceArea < 1000)
                            rushCost = 40;
                        else if (surfaceArea >= 1000 && surfaceArea < 2000)
                            rushCost = 50;
                        else
                            rushCost = 60;
                        break;
                    case "7 Days":
                        if (surfaceArea < 1000)
                            rushCost = 30;
                        else if (surfaceArea >= 1000 && surfaceArea < 2000)
                            rushCost = 35;
                        else
                            rushCost = 40;
                        break;
                    default:
                        rushCost = 0;
                        break;
                }
            }

            if (surfaceArea > 1000)
                price += (double)ConstPrice.SurfaceArea * (surfaceArea - 1000);
            price += (double)ConstPrice.Base + ((double)ConstPrice.Drawer * DrawerCount) + materialCost + rushCost;
            return price;
        }

    }

        public enum ConstPrice
    {
        Base = 200,
        SurfaceArea = 1,
        Drawer = 50
    }
}
