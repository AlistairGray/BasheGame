using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Bashe
{
    public class Data
    {
        public int ThingsCount = 10;
        public int Timeout = 0;
        public int CurrentThingsCount = 0;
        public int ScorePlayerOne = 0;
        public int ScorePlayerTwo = 0;
        public int CurrentTime = 0;
        public GameMode GameMode = GameMode.AI;
        public Player CurrentPlayer = Player.One;
        public System.Timers.Timer Timer = new System.Timers.Timer();
        public bool IsPlaying = false;
        public bool IsPaused = false;
    }

    public enum GameMode
    {
        AI,
        Human
    }

    public enum Player
    {
        One,
        Two
    }
}
