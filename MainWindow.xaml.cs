using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VPN
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Create a GeoMap control
            LiveCharts.Wpf.GeoMap geoMap1 = new LiveCharts.Wpf.GeoMap();

            // Create a dictionary that we'll fill with Random Data in this example
            Random r = new Random();

            // Note that we define the "key" and number pattern, where the key is the
            // ID of the element in the XML where you want to define the numeric value.
            Dictionary<string, double> values = new Dictionary<string, double>();

            // Fill the specific keys of the countries with a random number
            //values["MX"] = r.Next(0, 100);

            geoMap1.HeatMap = values;
            geoMap1.Source = $"{(new FileInfo(AppDomain.CurrentDomain.BaseDirectory)).Directory.Parent.Parent.FullName}\\World.xml";
            geoMap1.LandStrokeThickness = 0;
            geoMap1.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#dde8fa");
            // Important, you can only add the control to the form after defining the source of the map,
            // otherwise it will throw a File Not Found exception
            MainGrid.Children.Add(geoMap1);
        }
    }
}
