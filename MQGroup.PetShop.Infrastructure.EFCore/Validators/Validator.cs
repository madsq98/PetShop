using System;
using System.Collections.Generic;
using System.IO;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;

namespace MQGroup.PetShop.Infrastructure.EFCore.Validators
{
    public class Validator
    {
        private List<string> errors = new List<string>();

        private readonly IOwnerRepository _ownerRepository;
        private readonly IPetTypeRepository _petTypeRepository;

        public Validator(IOwnerRepository ownerRepository, IPetTypeRepository petTypeRepository)
        {
            _ownerRepository = ownerRepository;
            _petTypeRepository = petTypeRepository;
        }
        public bool ValidatePet(Pet pet)
        {
            bool e = false;

            if (!OwnerExists(pet.Owner.Id))
                throw new FileNotFoundException("Owner ID does not exist!");

            if (!PetTypeExists((int)pet.Type.ID))
                throw new FileNotFoundException("Pet Type ID does not exist!");
            
            if (pet.Name.Length < 2)
            {
                errors.Add("Name must be 2 or more characters!");
                e = true;
            }

            if (pet.Color.Length < 2)
            {
                errors.Add("Color must be 2 or more characters!");
                e = true;
            }

            if (pet.Type.ID is 0 or null)
            {
                errors.Add("A Pet Type must be applied!");
                e = true;
            }

            if (pet.Owner.Id < 1)
            {
                errors.Add("An Owner must be applied!");
                e = true;
            }

            if (pet.Birthdate > DateTime.Now)
            {
                errors.Add("Birthday can not be later than today!");
                e = true;
            }

            return !e;
        }

        public string GetErrors(bool delete = true)
        {
            string returnString = "";
            foreach (var err in errors)
            {
                returnString += err + "\r\n";
            }

            errors = delete ? new List<string>() : errors;

            return returnString;
        }

        private bool OwnerExists(int id)
        {
            return _ownerRepository.GetOwnerById(id) != null;
        }

        private bool PetTypeExists(int id)
        {
            return _petTypeRepository.GetByID(id) != null;
        }
    }
}