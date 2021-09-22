using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.Domain.Validators
{
    public interface IValidator
    {
        public bool ValidatePet(Pet pet);

        public bool ValidatePetType(PetType petType);

        public bool ValidateOwner(Owner owner);

        public string GetErrors(bool delete = true);

        public bool PetExists(int id);

        public bool PetTypeExists(int id);

        public bool OwnerExists(int id);
    }
}