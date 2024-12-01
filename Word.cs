namespace MauiDictionary
{
    public class Word
    {
        public string WordText { get; set; } // �����
        public string Translation { get; set; } // �������
        public string Explanation { get; set; } // ����������
        public string Category { get; set; } // ���������

        public Word(string wordText, string translation, string explanation, string category)
        {
            WordText = wordText;
            Translation = translation;
            Explanation = explanation;
            Category = category;
        }
    }
}