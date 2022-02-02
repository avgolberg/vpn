using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VPN
{
    /// <summary>
    /// Логика взаимодействия для Setup.xaml
    /// </summary>
    public partial class Setup : Window
    {
        private int pageNumber;
        private bool desktopShortcut;
        private string path;
        public Setup()
        {
            InitializeComponent();
            pageNumber = 0;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            nextButton.IsEnabled = true;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            nextButton.IsEnabled = false;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(@"Setup is not complete. If you exit now, the program will not be
installed.

You may run Setup again at another time to complete the installation.

Exit Setup?", "Exit Setup", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.No:
                    e.Cancel = true;
                    break;
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            pageNumber++;
            ShowPage();
        }

        private void ShowPage()
        {
            switch (pageNumber)
            {
                case 0:
                    mainLabel.Text = "License Agreement";
                    secondLabel.Text = "Please read the following important information before continuing.";
                    mainText.Text = @"Please read the following License Agreement. You must accept the terms of this,
agreement before continuing with the installation.";
                    agreementTextBox.Visibility = Visibility.Visible;
                    radios.Visibility = Visibility.Visible;
                    nextButton.Content = "Next";

                    backButton.Visibility = Visibility.Collapsed;
                    mainInfo.Visibility = Visibility.Collapsed;
                    additionalShortcuts.Visibility = Visibility.Collapsed;
                    browse.Visibility = Visibility.Collapsed;
                    freeSpaceInfo.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    mainLabel.Text = "Select Destination Location";
                    secondLabel.Text = "Where should VPN Unlimited be installed?";
                    mainInfo.Visibility = Visibility.Visible;
                    mainText.Text = "To continue, click Next. IF you would like to select a different folder, click Browse.";
                    nextButton.Content = "Next";
                    backButton.Visibility = Visibility.Visible;
                    browse.Visibility = Visibility.Visible;
                    freeSpaceInfo.Visibility = Visibility.Visible;

                    agreementTextBox.Visibility = Visibility.Collapsed;
                    radios.Visibility = Visibility.Collapsed;
                    additionalShortcuts.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    mainLabel.Text = "Select Start Menu Folder";
                    secondLabel.Text = "Where should Setup place the program's shortcuts?";
                    mainInfo.Visibility = Visibility.Visible;
                    mainInfoImage.Source = new BitmapImage(new Uri(@"Images/folder_icon.png"));
                    mainText.Text = "To continue, click Next. If you would like to select a different folder, click Browse.";
                    nextButton.Content = "Next";
                    backButton.Visibility = Visibility.Visible;
                    browse.Visibility = Visibility.Visible;

                    freeSpaceInfo.Visibility = Visibility.Collapsed;
                    agreementTextBox.Visibility = Visibility.Collapsed;
                    radios.Visibility = Visibility.Collapsed;
                    additionalShortcuts.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    mainLabel.Text = "Select Additional Tasks";
                    secondLabel.Text = "Which additional tasks should be performed?";
                    mainText.Text = @"Select the additional tasks you would like Setup to perform while installing VPN
Unlimited, then click Next.";
                    agreementTextBox.Visibility = Visibility.Collapsed;
                    radios.Visibility = Visibility.Collapsed;
                    additionalShortcuts.Visibility = Visibility.Visible;
                    nextButton.Content = "Next";
                    break;
                case 4:
                    mainLabel.Text = "Ready to Install";
                    secondLabel.Text = "Setup is now ready to begin installing VPN Unlimited on your computer.";
                    mainText.Text = "Click Install to continue with the installation.";
                    additionalShortcuts.Visibility = Visibility.Collapsed;
                    nextButton.Content = "Install";
                    break;
            }

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            pageNumber--;
            ShowPage();
        }

        private void desktopShortcutCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)desktopShortcutCheckBox.IsChecked) desktopShortcut = true;
            else desktopShortcut = false;
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                browseTextBox.Text = openFileDlg.SelectedPath;
                path = openFileDlg.SelectedPath;
            }
        }
    }
}
