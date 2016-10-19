using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bashe
{
    public static class Core
    {
        public static Data _data = new Data();

        public static void Init()
        {
            _data = new Data()
            {
                CurrentPlayer = Player.One,
                GameMode = GameMode.AI,
                IsPlaying = false,
                ScorePlayerOne = 0,
                ScorePlayerTwo = 0,
                ThingsCount = 10,
                Timeout = 0,
                Timer = new System.Timers.Timer(),
                CurrentThingsCount = 0,
                CurrentTime = 0
            };
        }

        public static void Play()
        {
            _data.IsPlaying = true;
            _data.CurrentThingsCount = _data.ThingsCount;
        }

        public static void GetThings(int count)
        {
            _data.CurrentThingsCount -= count;
        }

        public static void Stop()
        {

        }
    }
}
