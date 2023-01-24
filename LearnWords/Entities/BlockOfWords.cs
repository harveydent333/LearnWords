namespace LearnWords.Entities
{
    public class BlockOfWords
    {
        public long Id { get; set; }
        public byte[] Version { get; set; }
        public short RepeatCount { get; set; }
        public DateTime Created { get; set; }
        public DateTime NextRepeat { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<WordBlock> Words { get; set; }

        public BlockOfWords()
        {
            Words = new HashSet<WordBlock>();
        }
    }
}
