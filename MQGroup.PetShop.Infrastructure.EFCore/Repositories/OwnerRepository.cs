using System.Collections.Generic;
using System.Linq;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;
using MQGroup.PetShop.Infrastructure.EFCore.Entities;

namespace MQGroup.PetShop.Infrastructure.EFCore.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly PetApplicationContext _ctx;

        public OwnerRepository(PetApplicationContext ctx)
        {
            _ctx = ctx;
        }
        public List<Owner> GetAllOwners()
        {
            return _ctx.Owners
                .Select(ownerEntity => new Owner
                {
                    Id = ownerEntity.Id,
                    FirstName = ownerEntity.FirstName,
                    LastName = ownerEntity.LastName,
                    Address = ownerEntity.Address,
                    Email = ownerEntity.Email,
                    PhoneNumber = ownerEntity.PhoneNumber
                })
                .ToList();
        }

        public Owner GetOwnerById(int id)
        {
            return _ctx.Owners
                .Select(ownerEntity => new Owner
                {
                    Id = ownerEntity.Id,
                    FirstName = ownerEntity.FirstName,
                    LastName = ownerEntity.LastName,
                    Address = ownerEntity.Address,
                    Email = ownerEntity.Email,
                    PhoneNumber = ownerEntity.PhoneNumber
                }).FirstOrDefault(owner => owner.Id == id);
        }

        public Owner CreateOwner(Owner owner)
        {
            var newEntity = _ctx.Owners.Add(new OwnerEntity
                {
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Address = owner.Address,
                    Email = owner.Email,
                    PhoneNumber = owner.PhoneNumber
                }
            ).Entity;
            _ctx.SaveChanges();

            owner.Id = newEntity.Id;
            return owner;
        }

        public Owner UpdateOwner(int id, Owner owner)
        {
            OwnerEntity newEntity = new OwnerEntity
            {
                Id = id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Address = owner.Address,
                Email = owner.Email,
                PhoneNumber = owner.PhoneNumber
            };
            _ctx.Owners.Update(newEntity);
            _ctx.SaveChanges();
            owner.Id = id;
            return owner;
        }

        public bool DeleteOwnerById(int id)
        {
            _ctx.Owners.Remove(new OwnerEntity {Id = id});
            _ctx.SaveChanges();
            return true;
        }
    }
}