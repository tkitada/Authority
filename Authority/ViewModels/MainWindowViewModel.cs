using Authority.Model.ApplicationService;
using Authority.Model.Domain.Entity;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Authority.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string title_ = "Authority Manager";
        private ObservableCollection<AuthorityModel> authorityData_;
        private int selectedIndex_ = -1;
        private string programName_;
        private string pcUser_;
        private int? controlFlg1Value_;
        private int? controlFlg2Value_;

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

        public int SelectedIndex
        {
            get => selectedIndex_;
            set
            {
                SetProperty(ref selectedIndex_, value);
                DisplayData(value);
            }
        }

        public string ProgramName
        {
            get => programName_;
            set => SetProperty(ref programName_, value);
        }

        public string PCUser
        {
            get => pcUser_;
            set => SetProperty(ref pcUser_, value);
        }

        private string controlFlg1Name_;

        public string ControlFlg1Name
        {
            get => controlFlg1Name_;
            set => SetProperty(ref controlFlg1Name_, value);
        }

        public int? ControlFlg1Value
        {
            get => controlFlg1Value_;
            set => SetProperty(ref controlFlg1Value_, value);
        }

        private string controlFlg2Name_;

        public string ControlFlg2Name
        {
            get => controlFlg2Name_;
            set => SetProperty(ref controlFlg2Name_, value);
        }

        public int? ControlFlg2Value
        {
            get => controlFlg2Value_;
            set => SetProperty(ref controlFlg2Value_, value);
        }

        private DelegateCommand updateCommand_;

        public DelegateCommand UpdateCommand =>
            updateCommand_ ?? (updateCommand_ = new DelegateCommand(ExecuteUpdateCommand));

        private readonly AuthorityApplicationService appService_ = new AuthorityApplicationService();

        public MainWindowViewModel()
        {
            UpdateDataGrid();
        }

        public void DisplayData(int row)
        {
            if (row < 0) return;

            ProgramName = AuthorityData[row].ProgramName;
            PCUser = AuthorityData[row].PCUser;
            ControlFlg1Value = AuthorityData[row].ControlFlg1;
            ControlFlg2Value = AuthorityData[row].ControlFlg2;

            var detail = appService_.GetDetail(ProgramName);
            if (detail != null)
            {
                ControlFlg1Name = detail.ControlFlg1;
                ControlFlg2Name = detail.ControlFlg2;
            }
            else
            {
                ControlFlg1Name = "";
                ControlFlg2Name = "";
            }
        }

        private void UpdateDataGrid()
        {
            var selectedIndex = SelectedIndex;
            AuthorityData = new ObservableCollection<AuthorityModel>(appService_.GetAllAuthorities());
            SelectedIndex = selectedIndex;
        }

        private void ExecuteUpdateCommand()
        {
            if (SelectedIndex < 0) return;

            var beforeAuthority = AuthorityData[SelectedIndex];
            var afterAuthority = new AuthorityModel
            {
                ProgramName = ProgramName,
                PCUser = PCUser,
                ControlFlg1 = ControlFlg1Value,
                ControlFlg2 = ControlFlg2Value
            };
            appService_.UpdateAuthority(beforeAuthority, afterAuthority);
            UpdateDataGrid();
        }
    }
}