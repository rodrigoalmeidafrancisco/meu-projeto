using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Contexts
{
    public class ContextDefault : DbContext
    {
        public ContextDefault(DbContextOptions<ContextDefault> options) : base(options)
        {
        }

        //public DbSet<xxx> xxx { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new MapXXXX());
        }

        #region Suporte a Triggers - EntityFramework >= 8.0

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }

        public class BlankTriggerAddingConvention : IModelFinalizingConvention
        {
            public virtual void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
            {
                foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
                {
                    var table = StoreObjectIdentifier.Create(entityType, StoreObjectType.Table);
                    if (table != null
                        && entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(table.Value) == null)
                        && (entityType.BaseType == null || entityType.GetMappingStrategy() != RelationalAnnotationNames.TphMappingStrategy))
                    {
                        entityType.Builder.HasTrigger(table.Value.Name + "_Trigger");
                    }

                    foreach (var fragment in entityType.GetMappingFragments(StoreObjectType.Table))
                    {
                        if (entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(fragment.StoreObject) == null))
                        {
                            entityType.Builder.HasTrigger(fragment.StoreObject.Name + "_Trigger");
                        }
                    }
                }
            }
        }

        #endregion Suporte a Triggers - EntityFramework >= 8.0

    }
}
