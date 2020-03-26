using System.Windows;
using System.Windows.Controls;

namespace Wox.Plugin.Shell
{
    public partial class CMDSetting : UserControl
    {
        private readonly Settings _settings;
        private readonly PluginInitContext _context;

        public CMDSetting(Settings settings, PluginInitContext context)
        {
            InitializeComponent();
            _settings = settings;
            _context = context;
        }

        private void CMDSetting_OnLoaded(object sender, RoutedEventArgs re)
        {
            ReplaceWinR.IsChecked = _settings.ReplaceWinR;
            ReplaceStartKey.IsChecked = _settings.ReplaceStartKey;
            LeaveShellOpen.IsChecked = _settings.LeaveShellOpen;
            LeaveShellOpen.IsEnabled = _settings.Shell != Shell.RunCommand;

            LeaveShellOpen.Checked += (o, e) =>
            {
                _settings.LeaveShellOpen = true;
            };

            LeaveShellOpen.Unchecked += (o, e) =>
            {
                _settings.LeaveShellOpen = false;
            };

            ReplaceWinR.Checked += (o, e) =>
            {
                _settings.ReplaceWinR = true;
            };
            ReplaceWinR.Unchecked += (o, e) =>
            {
                _settings.ReplaceWinR = false;
            };

            ReplaceStartKey.Checked += (o, e) =>
            {
                _settings.ReplaceStartKey = true;
                _context.API.SetBuiltinHotkey(false);
                
            };
            ReplaceStartKey.Unchecked += (o, e) =>
            {
                _settings.ReplaceStartKey = false;
                _context.API.SetBuiltinHotkey(true);
            };

            ShellComboBox.SelectedIndex = (int) _settings.Shell;
            ShellComboBox.SelectionChanged += (o, e) =>
            {
                _settings.Shell = (Shell) ShellComboBox.SelectedIndex;
                LeaveShellOpen.IsEnabled = _settings.Shell != Shell.RunCommand;
            };
        }
    }
}
