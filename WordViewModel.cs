using SQLite;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MauiDictionary
{
    public class WordViewModel : INotifyPropertyChanged
    {
        private SQLiteAsyncConnection _database;

        public ObservableCollection<Word> LearningWords { get; set; }
        public ObservableCollection<Word> LearnedWords { get; set; }
        public ObservableCollection<Word> RevisionWords { get; set; }

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

        public WordViewModel()
        {
            LearningWords = new ObservableCollection<Word>();
            LearnedWords = new ObservableCollection<Word>();
            RevisionWords = new ObservableCollection<Word>();
            InitializeDatabase(); 
        }

        private async void InitializeDatabase()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dictionary.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            await _database.CreateTableAsync<Word>(); 
            await LoadWordsFromDatabase(); 
        }

        public async Task LoadWordsFromDatabase()
        {
            var words = await _database.Table<Word>().ToListAsync();
            foreach (var word in words)
            {
                AddWordToCategory(word);
            }
        }

        private void AddWordToCategory(Word word)
        {
            switch (word.Category)
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
        }

        public async void AddWord(string wordText, string translation, string explanation, string category)
        {
            var word = new Word(wordText, translation, explanation, category);
            await _database.InsertAsync(word); 
            AddWordToCategory(word); 
        }

        public async void RemoveWord(Word word)
        {
            await _database.DeleteAsync(word); 
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
        }

        public async void UpdateWord(Word word, string newWordText, string newTranslation, string newExplanation, string newCategory)
        {
            word.WordText = newWordText;
            word.Translation = newTranslation;
            word.Explanation = newExplanation;
            word.Category = newCategory;

            await _database.UpdateAsync(word); 
            ReloadWords();
        }

        private async void ReloadWords()
        {
            
            LearningWords.Clear();
            LearnedWords.Clear();
            RevisionWords.Clear();
            await LoadWordsFromDatabase(); 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
