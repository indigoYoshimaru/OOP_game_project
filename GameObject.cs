﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlantvsZombie
{
    public delegate void DieDelegate(object self);

    public abstract class GameObject 
    {
        public event DieDelegate Died;
        private float _Speed;
        private Vector2 _Position;

        public Vector2 Position
        {
            get { return _Position; }
            set
            {
                _Position = value;
            }
        }

        public float Speed
        {
            get { return _Speed; }
            set
            {
            
                _Speed = value;
            }
        }

        public abstract void Update(); // we can foretell this function
        public virtual void Die() // all objects must be in the form of this die function
        {
            if (Died != null)
                Died(this);
        }

        
        public abstract float GetScaleFactor();
        public GameObject()
        {

        }
        
    }
    
}
