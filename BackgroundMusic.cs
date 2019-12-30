using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantvsZombie
{
    public class BackgroundMusic
    {
        private Song _MenuSong;
        private Song _GameSong;
        private Song _EndSong;
        
        public void LoadContent()
        {
            _MenuSong = PVZGame.Game.Content.Load<Song>("Texture/Music/background1");
            _GameSong = PVZGame.Game.Content.Load<Song>("Texture/Music/background2");
            _EndSong = PVZGame.Game.Content.Load<Song>("Texture/Music/background3");
            MediaPlayer.IsRepeating = true;
        }

        public void Update()
        {
            switch (PVZGame.Game.State)
            {
                case PVZGame.GameState.START_MENU:
                    Play(_MenuSong);
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
