using CsvHelper.Configuration.Attributes;

namespace English.Model.CERF
{
    public class CERF
    {
        [Ignore]
        public int Id {  get; set; }
        public string Word { get; set; }
        public string CEFR { get; set; }

    }
}
