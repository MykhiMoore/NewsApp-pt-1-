using NewsApp.Models;
using NewsApp.Services;

namespace NewsApp.Pages;

public partial class NewsPage : ContentPage
{
    private bool IsNextPage = false;
    public List<Article> ArticlesList { get; set; }
    public List<Category> CategorysList = new List<Category>()
    {
    new Category(){Name = "breaking-news"},
    new Category(){Name = "world"},
    new Category(){Name = "nation"},
    new Category(){Name = "business"},
    new Category(){Name = "technology"},
    new Category(){Name = "entertainment"},
    new Category(){Name = "sports"},
    new Category(){Name = "science"},
    new Category(){Name = "health"},
    };
    public NewsPage()
	{
		InitializeComponent();
        ArticlesList = new List<Article>();
        CvCategories.ItemsSource = CategorysList;

	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (IsNextPage == false)
        {
            await PassCategory("breaking-news");
        }
    }

    public async Task PassCategory(string categoryName)
    {
        CvNews.ItemsSource = null;
        ArticlesList.Clear();
        ApiService apiService = new ApiService();
        var newsresult = await apiService.GetNews(categoryName);
        foreach (var item in newsresult.Articles)
        {
            ArticlesList.Add(item);
        }
        CvNews.ItemsSource = ArticlesList;
    }
        private async void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = e.CurrentSelection.FirstOrDefault() as Category;
            await PassCategory(selectedItem.Name); 
        }
    private async void CvNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as Article;
        IsNextPage = true;
        await Navigation.PushAsync(new NewsDetailPage(selectedItem));
    }
}