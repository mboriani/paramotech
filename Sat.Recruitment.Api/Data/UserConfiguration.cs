using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sat.Recruitment.Api.Model;

namespace Sat.Recruitment.Api.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.Email);

            builder.Property(k => k.Email)
                .IsRequired();

            builder.Property(k => k.Money)
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            builder.Property(k => k.Address)
                .IsRequired();

            builder.Property(k => k.Name)
                .IsRequired();

            builder.Property(k => k.Phone)
                .IsRequired();

            builder.HasOne<Role>(x => x.Role);
            
            builder.ToTable(nameof(User));
        }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(k => k.Id)
                .ValueGeneratedOnAdd();

            builder.HasDiscriminator<int>("RoleType")
                .HasValue<Normal>(0)
                .HasValue<SuperUser>(1)
                .HasValue<Premium>(2);
        }
    }
}