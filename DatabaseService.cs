using SQLite;
using System.IO;

namespace MauiDictionary
{
    public class DatabaseService
    {
        private SQLiteConnection _connection;

        // Конструктор для инициализации подключения к базе данных
        public DatabaseService()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "words.db3");
            _connection = new SQLiteConnection(databasePath);
            _connection.CreateTable<Word>();  // Создаем таблицу, если она еще не существует
        }

        // Вставка нового слова в базу данных
        public void InsertWord(Word word)
        {
            _connection.Insert(word);
        }

        // Получение всех слов из базы данных
        public List<Word> GetAllWords()
        {
            return _connection.Table<Word>().ToList();
        }

        // Удаление слова из базы данных
        public void DeleteWord(Word word)
        {
            _connection.Delete(word);
        }
    }
}
