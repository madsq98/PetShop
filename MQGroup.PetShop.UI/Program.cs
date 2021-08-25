using System;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Domain.IRepositories;
using MQGroup.PetShop.Domain.Services;
using MQGroup.PetShop.Infrastructure.DataAccess.Repositories;

namespace MQGroup.PetShop.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            //INITIALIZING
            IPetRepository petRepository = new PetRepository();
            IPetTypeRepository petTypeRepository = new PetTypeRepository();

            IPetService petService = new PetService(petRepository);
            IPetTypeService petTypeService = new PetTypeService(petTypeRepository);

            Menu menu = new Menu(petService, petTypeService);
            menu.ShowMenu();
        }
    }
}