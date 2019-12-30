using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public class PlantFactory:Factory
    {
        public override GameObject FactoryMethod(String state, Vector2 position)
        {
            return PlantConcreteFactory(state, position);
        }

        private Plant PlantConcreteFactory(string plantState, Vector2 plantPosition)
        {
            switch (plantState)
            {
                case "PeaShooter":
                    return new PeaShooter(plantPosition);
                case "SunFlower":
                    return new SunFlower(plantPosition);
                case "CarnivorousPlant":
                    return new CarnivorousPlant(plantPosition);
                default:
                    return null;
            }
        }
    }
}
