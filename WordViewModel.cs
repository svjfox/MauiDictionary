using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace MauiDictionary
{
    public class WordViewModel : INotifyPropertyChanged
    {
        
        public ObservableCollection<Word> LearningWords { get; set; }
        public ObservableCollection<Word> LearnedWords { get; set; }
        public ObservableCollection<Word> RevisionWords { get; set; }

        // �������� ��������� ��� ������ ���������
        private bool _isLearningVisible = true;
        public bool IsLearningVisible
        {
            get => _isLearningVisible;
            set
            {
                _isLearningVisible = value;
                OnPropertyChanged(nameof(IsLearningVisible));
            }
        }

        private bool _isLearnedVisible = true;
        public bool IsLearnedVisible
        {
            get => _isLearnedVisible;
            set
            {
                _isLearnedVisible = value;
                OnPropertyChanged(nameof(IsLearnedVisible));
            }
        }

        private bool _isRevisionVisible = true;
        public bool IsRevisionVisible
        {
            get => _isRevisionVisible;
            set
            {
                _isRevisionVisible = value;
                OnPropertyChanged(nameof(IsRevisionVisible));
            }
        }

        // �����������
        public WordViewModel()
        {
            LearningWords = new ObservableCollection<Word>();
            LearnedWords = new ObservableCollection<Word>();
            RevisionWords = new ObservableCollection<Word>();
            LoadWordsFromFile(); // �������� ���� �� ����� ��� �������������
        }

        // ���������� ����� � ��������������� ��������� � ����������
        public void AddWord(string wordText, string translation, string explanation, string category)
        {
            var word = new Word(wordText, translation, explanation, category);
            switch (category)
            {
                case "��������":
                    LearningWords.Add(word);
                    break;
                case "�������":
                    LearnedWords.Add(word);
                    break;
                case "����������":
                    RevisionWords.Add(word);
                    break;
            }
            SaveWordsToFile(); // ���������� ���� � ����
        }

        // �������� ����� � ���������� ���������
        public void RemoveWord(Word word)
        {
            switch (word.Category)
            {
                case "��������":
                    LearningWords.Remove(word);
                    break;
                case "�������":
                    LearnedWords.Remove(word);
                    break;
                case "����������":
                    RevisionWords.Remove(word);
                    break;
            }
            SaveWordsToFile(); // ���������� ��������� � ����
        }

        // ���������� ����� � ��� ����������� � ������ ���������
        public void UpdateWord(Word word, string newWordText, string newTranslation, string newExplanation, string newCategory)
        {
            RemoveWord(word); // ������� ����� �� ������ ���������
            word.WordText = newWordText;
            word.Translation = newTranslation;
            word.Explanation = newExplanation;
            word.Category = newCategory;
            AddWord(newWordText, newTranslation, newExplanation, newCategory); // ��������� � ����� ���������
        }

        // �������� ���� �� �����
        private void LoadWordsFromFile()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "words.txt");
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(';');
                        if (parts.Length == 4)
                        {
                            AddWord(parts[0], parts[1], parts[2], parts[3]);
                        }
                    }
                }
            }
        }

        // ���������� ���� � ����
        private void SaveWordsToFile()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "words.txt");
            using (var writer = new StreamWriter(path))
            {
                foreach (var word in LearningWords)
                {
                    writer.WriteLine($"{word.WordText};{word.Translation};{word.Explanation};{word.Category}");
                }
                foreach (var word in LearnedWords)
                {
                    writer.WriteLine($"{word.WordText};{word.Translation};{word.Explanation};{word.Category}");
                }
                foreach (var word in RevisionWords)
                {
                    writer.WriteLine($"{word.WordText};{word.Translation};{word.Explanation};{word.Category}");
                }
            }
        }

        // ���������� INotifyPropertyChanged ��� ���������� ��������
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}