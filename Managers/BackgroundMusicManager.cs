using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantvsZombie
{
    public class BackgroundMusicManager
    {
        private Song _StartSong;
        private Song _GameSong;
        private Song _EndSong;

        public void LoadContent()
        {
            _StartSong = PVZGame.Game.Content.Load<Song>("Media/background1");
            _GameSong = PVZGame.Game.Content.Load<Song>("Media/background2");
            _EndSong = PVZGame.Game.Content.Load<Song>("Media/background3");
            MediaPlayer.IsRepeating = true;
        }

        public void Update()
        {
            switch (PVZGame.Game.State)
            {
                case PVZGame.GameState.START_MENU:
                    Play(_StartSong);
                    break;
                case PVZGame.GameState.PLAYING:
                    Play(_GameSong);
                    break;
                case PVZGame.GameState.END_MENU:
                    Play(_EndSong);
                    break;
            }

        }

        public void OnChangeGameState()
        {
            MediaPlayer.Stop();
        }

        public void Play(Song s)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(s);
            }
        }
    }
}