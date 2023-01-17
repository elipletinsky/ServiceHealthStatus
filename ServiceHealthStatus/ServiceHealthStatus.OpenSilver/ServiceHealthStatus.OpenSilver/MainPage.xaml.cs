using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Resources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceHealthStatus.ViewModel;

namespace ServiceHealthStatus.OpenSilver
{
    public partial class MainPage : Page
    {
        private readonly Microsoft.Extensions.DependencyInjection.ServiceProvider _services;
        private readonly MainViewModel _mainViewModel;
        public MainPage()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureServices();
            _services = serviceCollection.BuildServiceProvider();
            _mainViewModel = _services.GetRequiredService<MainViewModel>();

            DataContext = _mainViewModel;

            this.InitializeComponent();

            Loaded += MainWindow_Loaded;
        }


        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var query = global::OpenSilver.Interop.ExecuteJavaScript("window.location.search").ToString();
            if (!string.IsNullOrEmpty(query))
            {
                var args = HttpUtility.ParseQueryString(query);
                var uri = args["config"];
                uri = HttpUtility.UrlDecode(uri);

                _mainViewModel.ModelFilePath = uri;
                await _mainViewModel.Populate();
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new global::OpenSilver.Controls.OpenFileDialog();
            dlg.Filter = "All Files (*.*)|*.*";
            dlg.Multiselect = false;
            var result = await dlg.ShowDialogAsync();

            if (result == true)
            {
                var stream = dlg.File.OpenRead();
                var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                  _mainViewModel.ModelFileContent = json;
                await _mainViewModel.Populate();
                Console.WriteLine(_mainViewModel.Children.Count.ToString());
            }
        }
    }
}
