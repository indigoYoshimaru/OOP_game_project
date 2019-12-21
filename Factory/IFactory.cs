using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlantvsZombie
{
    interface IFactory
    {
        Plant PlantFactory(string plantState, Vector2 plantPosition);
        Zombie ZombieFactory();
    }
}
