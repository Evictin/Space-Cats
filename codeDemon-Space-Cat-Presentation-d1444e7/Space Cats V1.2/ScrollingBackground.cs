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
using System.Text;

namespace Space_Cats_V1._2
{
    class ScrollingBackground : GameObject
    {
        //Constructor
        public ScrollingBackground(Texture2D background)
            : base(background)
        {
            this.Velocity = new Vector2(0f,0.4f);
        }

        //Methods
        //Create a Method that will return a new vector for scaling the sprite with the viewport
        public Vector2 Scale(Rectangle viewPort)
        {
            /* How to calculate the scale percentage needed?
             * If sprite width = 2
             * and viewPort width = 4
             * Then 2 * x = 4
             * x = 4/2
             * x = 2
             * so scale % = (viewPort width/sprite width)?
            
            
            float scaleX = ((float)viewPort.Width / this.getSprite().Width);
            float scaleY = ((float)viewPort.Height / this.getSprite().Height);
            */

            return new Vector2(((float)viewPort.Width / this.Sprite.Width),
                                ((float)viewPort.Height / this.Sprite.Height));

        }





    }
}
