using System.Collections.Generic;
using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.Domain.IRepositories
{
    public interface IOwnerRepository
    {
        public List<Owner> GetAllOwners();

        public Owner GetOwnerById(int id);

        public Owner CreateOwner(Owner owner);

        public Owner UpdateOwner(Owner owner);

        public bool DeleteOwnerById(int id);
    }
}