using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;
using MQGroup.PetShop.Domain.Validators;
using MQGroup.PetShop.Infrastructure.EFCore.Entities;

namespace MQGroup.PetShop.Infrastructure.EFCore.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly PetApplicationContext _ctx;

        public PetRepository(PetApplicationContext ctx)
        {
            _ctx = ctx;
        }
        public List<Pet> ReadAllPets()
        {
            try
            {
                return ConversionOfPet().ToList();
            }
            catch (DbUpdateException)
            {
                throw new SystemException("An internal error occured. Please contact the system provider.");
            }
        }

        public Pet AddPet(Pet pet)
        {
            try
            {
                var newEntity = _ctx.Pets.Add(new PetEntity
                {
                    ID = pet.ID,
                    Name = pet.Name,
                    Color = pet.Color,
                    Birthdate = pet.Birthdate,
                    SoldDate = pet.SoldDate,
                    Price = pet.Price,
                    TypeId = (int) pet.Type.ID,
                    OwnerId = pet.Owner.Id
                }).Entity;
                _ctx.SaveChanges();

                return GetPetById(newEntity.ID);
            }
            catch (DbUpdateException)
            {
                throw new SystemException("An internal error occured. Please contact the system provider.");
            }
        }

        public bool DeletePetById(int id)
        {
            try
            {
                _ctx.Pets.Remove(new PetEntity {ID = id});
                _ctx.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new SystemException("An internal error occured. Please contact the system provider.");
            }
        }

        public Pet GetPetById(int? id)
        {
            try
            {
                return ConversionOfPet().FirstOrDefault(pet => pet.ID == id);
            }
            catch (DbUpdateException)
            {
                throw new SystemException("An internal error occured. Please contact the system provider.");
            }
        }

        public Pet UpdatePet(int id, Pet pet)
        {
            try
            {
                var newEntity = new PetEntity
                {
                    ID = id,
                    Name = pet.Name,
                    Color = pet.Color,
                    Birthdate = pet.Birthdate,
                    SoldDate = pet.SoldDate,
                    Price = pet.Price,
                    TypeId = (int) pet.Type.ID
                };
                _ctx.Pets.Update(newEntity);
                _ctx.SaveChanges();
                pet.ID = id;
                return pet;
            }
            catch (DbUpdateException)
            {
                throw new SystemException("An internal error occured. Please contact the system provider.");
            }
        }

        public List<Pet> ReadPetsByType(PetType petType)
        {
            throw new System.NotImplementedException();
        }

        private IQueryable<Pet> ConversionOfPet()
        {
            return _ctx.Pets
                .Include(petEntity => petEntity.Type)
                .Include(petEntity => petEntity.Owner)
                .Select(petEntity => new Pet
                {
                    ID = petEntity.ID,
                    Name = petEntity.Name,
                    Birthdate = petEntity.Birthdate,
                    SoldDate = petEntity.SoldDate,
                    Color = petEntity.Color,
                    Price = petEntity.Price,
                    Type = new PetType {ID = petEntity.Type.Id, Name = petEntity.Type.Name},
                    Owner = new Owner
                    {
                        Id = petEntity.Owner.Id,
                        FirstName = petEntity.Owner.FirstName,
                        LastName = petEntity.Owner.LastName,
                        Address = petEntity.Owner.Address,
                        Email = petEntity.Owner.Email,
                        PhoneNumber = petEntity.Owner.PhoneNumber
                    }
                });
        }
    }
}