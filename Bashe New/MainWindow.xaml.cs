using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Timers;

namespace Bashe_New
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            rbAI.IsChecked = true;
            EnableDisableButtons();
            timer.Interval = 1;
            timer.Elapsed += Timer_Elapsed;
            // Запуск менеджера отрисовки
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(Update);
        }

        void Update()
        {
            DrawItems();
            gbPlayer.Header = Data.CurrentPlayer == Player.One ? "Игрок 1" : "Игрок 2";
        }

        Core core = new Core();

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            // Деактивация окна
            IsEnabled = false;
            // Создаём окно настроек
            var settingsWindow = new Settings();
            // Вызываем окно настроек и узнаём выбор пользователя ОК или отмена
            settingsWindow.ShowDialog();
            IsEnabled = true;
        }

        private void NewGame()
        {

            // Рисует предметы
            DrawItems();

            core.ChangeGameMode(rbAI.IsChecked.Value ? GameMode.Ai : GameMode.Human);
            bPlayPause.Content = "Пауза";
            core.GameEnded+= CoreOnGameEnded;
            miSettings.IsEnabled = !miSettings.IsEnabled;

            // Переводит игру в статус Playing
            core.Start();
        }

        private void CoreOnGameEnded()
        {
            bPlayPause.Content = "Новая игра";
            EnableDisableButtons();
            core.GameEnded -= CoreOnGameEnded;
            miSettings.IsEnabled = !miSettings.IsEnabled;

        }

        private void PlayPause_OnClick(object sender, RoutedEventArgs e)
        {
            if (Data.GameStatus == GameStatus.Stopped)
            {
                NewGame();
            }
            else if (Data.GameStatus == GameStatus.Playing)
            {
                bPlayPause.Content = "Продолжить";
                core.PlayPause();
            }
            else
            {
                core.PlayPause();
                bPlayPause.Content = "Pause";

            }
            EnableDisableButtons();

        }

        /// <summary>
        /// Добавляет элементы в интерфейс и обновляет его
        /// </summary>
        private void DrawItems()
        {
            spThingsStorage.Children.Clear();
            // Отрисовка палочек
            for (int i = Data.CurrentItemsCount; i-- > 0;)
            {
                spThingsStorage.Children.Add(GetRectangle());
            }

            spThingsStorage.UpdateLayout();
        }

        /// <summary>
        /// Создаёт новый прямоугольник со стилем
        /// </summary>
        /// <returns></returns>
        private Rectangle GetRectangle()
        {
            return new Rectangle()
            {
                Width = 7,
                Fill = Brushes.Green,
                StrokeThickness = 2,
                Margin = new Thickness(4, 0, 4, 0)
            };
        }

        /// <summary>
        /// Блокирует или разблокирует кнопки
        /// </summary>
        private void EnableDisableButtons()
        {
            bOne.IsEnabled = !bOne.IsEnabled;
            bTwo.IsEnabled = !bTwo.IsEnabled;
            bThree.IsEnabled = !bThree.IsEnabled;

        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            core.GetItems(int.Parse(button.Tag.ToString()));
            core.ChangeCurrentPlayer();
            var itemsCount = Data.CurrentItemsCount;
        }

        Timer timer = new Timer();

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            timer.Stop();
        }
    }
}
