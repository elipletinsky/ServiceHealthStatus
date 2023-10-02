namespace ServiceHealthStatus.ViewModel.Model

{
    public class ServiceItemBase : IResultPatternHolderModel
    {
        public string ResultPattern { get; set; }
        public string Name { get; set; }
    }
}
