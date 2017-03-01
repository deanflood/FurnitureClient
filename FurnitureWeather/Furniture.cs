using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FurnitureService.Models
{
    public enum Type
    {
        BED,
        CHAIR,
        TABLE
    }

    public class Furniture
    {
        public static int CounterId { get; set; }

        public int Id { get; set; }
        public string Make { get; set; }
        public Type Type { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }

        public Furniture(string make, Type type, double price, bool isAvailable)
        {
            Id = CounterId;
            CounterId++;
            Make = make;
            Type = type;
            Price = price;
            IsAvailable = isAvailable;
        }

        public Furniture()
        {
        }

        public override string ToString()
        {
            return "ID: " + Id + "\nMake: " + Make + "\nType: " + Type + "\nPrice: " + Price + "\nAvailable: " + IsAvailable + "\n"; 
        }

    }
}