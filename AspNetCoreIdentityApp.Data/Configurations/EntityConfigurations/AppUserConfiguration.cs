using AspNetCoreIdentityApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Data.Configurations.EntityConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.City).HasMaxLength(100).IsRequired(false);
            builder.Property(u => u.Picture).IsRequired(false);
            builder.Property(u => u.BirtDate).HasColumnType("date").IsRequired(false);
            builder.Property(u => u.Gender);
        }
    }
}
