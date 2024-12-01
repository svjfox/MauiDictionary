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

        // Свойства видимости для каждой категории
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

        // Конструктор
        public WordViewModel()
        {
            LearningWords = new ObservableCollection<Word>();
            LearnedWords = new ObservableCollection<Word>();
            RevisionWords = new ObservableCollection<Word>();
            LoadWordsFromFile(); // Загрузка слов из файла при инициализации
        }

        // Добавление слова в соответствующую категорию и сохранение
        public void AddWord(string wordText, string translation, string explanation, string category)
        {
            var word = new Word(wordText, translation, explanation, category);
            switch (category)
            {
                case "Обучение":
                    LearningWords.Add(word);
                    break;
                case "Выучено":
                    LearnedWords.Add(word);
                    break;
                case "Повторение":
                    RevisionWords.Add(word);
                    break;
            }
            SaveWordsToFile(); // Сохранение слов в файл
        }

        // Удаление слова и сохранение изменений
        public void RemoveWord(Word word)
        {
            switch (word.Category)
            {
                case "Обучение":
                    LearningWords.Remove(word);
                    break;
                case "Выучено":
                    LearnedWords.Remove(word);
                    break;
                case "Повторение":
                    RevisionWords.Remove(word);
                    break;
            }
            SaveWordsToFile(); // Сохранение изменений в файл
        }

        // Обновление слова и его перемещение в нужную категорию
        public void UpdateWord(Word word, string newWordText, string newTranslation, string newExplanation, string newCategory)
        {
            RemoveWord(word); // Удаляем слово из старой категории
            word.WordText = newWordText;
            word.Translation = newTranslation;
            word.Explanation = newExplanation;
            word.Category = newCategory;
            AddWord(newWordText, newTranslation, newExplanation, newCategory); // Добавляем в новую категорию
        }

        // Загрузка слов из файла
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

        // Сохранение слов в файл
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

        // Реализация INotifyPropertyChanged для обновления привязок
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}