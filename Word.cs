namespace MauiDictionary
{
    public class Word
    {
        public string WordText { get; set; } // Слово
        public string Translation { get; set; } // Перевод
        public string Explanation { get; set; } // Объяснение
        public string Category { get; set; } // Категории

        public Word(string wordText, string translation, string explanation, string category)
        {
            WordText = wordText;
            Translation = translation;
            Explanation = explanation;
            Category = category;
        }
    }
}