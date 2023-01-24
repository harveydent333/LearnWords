using Microsoft.EntityFrameworkCore;
using LearnWords.Entities;
using LearnWords.EntityConfiguration;

namespace LearnWords
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DataContext():base()
        {
            
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<BlockOfWords> BlockOfWords { get; set; }
        public DbSet<WordBlock> WordBlocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BlockOfWordsConfiguration());
            modelBuilder.ApplyConfiguration(new WordBlockConfiguration());
            modelBuilder.ApplyConfiguration(new WordConfiguration());
        }
    }
}
