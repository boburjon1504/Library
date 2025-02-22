using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.DataContext.EntityConfigurations;

class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasIndex(u => u.Username)
            .IsUnique();

        builder
            .HasMany<Book>()
            .WithOne()
            .HasForeignKey(b => b.UserId);
    }
}
