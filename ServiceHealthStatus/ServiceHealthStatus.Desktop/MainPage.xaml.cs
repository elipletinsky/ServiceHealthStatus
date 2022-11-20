using ServiceHealthStatus.ViewModel;

namespace ServiceHealthStatus.Desktop
{
    public partial class MainPage
    {
        private readonly MainViewModel _vm;
        int count = 0;
        

        public MainPage(MainViewModel vm)
        {
            _vm = vm;
            BindingContext = _vm;       
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, EventArgs e)
        {
            _vm.ModelFilePath = @"j:\temp\CatalogServiceModel1.json";
            await _vm.Populate();
        }

       
    }
}