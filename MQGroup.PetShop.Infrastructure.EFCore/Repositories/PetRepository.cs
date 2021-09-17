using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;
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
            return ConversionOfPet().ToList();
        }

        public Pet AddPet(Pet pet)
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

        public bool DeletePetById(int id)
        {
            _ctx.Pets.Remove(new PetEntity {ID = id});
            _ctx.SaveChanges();
            return true;
        }

        public Pet GetPetById(int? id)
        {
            return ConversionOfPet().FirstOrDefault(pet => pet.ID == id);
        }

        public Pet UpdatePet(int id, Pet pet)
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