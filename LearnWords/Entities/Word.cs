namespace LearnWords.Entities
{
    public class Word
    {
        public long Id { get; set; }
        public byte[] Version { get; set; }
        public string Russian { get; set; }
        public string English { get; set; }
        public bool IsNew { get; set; } = true;
        public bool Learned { get; set; }
        public PartsOfSpeech PartOfSpeech { get; set; }
        public bool IsLearnedInBlock { get; set; }
    }
}
