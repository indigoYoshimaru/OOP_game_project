using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public interface IDisplay
    {
        void LoadContent();
        void Display(GameTime gameTime);
    }
}
