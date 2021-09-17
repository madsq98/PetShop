using System.Collections.Generic;
using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.Core.IServices
{
    public interface IOwnerService
    {
        public List<Owner> GetAllOwners();

        public Owner GetOwnerById(int id);

        public Owner CreateOwner(Owner owner);

        public Owner UpdateOwner(int id, Owner owner);

        public bool DeleteOwnerById(int id);
    }
}