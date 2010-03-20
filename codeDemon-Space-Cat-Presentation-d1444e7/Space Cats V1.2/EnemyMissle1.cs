﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Space_Cats_V1._2
{
    class EnemyMissle1 : MissileObject
    {
        //Instance Variables ---------------------------------------------------------
        private bool isAvailable;

        //Constructor ----------------------------------------------------------------
        public EnemyMissle1(Texture2D MissleSprite, Vector2 playersLocation, SpriteBatch spriteBatch)
            : base(MissleSprite, playersLocation, spriteBatch)
        {
            this.Velocity = Vector2.UnitY;
            this.Speed = 5;
            this.IsAlive = true;
            this.isAvailable = true;
        }

        //Accessor Methods -----------------------------------------------------------
        public bool getIsAvailable()
        {
            return this.isAvailable;
        }

        //Mutator Methods ------------------------------------------------------------
        public void setIsAvailable(bool isAvailable)
        {
            this.isAvailable = isAvailable;
        }

        //Other Methods --------------------------------------------------------------





    }
}
