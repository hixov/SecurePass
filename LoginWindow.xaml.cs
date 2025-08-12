using Microsoft.Win32;
using System.Windows;

namespace SecurePassWPF
{
    public partial class LoginWindow : Window
    {
        public string VaultFilePath { get; private set; }
        public string MasterPassword { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilePathTextBox.Text) || FilePathTextBox.Text == "Выберите или создайте файл хранилища...")
            {
                MessageBox.Show("Пожалуйста, выберите файл хранилища.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Пожалуйста, введите мастер-пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            VaultFilePath = FilePathTextBox.Text;
            MasterPassword = PasswordBox.Password;
            DialogResult = true;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Выберите или создайте файл хранилища",
                Filter = "SecurePass Vault (*.spv)|*.spv",
                DefaultExt = "spv"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = saveFileDialog.FileName;
            }
        }
    }
}