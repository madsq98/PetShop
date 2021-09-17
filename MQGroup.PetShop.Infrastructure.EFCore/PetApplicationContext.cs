using Microsoft.EntityFrameworkCore;
using MQGroup.PetShop.Infrastructure.EFCore.Entities;

namespace MQGroup.PetShop.Infrastructure.EFCore
{
    public class PetApplicationContext : DbContext
    {
        public DbSet<OwnerEntity> Owners { get; set; }
        public DbSet<PetEntity> Pets { get; set; }
        public DbSet<PetTypeEntity> PetTypes { get; set; }

        public PetApplicationContext(DbContextOptions<PetApplicationContext> options) : base(options)
        {
            
        }
    }
}