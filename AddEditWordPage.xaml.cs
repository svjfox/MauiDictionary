using Microsoft.Maui.Controls;

namespace MauiDictionary
{
    public partial class AddEditWordPage : ContentPage
    {
        private WordViewModel viewModel;
        private Word word;

        public AddEditWordPage(WordViewModel viewModel, Word word)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.word = word;

            if (word != null)
            {
                WordEntry.Text = word.WordText;
                TranslationEntry.Text = word.Translation;
                ExplanationEntry.Text = word.Explanation;
                CategoryPicker.SelectedItem = word.Category; 
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            string wordText = WordEntry.Text;
            string translation = TranslationEntry.Text;
            string explanation = ExplanationEntry.Text;
            string category = CategoryPicker.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(wordText) || string.IsNullOrWhiteSpace(translation))
            {
                await DisplayAlert("Ошибка", "Слово и перевод не могут быть пустыми.", "OK");
                return;
            }

            if (word == null)
            {
                
                viewModel.AddWord(wordText, translation, explanation, category);
            }
            else
            {
                
                viewModel.UpdateWord(word, wordText, translation, explanation, category);
            }

            await Navigation.PopAsync();
        }

    }
}
