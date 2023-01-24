using LearnWords.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnWords.EntityConfiguration
{
	internal class WordBlockConfiguration : IEntityTypeConfiguration<WordBlock>
	{
		public void Configure(EntityTypeBuilder<WordBlock> builder)
		{
			builder.ToTable("WordBlocks");
			builder.HasKey(x => x.Id);

            builder.Property(x => x.BlockId).IsRequired();
			builder.Property(x => x.WordId).IsRequired();
            builder.Property(e => e.Version).HasColumnName(@"Version")
                .IsRequired().HasColumnType("timestamp").HasMaxLength(8).IsRowVersion();

            builder
                .HasOne(x => x.Word)
                .WithMany()
                .HasForeignKey(x => x.WordId);
            
            builder
                .HasOne(x => x.BlockOfWords)
                .WithMany(b => b.Words)
                .HasForeignKey(x => x.BlockId);
        }
	}
}