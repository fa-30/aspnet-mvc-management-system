using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Shared;

namespace Demo.DAL.Data.Configuration
{
    public class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(D => D.CreatedOn).HasDefaultValueSql(sql: "GETDATE()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql(sql: "GETDATE()"); 
        }
    }
}
