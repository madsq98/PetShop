﻿using System.Collections.Generic;
using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.Core.IServices
{
    public interface IPetService
    {
        public List<Pet> GetAllPets();
    }
}