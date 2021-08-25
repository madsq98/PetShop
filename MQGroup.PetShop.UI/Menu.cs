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

        private void seeAllPets()
        {
            Print("All pets:");
            var pets = _petService.GetAllPets();
            foreach(Pet p in pets)
            {
                Print($"{p.ID}, {p.Name}, {p.Type.Name}, {p.Color}, {p.Birthdate}, {p.SoldDate}, {p.Price}");
            }
        }
    }
}