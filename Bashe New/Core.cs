using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bashe_New
{
    class Core
    {
        System.Threading.Timer Timer;

        /// <summary>
        /// Текущее ограничение на ход
        /// </summary>
        int CurrentTime = 0;

        public event EventHandler GameEnded;


        /// <summary>
        /// Действия по истечении одной секунды
        /// </summary>
        /// <param name="state"></param>
        public void OnTimerElapsed(object state)
        {
            if (Data.GameStatus == GameStatus.Playing)
            {
                CurrentTime--;
                if (CurrentTime == 0)
                {
                    GetItems(1);
                    ChangeCurrentPlayer();
                }
            }
        }

        /// <summary>
        /// Запуск игрового цикла
        /// </summary>
        public void Start()
        {
            Data.CurrentItemsCount = Data.ItemsCount;
            CurrentTime = Data.TurnInterval;
            Data.CurrentPlayer = Player.One;


            if (Data.GameMode == GameMode.Ai)
            {
                //Игра с компьютером
                Data.CurrentPlayerChanged += OnCurrentPlayerChanged;
            }
            else if (Data.GameMode == GameMode.Human)
            {
                
            }

            GameEnded += OnGameEnded;

            Data.CurrentItemsCountChanged += Data_CurrentItemsCountChanged;


            Data.GameStatus = GameStatus.Playing;
            //if (Data.TurnInterval > 0)
            //{
            //    Timer = new System.Threading.Timer(OnTimerElapsed, null, 0, 1000);
            //}
        }

        private void Data_CurrentItemsCountChanged()
        {
            if (Data.CurrentItemsCount<=0)
            {
                GameEnded?.Invoke();
            }
        }

        public void OnGameEnded()
        {
            var currentPlayer = Data.CurrentPlayer;

            string winMessage = "Игрок ";
            if (currentPlayer == Player.One)
            {
                Data.Score2++;
                winMessage += "2 ";
            }
            else
            {
                Data.Score1++;
                winMessage += "1 ";
            }

            winMessage += "победил!";
            MessageBox.Show(null, winMessage, "Победа", MessageBoxButtons.OK, MessageBoxIcon.Information);


            Stop();
        }


        /// <summary>
        /// Дествия при смене игрока для того чтобы подменить реального второго игрока компьютером
        /// </summary>
        public void OnCurrentPlayerChanged()
        {
            if (Data.CurrentPlayer == Player.Two)
            {
                var Random = new Random(DateTime.Now.Millisecond);
                int numberOfItemsToGet = Random.Next(1, 3);
                GetItems(numberOfItemsToGet);
                ChangeCurrentPlayer();
            }
        }

        public void Stop()
        {
            if (Data.GameStatus != GameStatus.Stopped)
            {
                Data.CurrentPlayerChanged -= OnCurrentPlayerChanged;
                GameEnded -= OnGameEnded;
                Data.GameStatus = GameStatus.Stopped;
            }
        }

        /// <summary>
        /// Приостановка / возобновление
        /// </summary>
        public void PlayPause()
        {

            Data.GameStatus = (Data.GameStatus == GameStatus.Playing) ? GameStatus.Paused : GameStatus.Playing;

        }

        /// <summary>
        /// Вычитает из текущего количества палочек указанное
        /// </summary>
        /// <param name="number">Количество палочек для вычета</param>
        public void GetItems(int number)
        {
            if (Data.GameStatus == GameStatus.Playing)
            {       
                Data.CurrentItemsCount -= number;
            }
        }

        /// <summary>
        /// Изменяет режим игры на указанный
        /// </summary>
        /// <param name="newGameMode">Выбранный режим</param>
        public void ChangeGameMode(GameMode newGameMode)
        {
            Data.GameMode = newGameMode;
        }

        /// <summary>
        /// Меняет текущего игрока на противоположного
        /// </summary>
        public void ChangeCurrentPlayer()
        {
            Data.CurrentPlayer = ((int)Data.CurrentPlayer) == 0 ? Player.Two : Player.One;
        }

        public delegate void EventHandler();
    }
}
