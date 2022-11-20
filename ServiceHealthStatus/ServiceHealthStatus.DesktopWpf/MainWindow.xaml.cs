using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceHealthStatus.ViewModel;

namespace ServiceHealthStatus.DesktopWpf
{
    public partial class MainWindow : Window
    {
        private readonly ServiceProvider _services;
        private readonly MainViewModel _mainViewModel;
        public MainWindow()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureServices();
            _services = serviceCollection.BuildServiceProvider();

            _mainViewModel = _services.GetRequiredService<MainViewModel>();
            _mainViewModel.ModelFilePath = @"j:\temp\CatalogServiceModel1.json";
            DataContext = _mainViewModel;

            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _mainViewModel.Populate();
        }
    }
}
