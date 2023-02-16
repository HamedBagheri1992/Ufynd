using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Ufynd.Task2.Domain.Entities;

namespace Ufynd.Task2.Infrastructure.Persistence
{
    public class Task2DbContext : DbContext
    {
        public Task2DbContext()
        {
        }

        public Task2DbContext(DbContextOptions<Task2DbContext> options) : base(options)
        {
        }

        public virtual DbSet<AutoProcessing> AutoProcessings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}
