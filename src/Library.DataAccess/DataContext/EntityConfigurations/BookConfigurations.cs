using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.DataContext.EntityConfigurations;

public class BookConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Author)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasIndex(b => b.Title)
            .IsUnique();
    }
}
