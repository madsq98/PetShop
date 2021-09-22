using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;

namespace MQGroup.PetShop.Domain.Validators
{
    public class Validator : IValidator
    {
        private List<string> errors = new List<string>();

        private readonly IPetRepository _petRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPetTypeRepository _petTypeRepository;

        public Validator(IPetRepository petRepository, IOwnerRepository ownerRepository, IPetTypeRepository petTypeRepository)
        {
            _petRepository = petRepository;
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

        public bool ValidatePetType(PetType petType)
        {
            bool e = false;

            if (petType.Name.Length < 2)
            {
                errors.Add("Name must be 2 or more characters!");
                e = true;
            }

            return !e;
        }

        public bool ValidateOwner(Owner owner)
        {
            bool e = false;

            if (owner.FirstName.Length < 2)
            {
                errors.Add("First name must be 2 or more characters!");
                e = true;
            }

            if (owner.LastName.Length < 2)
            {
                errors.Add("Last name must be 2 or more characters!");
                e = true;
            }

            if (!IsValidEmail(owner.Email))
            {
                errors.Add("Email must be in the correct format! (email@petshop.com)");
                e = true;
            }

            if (owner.Address.Length < 10)
            {
                errors.Add("Address must be 10 or more characters!");
                e = true;
            }

            if (owner.PhoneNumber.Length < 11)
            {
                errors.Add("Phone number must be in the correct format! (+4576343422)");
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

        public bool PetExists(int id)
        {
            return _petRepository.GetPetById(id) != null;
        }

        public bool OwnerExists(int id)
        {
            return _ownerRepository.GetOwnerById(id) != null;
        }

        public bool PetTypeExists(int id)
        {
            return _petTypeRepository.GetByID(id) != null;
        }
        
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}