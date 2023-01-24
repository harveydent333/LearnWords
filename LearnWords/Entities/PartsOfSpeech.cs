using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LearnWords.Entities
{
    /// <summary>
    /// Части речи
    /// </summary>
    public enum PartsOfSpeech
    {
        /// <summary>
        /// Глагол
        /// </summary>
        [Display(Name = "Verb")]
        Verb,

        /// <summary>
        /// Прилагательное
        /// </summary>
        [Display(Name = "Adjective")]
        Adjective,

        /// <summary>
        /// Наречие
        /// </summary>
        [Display(Name = "AdVerb")]
        AdVerb,

        /// <summary>
        /// Существительное
        /// </summary>
        [Display(Name = "Noun")]
        Noun,

        /// <summary>
        /// Предлог
        /// </summary>
        [Display(Name = "Preposition")]
        Preposition,

        /// <summary>
        /// Числительное 
        /// </summary>
        [Display(Name = "Numeral")]
        Numeral,

        /// <summary>
        /// Местоимение
        /// </summary>
        [Display(Name = "Pronoun")]
        Pronoun,

        /// <summary>
        /// Артикль
        /// </summary>
        [Display(Name = "Article")]
        Article,

        /// <summary>
        /// Союз
        /// </summary>
        [Display(Name = "Conjunction")]
        Conjunction,

        /// <summary>
        /// Идиома
        /// </summary>
        [Display(Name = "Idiom")]
        Idiom,

        [Display(Name = "Irregular Verb")]
        IrregularVerb,

        [Display(Name = "Phrasal Verb")]
        PhrasalVerb
    }
}
