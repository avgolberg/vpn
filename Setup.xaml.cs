using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

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
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 6) this.Close();

            if ((pageNumber == 1 || pageNumber == 2) && browseTextBox.Text.Length == 0) return;

            pageNumber++;
            ShowPage();

            if (pageNumber == 5)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork += worker_DoWork;
                worker.ProgressChanged += worker_ProgressChanged;
                worker.RunWorkerAsync();
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            pageNumber--;
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
                    backButton.Visibility = Visibility.Visible;
                    browse.Visibility = Visibility.Visible;
                    if (path != null && path.Length != 0) browseTextBox.Text = path;
                    freeSpaceInfo.Visibility = Visibility.Visible;
                    mainInfoImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/folder_icon.png"));

                    agreementTextBox.Visibility = Visibility.Collapsed;
                    radios.Visibility = Visibility.Collapsed;
                    additionalShortcuts.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    mainLabel.Text = "Select Start Menu Folder";
                    secondLabel.Text = "Where should Setup place the program's shortcuts?";
                    mainInfo.Visibility = Visibility.Visible;
                    mainInfoImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/start_folder_icon.ico"));
                    mainText.Text = "To continue, click Next. If you would like to select a different folder, click Browse.";
                    backButton.Visibility = Visibility.Visible;
                    browse.Visibility = Visibility.Visible;
                    browseTextBox.Text = "VPN Unlimited";
                    mainInfoText.Text = "Setup will create the program's shortcuts in the following Start Menu folder.";

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
                    additionalShortcuts.Visibility = Visibility.Visible;
                    nextButton.Content = "Next";

                    mainInfo.Visibility = Visibility.Collapsed;
                    browse.Visibility = Visibility.Collapsed;
                    finalSettings.Visibility = Visibility.Collapsed;
                    break;
                case 4:
                    mainLabel.Text = "Ready to Install";
                    secondLabel.Text = "Setup is now ready to begin installing VPN Unlimited on your computer.";
                    mainText.Text = @"Click Install to continue with the installation, or click Back if you want to review or
change any settings.";
                    finalSettings.Visibility = Visibility.Visible;
                    finalSettings.Text = "Destination location:\n\t" + path + "\n\nStart Menu folder:\n\t" + browseTextBox.Text + "\n\nAdditional tasks:\n\t" + (desktopShortcut ? "Additional shortcuts:\n\t   Create a desktop shortcut" : "-");
                    additionalShortcuts.Visibility = Visibility.Collapsed;
                    nextButton.Content = "Install";
                    break;
                case 5:
                    mainLabel.Text = "Installing";
                    secondLabel.Text = "Please wait while Setup installs VPN Unlimited on your computer.";
                    mainText.Text = "Extracting files...";
                    pbStatus.Visibility = Visibility.Visible;

                    finalSettings.Visibility = Visibility.Collapsed;
                    backButton.Visibility = Visibility.Collapsed;
                    nextButton.Visibility = Visibility.Collapsed;
                    break;
                case 6:
                    completeGrid.Visibility = Visibility.Visible;
                    nextButton.Content = "Finish";

                    rect1.Visibility = Visibility.Hidden;
                    border1.Visibility = Visibility.Hidden;
                    pbStatus.Visibility = Visibility.Collapsed;
                    cancelButton.Visibility = Visibility.Hidden;
                    logo.Visibility = Visibility.Collapsed;
                    mainLabel.Visibility = Visibility.Collapsed;
                    secondLabel.Visibility = Visibility.Collapsed;
                    mainText.Visibility = Visibility.Collapsed;
                    nextButton.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            nextButton.IsEnabled = true;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            nextButton.IsEnabled = false;
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

        private void desktopShortcutCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)desktopShortcutCheckBox.IsChecked) desktopShortcut = true;
            else desktopShortcut = false;
        }

        private void pbStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (pbStatus.Value == 100)
            {
                pageNumber++;
                ShowPage();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 101; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(100);
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (pageNumber != 6)
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
        }      
    }
}
