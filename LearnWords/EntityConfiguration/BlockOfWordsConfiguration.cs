using LearnWords.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnWords.EntityConfiguration
{
	internal class BlockOfWordsConfiguration : IEntityTypeConfiguration<BlockOfWords>
	{
		public void Configure(EntityTypeBuilder<BlockOfWords> builder)
		{
			builder.ToTable("BlockOfWords");
			builder.HasKey(x => x.Id);

            builder.Property(x => x.RepeatCount).IsRequired();
			builder.Property(x => x.NextRepeat).IsRequired();
			builder.Property(x => x.Created).IsRequired();
			builder.Property(x => x.Deleted);

            builder.Property(e => e.Version).HasColumnName(@"Version")
                .IsRequired().HasColumnType("timestamp").HasMaxLength(8).IsRowVersion();
        }
	}
}