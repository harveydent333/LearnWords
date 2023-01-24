using LearnWords.Entities;

namespace LearnWords.Models
{
    public class BlockWordsModel
    {
        public long Id { get; set; }

        public List<Word> Words { get; set; }
        public DateTime Created { get; set; }
        public short RepeatCount { get; set; }
        public DateTime NextRepeat { get; set; }
    }
}
