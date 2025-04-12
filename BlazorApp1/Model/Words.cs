using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp1.Model
{
    public class Words
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("englishword")]
        public string EnglishWord { get; set; }

        [Column("portuguesetranslation")]
        public string PortugueseTranslation { get; set; }
    }
}
