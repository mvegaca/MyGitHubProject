using System;

using MyGitHubProject.ViewModels;

using Windows.UI.Xaml.Controls;

namespace MyGitHubProject.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
