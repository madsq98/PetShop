using System;

namespace MQGroup.PetShop.WebApi.DTO
{
    public class PetDto
    {
        public string Name { get; set; }
        
        public int PetTypeId { get; set; }
        
        public int OwnerId { get; set; }
        
        public DateTime Birthdate { get; set; }
        
        public DateTime SoldDate { get; set; }
        
        public string Color { get; set; }
        
        public double Price { get; set; }
    }
}