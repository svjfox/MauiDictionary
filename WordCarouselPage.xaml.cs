namespace MauiDictionary
{
    public partial class WordCarouselPage : ContentPage
    {
        private WordViewModel viewModel;

        public WordCarouselPage()
        {
            InitializeComponent();
            viewModel = new WordViewModel();
            BindingContext = viewModel;
        }

        private async void OnAddWordClicked(object sender, EventArgs e)
        {
            // Переход на страницу добавления/редактирования слова
            await Navigation.PushAsync(new AddEditWordPage(viewModel, null));
        }

        private async void OnRemoveWordClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var word = (Word)button.CommandParameter;
            viewModel.RemoveWord(word);
        }

        private async void OnEditWordClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var word = (Word)button.CommandParameter;
            await Navigation.PushAsync(new AddEditWordPage(viewModel, word));
        }
    }
}
