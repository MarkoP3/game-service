using GameService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations;

public class ChoiceConfiguration :
    IEntityTypeConfiguration<Choice>
{
    public void Configure(EntityTypeBuilder<Choice> builder)
    {
        builder.ToTable("choices", "game");
        builder.Property(x => x.Name)
        .HasMaxLength(12)
            .IsRequired();

        builder.HasMany(c => c.WeakerChoices)
            .WithMany();
    }
}
