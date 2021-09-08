using System.Collections.Generic;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;

namespace MQGroup.PetShop.Domain.Services
{
    public class OwnerService : IOwnerService
    {
        private IOwnerRepository _repo;

        public OwnerService(IOwnerRepository repo)
        {
            _repo = repo;
        }
        
        public List<Owner> GetAllOwners()
        {
            return _repo.GetAllOwners();
        }

        public Owner GetOwnerById(int id)
        {
            return _repo.GetOwnerById(id);
        }

        public Owner CreateOwner(Owner owner)
        {
            return _repo.CreateOwner(owner);
        }

        public Owner UpdateOwner(Owner owner)
        {
            return _repo.UpdateOwner(owner);
        }

        public bool DeleteOwnerById(int id)
        {
            return _repo.DeleteOwnerById(id);
        }
    }
}