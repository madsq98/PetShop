using System;
using System.Runtime.InteropServices;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.UI
{
    public class Menu
    {
        private static IPetService _petService;
        private static IPetTypeService _petTypeService;

        public Menu(IPetService petService, IPetTypeService petTypeService)
        {
            _petService = petService;
            _petTypeService = petTypeService;
        }

        public void ShowMenu()
        {
            WelcomeGreeting();
            StartLoop();
        }
        
        //Internal methods
        private void StartLoop()
        {
            int choice;
            while ((choice = GetMainMenuSelection()) != 0)
            {
                if (choice == 1)
                {
                    //See all pets
                    seeAllPets();
                } else if (choice == 2)
                {
                    createPet();
                }
                
                PrintNewLine();
            }
        }
        
        private int GetMainMenuSelection()
        {
            ShowMainMenu();
            PrintNewLine();
            var selectionString = Console.ReadLine();
            int selection;
            if (int.TryParse(selectionString, out selection))
            {
                return selection;
            }
            return -1;
        }

        private void ShowMainMenu()
        {
            Print("1 - See all pets");
            Print("2 - Create a pet");
            Print("3 - Edit a pet");
            Print("4 - Delete a pet");
            Print("0 - Exit");
        }
        private void WelcomeGreeting()
        {
            Print("Welcome to the pet shop!");
            Print("You have the following options:");
        }
        private void Print(string value)
        {
            Console.WriteLine(value);
        }

        private void PrintNewLine()
        {
            Print(" ");
        }

        private void seeAllPetTypes()
        {
            var petTypes = _petTypeService.GetAllPetTypes();
            foreach (PetType p in petTypes)
            {
                Print($"ID: {p.ID} | {p.Name}");
            }
        }
        
        private void seeAllPets()
        {
            Print("All pets:");
            var pets = _petService.GetAllPets();
            foreach(Pet p in pets)
            {
                Print($"{p.ID}, {p.Name}, {p.Type.Name}, {p.Color}, {p.Birthdate}, {p.SoldDate}, {p.Price}");
            }
        }

        private void createPet()
        {
            Print("Welcome to Create-A-Pat :-)");
            
            Print("Please enter a pet name:");
            string petName = Console.ReadLine();
            PrintNewLine();

            Print("Please select a Pet Type ID:");
            seeAllPetTypes();
            var petType = Console.ReadLine();
            int selection;
            while (!int.TryParse(petType, out selection))
            {
                Print("You did not type a number! Try again!");
                petType = Console.ReadLine();
            }

            while (_petTypeService.GetByID(selection) == null)
            {
                Print("Selected ID does not exist! Try again!");
                petType = Console.ReadLine();
            }

            PetType pt = _petTypeService.GetByID(selection);
            
            Print("Please enter a color:");
            string petColor = Console.ReadLine();
            PrintNewLine();
            
            Print("Please enter birthday: (Format: DD-MM-YYYY)");
            string petBirthUnformatted = Console.ReadLine();
            DateTime petBirthday = DateTime.Parse(petBirthUnformatted);
            PrintNewLine();
            
            Print("Please enter sold date: (Format: DD-MM-YYYY)");
            string petSoldUnformatted = Console.ReadLine();
            DateTime petSoldDate = DateTime.Parse(petSoldUnformatted);
            PrintNewLine();
            
            Print("Please enter price:");
            string petPriceUnformatted = Console.ReadLine();
            double petPrice = double.Parse(petPriceUnformatted);
            PrintNewLine();

            Print("Thank you! Your pet was created :-)");
            _petService.CreatePet(
                new Pet
                {
                    Name = petName, Color = petColor, Birthdate = petBirthday, SoldDate = petSoldDate, Type = pt, Price = petPrice
                }
            );
        }
    }
}