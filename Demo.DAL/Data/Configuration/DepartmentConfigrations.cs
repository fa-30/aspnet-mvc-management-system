
namespace Demo.DAL.Data.Configuration
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property( D => D.Id).UseIdentityColumn(seed: 10, increment: 10);
            builder.Property( D => D.Name).HasColumnType(typeName: "varchar(20)");
            builder.Property( D => D.Code).HasColumnType(typeName: "varchar(20)");
            builder.Property(D => D.CreatedOn).HasDefaultValueSql(sql: "GETDATE()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql(sql: "GETDATE()");
        }
    }
}
