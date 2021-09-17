using System.Collections.Generic;
using System.Linq;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;
using MQGroup.PetShop.Infrastructure.EFCore.Entities;

namespace MQGroup.PetShop.Infrastructure.EFCore.Repositories
{
    public class PetTypeRepository : IPetTypeRepository
    {
        private readonly PetApplicationContext _ctx;

        public PetTypeRepository(PetApplicationContext ctx)
        {
            _ctx = ctx;
        }
        public List<PetType> GetAllPetTypes()
        {
            return _ctx.PetTypes.Select(petTypeEntity => new PetType
            {
                ID = petTypeEntity.Id,
                Name = petTypeEntity.Name
            }).ToList();
        }

        public PetType GetByID(int id)
        {
            return _ctx.PetTypes.Select(petTypeEntity => new PetType
            {
                ID = petTypeEntity.Id,
                Name = petTypeEntity.Name
            }).FirstOrDefault(petType => petType.ID == id);
        }

        public PetType SavePetType(PetType petType)
        {
            var newEntity = _ctx.Add(new PetTypeEntity
            {
                Id = petType.ID,
                Name = petType.Name
            }).Entity;
            _ctx.SaveChanges();

            petType.ID = newEntity.Id;
            return petType;
        }

        public bool DeletePetTypeById(int id)
        {
            _ctx.PetTypes.Remove(new PetTypeEntity {Id = id});
            _ctx.SaveChanges();
            return true;
        }

        public PetType UpdatePetType(int id, PetType petType)
        {
            PetTypeEntity newEntity = new PetTypeEntity
            {
                Id = id,
                Name = petType.Name
            };
            _ctx.PetTypes.Update(newEntity);
            _ctx.SaveChanges();
            petType.ID = id;
            return petType;
        }
    }
}