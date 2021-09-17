using System;
using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.Infrastructure.EFCore.Entities
{
    public class PetEntity
    {
        public int? ID { get; set; }
        
        public string Name { get; set; }
        
        public int TypeId { get; set; }
        
        public PetTypeEntity Type { get; set; }
        
        public int OwnerId { get; set; }
        
        public OwnerEntity Owner { get; set; }
        
        public DateTime Birthdate { get; set; }
        
        public DateTime SoldDate { get; set; }
        
        public string Color { get; set; }
        
        public double Price { get; set; }
    }
}