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
                PrintNewLine();
                if (choice == 1)
                {
                    //See all pets
                    seeAllPets();
                } else if (choice == 2)
                {
                    createPet();
                } else if (choice == 3)
                {
                    updatePet();
                } else if (choice == 4)
                {
                    deletePet();
                } else if (choice == 5)
                {
                    getPetsByType();
                } else if (choice == 6)
                {
                    sortByPrice();
                } else if (choice == 7)
                {
                    fiveCheapestPets();
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
            Print("--------------------------------");
            Print("5 - Get a list of pets by Pet Type");
            Print("6 - Get a list of pets, sorted by price");
            Print("7 - List 5 cheapest pets");
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

        private void sortByPrice()
        {
            Print("List of pets, sorted by price:");
            var pets = _petService.SortPetsByPrice(_petService.GetAllPets());
            foreach (Pet p in pets)
            {
                Print($"{p.ID}, {p.Name}, {p.Type.Name}, {p.Color}, {p.Birthdate}, {p.SoldDate}, {p.Price}");
            }
            PrintNewLine();
        }

        private void fiveCheapestPets()
        {
            Print("5 cheapest pets:");
            var pets = _petService.SortPetsByPrice(_petService.GetAllPets());
            for(int i = 0; i < 5; i++)
            {
                Pet p = pets[i];
                Print($"{p.ID}, {p.Name}, {p.Type.Name}, {p.Color}, {p.Birthdate}, {p.SoldDate}, {p.Price}");
            }
            PrintNewLine();
        }

        private void getPetsByType()
        {
            Print("List of pet types:");
            seeAllPetTypes();
            PrintNewLine();
            Print("Enter ID of Pet Type to search for:");
            var petTypeId = Console.ReadLine();
            int selection;
            while (!int.TryParse(petTypeId, out selection))
            {
                Print("You did not type a number! Try again!");
                petTypeId = Console.ReadLine();
            }
            
            while (_petTypeService.GetByID(selection) == null)
            {
                Print("Invalid Pet Type ID!");
                petTypeId = Console.ReadLine();
            }

            PetType petType = _petTypeService.GetByID(selection);

            Print($"All pets with Pet Type ({petType.Name}):");
            var pets = _petService.GetPetsByType(petType);
            foreach (Pet p in pets)
            {
                Print($"{p.ID}, {p.Name}, {p.Type.Name}, {p.Color}, {p.Birthdate}, {p.SoldDate}, {p.Price}");
            }
        }

        private void updatePet()
        {
            Print("Type ID of pet to update:");
            
            var petId = Console.ReadLine();
            int selection;
            while (!int.TryParse(petId, out selection))
            {
                Print("You did not type a number! Try again!");
                petId = Console.ReadLine();
            }

            Pet oldPet = _petService.GetPetById(selection);

            if (oldPet == null)
            {
                Print($"Pet with ID {selection} was not found!");
            }
            else
            {
                Print("Welcome to Update-A-Pet :-)");
                Print("Keep the field blank, and press enter to keep the old value!");
                PrintNewLine();
            
                Print($"The old name was {oldPet.Name}. Please enter a new name:");
                string petName = Console.ReadLine();
                petName = petName.Length > 0 ? petName : oldPet.Name;
                oldPet.Name = petName;
                PrintNewLine();

                Print($"The old Pet Type is {oldPet.Type.Name}. Please enter a new Pet Type ID:");
                seeAllPetTypes();
                var petType = Console.ReadLine();
                PetType pt = oldPet.Type;
                if (petType.Length > 0)
                {
                    int selection1;
                    while (!int.TryParse(petType, out selection1))
                    {
                        Print("You did not type a number! Try again!");
                        petType = Console.ReadLine();
                    }

                    while (_petTypeService.GetByID(selection1) == null)
                    {
                        Print("Selected ID does not exist! Try again!");
                        petType = Console.ReadLine();
                    }

                    pt = _petTypeService.GetByID(selection1);
                }

                oldPet.Type = pt;

                Print($"The old color is {oldPet.Color}. Please type a new color:");
                string petColor = Console.ReadLine();
                petColor = petColor.Length > 0 ? petColor : oldPet.Color;
                oldPet.Color = petColor;
                PrintNewLine();
            
                Print($"Old birthday is {oldPet.Birthdate}. Please enter new birthday: (Format: DD-MM-YYYY)");
                string petBirthUnformatted = Console.ReadLine();
                DateTime petBirthday = petBirthUnformatted.Length > 0 ? DateTime.Parse(petBirthUnformatted) : oldPet.Birthdate;
                oldPet.Birthdate = petBirthday;
                PrintNewLine();
            
                Print($"Old sold date is {oldPet.SoldDate}. Please enter new sold date: (Format: DD-MM-YYYY)");
                string petSoldUnformatted = Console.ReadLine();
                DateTime petSoldDate = petSoldUnformatted.Length > 0 ? DateTime.Parse(petSoldUnformatted) : oldPet.SoldDate;
                oldPet.SoldDate = petSoldDate;
                PrintNewLine();
            
                Print($"Old price is {oldPet.Price}. Please enter new price:");
                string petPriceUnformatted = Console.ReadLine();
                double petPrice = petPriceUnformatted.Length > 0 ? double.Parse(petPriceUnformatted) : oldPet.Price;
                oldPet.Price = petPrice;
                PrintNewLine();

                Print("Thank you! Your pet was updated :-)");
            }
        }

        private void deletePet()
        {
            Print("Type ID to delete:");

            var petId = Console.ReadLine();
            int selection;
            while (!int.TryParse(petId, out selection))
            {
                Print("You did not type a number! Try again!");
                petId = Console.ReadLine();
            }

            if (_petService.DeletePetById(selection))
            {
                Print($"Thank you! Pet with ID {selection} was deleted!");
            }
            else
            {
                Print($"Pet with ID {selection} was not found!");
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
            Print("Welcome to Create-A-Pet :-)");
            
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