using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public abstract class Factory
    {
        public abstract GameObject FactoryMethod(String state, Vector2 position);

        public Zombie GetZombie()
        {
            return (Zombie)FactoryMethod(null, Vector2.Zero);
        }

        public Plant GetPlant(string plantState, Vector2 plantPosition)
        {
            return (Plant)FactoryMethod(plantState, plantPosition);
        }

    }
}
