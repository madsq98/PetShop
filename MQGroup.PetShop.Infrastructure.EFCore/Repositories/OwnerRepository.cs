using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                return ConversionOfOwner().ToList();
            }
            catch (DbUpdateException)
            {
                throw new SystemException("An internal error occured. Please contact the system provider.");
            }
        }

        public Owner GetOwnerById(int id)
        {
            try
            { 
                return ConversionOfOwner().FirstOrDefault(owner => owner.Id == id);
            }
            catch (DbUpdateException)
            {
                throw new SystemException("An internal error occured. Please contact the system provider.");
            }
        }

        public Owner CreateOwner(Owner owner)
        {
            try
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
            catch (DbUpdateException)
            {
                throw new SystemException("An internal error occured. Please contact the system provider.");
            }
        }

        public Owner UpdateOwner(int id, Owner owner)
        {
            try
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
            catch (DbUpdateException)
            {
                throw new SystemException("An internal error occured. Please contact the system provider.");
            }
        }

        public bool DeleteOwnerById(int id)
        {
            _ctx.Owners.Remove(new OwnerEntity {Id = id});
            _ctx.SaveChanges();
            return true;
        }

        private IQueryable<Owner> ConversionOfOwner()
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
                });
        }
    }
}