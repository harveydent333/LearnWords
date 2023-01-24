using LearnWords.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnWords.EntityConfiguration
{
	internal class WordConfiguration : IEntityTypeConfiguration<Word>
	{
		public void Configure(EntityTypeBuilder<Word> builder)
		{
			builder.ToTable("Words");
			builder.HasKey(x => x.Id);

            builder.Ignore(x => x.IsLearnedInBlock);

            builder.Property(x => x.Russian).HasMaxLength(350).IsRequired();
			builder.Property(x => x.English).HasMaxLength(350).IsRequired();
            builder.Property(e => e.Version).HasColumnName(@"Version")
                .IsRequired().HasColumnType("timestamp").HasMaxLength(8).IsRowVersion();

            builder.Property(x => x.PartOfSpeech).HasColumnType("tinyint").IsRequired();
        }
	}
}