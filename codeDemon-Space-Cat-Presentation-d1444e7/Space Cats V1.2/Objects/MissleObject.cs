using System;
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
    class MissleObject : GameObject
    {
        //Instance Variables ---------------------------------------------------------
        private SpriteBatch z_spriteBatch;
        private bool z_hasHitSomething;
        
       

        //Constructor ----------------------------------------------------------------
        public MissleObject(Texture2D MissleSprite, Vector2 startingLocation, SpriteBatch spriteBatch)
            : base(MissleSprite)
        {
            this.setPosition(startingLocation);
            this.setHitRec(new Rectangle((int)startingLocation.X, (int)startingLocation.Y,
                                    MissleSprite.Width, MissleSprite.Height));
            this.z_spriteBatch = spriteBatch;
            this.z_hasHitSomething = false;
        }

        //Accessor Methods -----------------------------------------------------------       
        public SpriteBatch getSpriteBatch()
        {
            return this.z_spriteBatch;
        }
        public bool getHasHitSomething()
        {
            return this.z_hasHitSomething;
        }
        

        //Mutator Methods ------------------------------------------------------------       
        public void setSpriteBatch(SpriteBatch newSpriteBatch)
        {
            this.z_spriteBatch = newSpriteBatch;
        }
        public void setHasHitSomething(bool boom)
        {
            this.z_hasHitSomething = boom;
        }
        

        //Other Methods --------------------------------------------------------------
        public void upDateMissle()
        {
            this.upDatePositionWithSpeed();
            this.setHitRec(new Rectangle((int)this.getPosition().X, (int)this.getPosition().Y,
                                         this.getSprite().Width, this.getSprite().Height));           
        }

        public void DrawMissle()
        {
            if(this.isAlive())
                this.z_spriteBatch.Draw(this.getSprite(), this.getPosition(), Color.White);
        }











    }
}
