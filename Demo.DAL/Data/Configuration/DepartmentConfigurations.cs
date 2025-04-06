
using Demo.DAL.Models.DepartmentModel;

namespace Demo.DAL.Data.Configuration
{
    public class DepartmentConfigurations :BaseEntityConfigurations<Department>, IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property( D => D.Id).UseIdentityColumn(seed: 10, increment: 10);
            builder.Property( D => D.Name).HasColumnType(typeName: "varchar(20)");
            builder.Property( D => D.Code).HasColumnType(typeName: "varchar(20)");
            base.Configure(builder);
        }
    }
}
