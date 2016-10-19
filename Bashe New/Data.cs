using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bashe_New
{
    static class Data
    {
        public static int ItemsCount = 10;

        public static int TurnInterval = 0;

        public static GameMode GameMode = GameMode.Ai;

        public static GameStatus GameStatus = GameStatus.Stopped;

        public static Player CurrentPlayer
        {
            get
            {
                return _currentPlayer;
            }
            set
            {
                _currentPlayer = value;
                
                CurrentPlayerChanged?.Invoke();
            }
        }

        private static Player _currentPlayer = Player.One;

        public static int Score1 = 0;

        public static int Score2 = 0;

        public static int CurrentItemsCount
        {
            get
            {
                return _currentItemsCount;
            }
            set
            {
                _currentItemsCount = value;
                CurrentItemsCountChanged?.Invoke();
            }
        }

        private static int _currentItemsCount = ItemsCount;

        public delegate void ValueChanged();

        public static event ValueChanged CurrentPlayerChanged;

        public static event ValueChanged CurrentItemsCountChanged;
    }

    public enum GameMode
    {
        Ai,
        Human
    }

    public enum GameStatus
    {
        Stopped,
        Paused,
        Playing
    }

    public enum Player
    {
        One,
        Two
    }
}
