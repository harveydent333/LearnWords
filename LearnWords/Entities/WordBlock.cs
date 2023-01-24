namespace LearnWords.Entities
{
    public class WordBlock
    {
        public long Id { get; set; }
        public byte[] Version { get; set; }
        public long BlockId { get; set; }
        public long WordId { get; set; }

        public virtual BlockOfWords BlockOfWords { get; set; }
        public virtual Word Word { get; set; }
    }
}
