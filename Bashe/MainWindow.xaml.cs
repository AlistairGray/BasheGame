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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bashe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Core.Init();
            spButtons.IsEnabled = false;

            ScoreUpdate();

            var timer = Core._data.Timer;
            //timer.Interval = Core._data.Timeout * 1000;

            Core._data.Timer.Elapsed += (sender, args) =>
            {
                ChangePlayer();
                timer.Interval = Core._data.Timeout * 1000;
                if (Core._data.IsPlaying)
                {
                    timer.Start();
                }
            };
        }

        public void ScoreUpdate()
        {
            var data = Core._data;
            lScore.Content = $"Счёт: {data.ScorePlayerOne}:{data.ScorePlayerTwo}";
        }


        private void StartGame_OnClick(object sender, RoutedEventArgs e)
        {
            if (Core._data.IsPlaying)
            {
                if (Core._data.IsPaused)
                {
                    //Возобнавляем игру
                    bPlayPause.Content = "Приостановить";
                    gbPlayer.IsEnabled = true;
                    Core._data.IsPaused = false;
                }
                else
                {
                    //Приостанавливаем игру
                    bPlayPause.Content = "Возобновить";
                    Core._data.IsPaused = true;
                    gbPlayer.IsEnabled = false;
                }
            }
            else
            {
                //Начало новой игры
                if (Core._data.Timeout > 0)
                {
                    //Core._data.Timer.Start();
                }

                //Активация кнопок в начале игры
                bool isFirstElement = true;
                foreach (var child in spButtons.Children)
                {
                    if (isFirstElement)
                    {
                        isFirstElement = false;
                        continue;
                    }
                    var button = child as Button;
                    button.IsEnabled = true;
                }
                spButtons.IsEnabled = true;
                bPlayPause.Content = "Приостановить";

                Core.Play();


                spThingsStorage.UpdateLayout();
            }
        }

        void ChangePlayer()
        {
            Core._data.CurrentPlayer = ((int)(Core._data.CurrentPlayer)) == 0 ? Player.Two : Player.One;

            gbPlayer.Header = "Игрок ";
            gbPlayer.Header += ((int)(Core._data.CurrentPlayer) + 1).ToString();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            //Удаляем предмеиты по нажатию на кнопку
            var button = sender as Button;
            var deletedThings = Convert.ToInt32(button.Tag);

            Core.GetThings(deletedThings);

            for (int i = deletedThings; i-- > 0;)
            {
                spThingsStorage.Children.RemoveAt(0);
            }

            //Меняем игрока



            //Фризим кнопки если элементом меньше
            if (Core._data.CurrentThingsCount < 3)
            {
                bool isFirstElement = true;

                foreach (var child in spButtons.Children)
                {
                    if (isFirstElement == true)
                    {
                        isFirstElement = false;
                        continue;

                    }
                    var _button = child as Button;
                    if (Convert.ToInt32(_button.Tag.ToString()) > Core._data.CurrentThingsCount)
                    {
                        _button.IsEnabled = false;
                    }
                }

                if (Core._data.CurrentThingsCount == 0)
                {
                    Core._data.IsPlaying = false;

                    string GameOverMessage = "Игрок ";
                    if (Core._data.CurrentPlayer == 0)
                    {
                        GameOverMessage += "1";
                        Core._data.ScorePlayerOne++;

                    }
                    else
                    {
                        GameOverMessage += "2";
                        Core._data.ScorePlayerTwo++;
                    }
                    GameOverMessage += " Победил!";
                    MessageBox.Show("Игра окончена.\n" + GameOverMessage);

                    ScoreUpdate();
                    bPlayPause.Content = "Новая игра";
                }
            }

            spThingsStorage.UpdateLayout();

            if (Core._data.GameMode == GameMode.AI && Core._data.CurrentPlayer == Player.Two)
            {
                if (Core._data.CurrentThingsCount <= 3)
                {
                    bool IsFirstElement = true;
                    foreach (var child in spButtons.Children)
                    {
                        if (IsFirstElement)
                        {
                            IsFirstElement = false;
                            continue;
                        }
                        var _button = child as Button;
                        if (_button.Tag.ToString() == Core._data.CurrentThingsCount.ToString())
                        {
                            Button_OnClick(_button, e);
                        }
                    }
                }
                else
                {
                    var random = new Random(DateTime.Now.Millisecond);

                }
            }

            ChangePlayer();
        }

        private void NewGame_OnClick(object sender, RoutedEventArgs e)
        {
            Core._data.IsPlaying = false;
            StartGame_OnClick(sender, e);
        }
    }
}
