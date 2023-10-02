using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
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
            DataContext = _mainViewModel;
            InitializeComponent();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _mainViewModel.Populate();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "All Files (*.*)|*.*";
            dlg.Multiselect = false;
            var result = dlg.ShowDialog();

            if (result == true)
            {
                var stream = dlg.OpenFile(); 
                var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                _mainViewModel.ModelFileContent = json;
                await _mainViewModel.Populate();
            }
        }
    }
}
