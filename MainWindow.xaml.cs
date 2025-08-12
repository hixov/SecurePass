using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace SecurePassWPF
{
    public partial class MainWindow : Window
    {
        public class Credential
        {
            public string Service { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private List<Credential> _credentials = new List<Credential>();
        private string _vaultFilePath;
        private string _masterPassword;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded; 
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            if (!ShowLoginDialog())
            {
                Application.Current.Shutdown(); 
                return;
            }
            this.Show();
            RefreshServiceList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CredentialDialog();
            if (dialog.ShowDialog() == true)
            {
                _credentials.Add(dialog.NewCredential);
                SaveChanges();
                RefreshServiceList();
                ServiceListBox.SelectedItem = dialog.NewCredential.Service;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceListBox.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите сервис для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var serviceToRemove = ServiceListBox.SelectedItem.ToString();
            var result = MessageBox.Show($"Вы уверены, что хотите удалить запись для '{serviceToRemove}'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _credentials.RemoveAll(c => c.Service == serviceToRemove);
                SaveChanges();
                RefreshServiceList();
                ClearDetails();
            }
        }

        private void ServiceListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceListBox.SelectedItem == null)
            {
                ClearDetails();
                return;
            }

            var selectedService = ServiceListBox.SelectedItem.ToString();
            var cred = _credentials.FirstOrDefault(c => c.Service == selectedService);
            if (cred != null)
            {
                ServiceTextBox.Text = cred.Service;
                UsernameTextBox.Text = cred.Username;
                PasswordBox.Password = cred.Password;
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordBox.Password))
            {
                Clipboard.SetText(PasswordBox.Password);
                StatusTextBlock.Text = "Пароль скопирован в буфер обмена!";
            }
        }


        private bool ShowLoginDialog()
        {
            var dialog = new LoginWindow();
            if (dialog.ShowDialog() == true)
            {
                _vaultFilePath = dialog.VaultFilePath;
                _masterPassword = dialog.MasterPassword;
                LoadVault();
                return true;
            }
            return false;
        }

        private void RefreshServiceList()
        {
            var selected = ServiceListBox.SelectedItem?.ToString();
            ServiceListBox.Items.Clear();
            var sortedCreds = _credentials.OrderBy(c => c.Service).ToList();
            foreach (var cred in sortedCreds)
            {
                ServiceListBox.Items.Add(cred.Service);
            }

            if (selected != null)
            {
                ServiceListBox.SelectedItem = selected;
            }
        }

        private void ClearDetails()
        {
            ServiceTextBox.Clear();
            UsernameTextBox.Clear();
            PasswordBox.Clear();
        }

        private void LoadVault()
        {
            try
            {
                if (File.Exists(_vaultFilePath))
                {
                    byte[] encryptedData = File.ReadAllBytes(_vaultFilePath);
                    string json = Crypto.Decrypt(encryptedData, _masterPassword);
                    _credentials = JsonSerializer.Deserialize<List<Credential>>(json);
                }
                else
                {
                    _credentials = new List<Credential>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки хранилища: {ex.Message}. Неверный пароль или файл поврежден.", "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void SaveChanges()
        {
            try
            {
                string json = JsonSerializer.Serialize(_credentials);
                byte[] encryptedData = Crypto.Encrypt(json, _masterPassword);
                File.WriteAllBytes(_vaultFilePath, encryptedData);
                StatusTextBlock.Text = "Изменения сохранены.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}