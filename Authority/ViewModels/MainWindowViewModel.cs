using Authority.Model.ApplicationService;
using Authority.Model.Domain.Entity;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Authority.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string title_ = "Authority Manager";
        private ObservableCollection<AuthorityModel> authorityData_;

        public string Title
        {
            get => title_;
            set => SetProperty(ref title_, value);
        }

        public ObservableCollection<AuthorityModel> AuthorityData
        {
            get => authorityData_;
            set => SetProperty(ref authorityData_, value);
        }

        private readonly AuthorityApplicationService appService_ = new AuthorityApplicationService();

        public MainWindowViewModel()
        {
            AuthorityData = new ObservableCollection<AuthorityModel>(appService_.GetAllAuthorities());
        }
    }
}