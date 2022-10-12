using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("Id");
            builder.Property(u => u.FirstName).HasColumnName("FirstName");
            builder.Property(u => u.LastName).HasColumnName("LastName");
            builder.Property(u => u.Email).HasColumnName("Email");
            builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
            builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
            builder.Property(u => u.Status).HasColumnName("Status").HasDefaultValue(true);
            builder.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType");


            User[] userSeeds =
            {
                new User(
                    id:1,
                    passwordHash:new byte[255],
                    passwordSalt:new byte[255],
                    firstName:"Mustafa", 
                    lastName:"Uygur",
                    email:"mustafauygur@outlook.com",
                    status:true,
                    authenticatorType:0),
                new User(
                    id:2,
                    passwordHash:new byte[255],
                    passwordSalt:new byte[255],
                    firstName:"Kodlama",
                    lastName:"io",
                    email:"kodlamaio@kodlamaio.com",
                    status:true,
                    authenticatorType:0)
            };
            builder.HasData(userSeeds);

        }
    }
}
