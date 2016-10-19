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

namespace Bashe
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();

            var data = Core._data;
            tThingsCount.Text = data.ThingsCount.ToString();
            tTimeout.Text = (data.Timeout).ToString();
            

            if (data.GameMode == GameMode.AI)
            {
                rbAI.IsChecked = true;
            }
            else
            {
                rbHuman.IsChecked = true;
            }
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OK_Onlick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            var newData = new Data()
            {
                GameMode = (rbAI.IsChecked == true) ? GameMode.AI : GameMode.Human,
                ThingsCount = Convert.ToInt32(tThingsCount.Text),
                Timeout = Convert.ToInt32(tTimeout.Text)
            };

            Core._data.GameMode = newData.GameMode;
            Core._data.ThingsCount = newData.ThingsCount;
            Core._data.Timeout = newData.Timeout;

            Close();
        }
    }

}
