using System.Collections.Generic;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;

namespace MQGroup.PetShop.Infrastructure.DataAccess.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private static List<Owner> _ownerTable = new List<Owner>();
        private static int nextID = 1;
        public List<Owner> GetAllOwners()
        {
            return _ownerTable;
        }

        public Owner GetOwnerById(int id)
        {
            foreach (Owner owner in _ownerTable)
            {
                if (owner.Id == id)
                    return owner;
            }

            return null;
        }

        public Owner CreateOwner(Owner owner)
        {
            owner.Id = nextID;
            nextID++;
            _ownerTable.Add(owner);
            return owner;
        }

        public Owner UpdateOwner(Owner owner)
        {
            return owner;
        }

        public bool DeleteOwnerById(int id)
        {
            for (int i = 0; i < _ownerTable.Count; i++)
            {
                Owner o = _ownerTable[i];
                if (o.Id == id)
                {
                    _ownerTable.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }
    }
}