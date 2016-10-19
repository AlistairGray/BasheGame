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

namespace Bashe_New
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {

        public Settings()
        {
            InitializeComponent();

            tThingsCount.Text = Data.ItemsCount.ToString();
            tTimeout.Text = (Data.TurnInterval).ToString();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OK_Onlick(object sender, RoutedEventArgs e)
        {
            Data.ItemsCount = int.Parse(tThingsCount.Text);
            Data.TurnInterval = int.Parse(tTimeout.Text);

            Close();
        }
    }
}
