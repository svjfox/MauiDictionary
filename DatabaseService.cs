using SQLite;
using System.IO;

namespace MauiDictionary
{
    public class DatabaseService
    {
        private SQLiteConnection _connection;

        
        public DatabaseService()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "words.db3");
            _connection = new SQLiteConnection(databasePath);
            _connection.CreateTable<Word>();  
        }

        
        public void InsertWord(Word word)
        {
            _connection.Insert(word);
        }

        
        public List<Word> GetAllWords()
        {
            return _connection.Table<Word>().ToList();
        }

        
        public void DeleteWord(Word word)
        {
            _connection.Delete(word);
        }
    }
}
