using System.Net;
using System.Windows;
using static SecurePassWPF.MainWindow;

namespace SecurePassWPF
{
    public partial class CredentialDialog : Window
    {
        public Credential NewCredential { get; private set; }

        public CredentialDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ServiceTextBox.Text))
            {
                MessageBox.Show("Имя сервиса не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewCredential = new Credential
            {
                Service = ServiceTextBox.Text,
                Username = UsernameTextBox.Text,
                Password = PasswordTextBox.Text
            };

            DialogResult = true;
        }
    }
}