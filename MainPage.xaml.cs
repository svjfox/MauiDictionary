namespace MauiDictionary
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartButtonClicked(object sender, EventArgs e)
        {
            // Переход к странице изучения слов
            await Navigation.PushAsync(new WordCarouselPage());
        }
    }
}
