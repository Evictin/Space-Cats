using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Space_Cats_V1._2.AI
{
    class SimpleBulletAI:IArtificialIntelligence
    {
        private Rectangle z_viewport;
        private Vector2 z_startLocation;
        private Vector2 z_velocity;
        private float z_speed;
        private bool z_removeEnemy;
        private int z_ID;

        #region Public Properties
        public float Speed 
        { 
            get { return z_speed; }
            set { z_speed = value; }
        }
        #endregion

        #region Constructors
        private SimpleBulletAI()
        {
            // Make it private so a blank one can't be called
            // we do NOT want a blank AI here
        }

        public SimpleBulletAI(Rectangle viewport)
        {
            this.z_removeEnemy = false;
            this.z_viewport = viewport;
            this.z_speed = 10f;
            this.z_velocity = Vector2.UnitY;
        }
        #endregion

        #region Accessors
        //Get the starting position of the enemy
        public Vector2 getStartingPosition()
        {
            return z_startLocation;
        }
        #endregion

        public IArtificialIntelligence clone()
        {
            return new SimpleBulletAI(z_viewport);
        }

        //Return the new velocity for the enemy
        public Vector2 calculateNewVelocity(Vector2 currentPosition, GameTime gameTime)
        {
            return z_velocity * z_speed;
        }

        //Return a new speed for the enemy
        public float calculateNewSpeed(Vector2 currentPosition, GameTime gameTime)
        {
            return 1.0f;
        }

        //Decide when the enemy should fire a missle
        public bool firesMissle(Vector2 currentPosition, GameTime gameTime)
        {
            return false;
        }

        //The enemy's AI is finished and the enemy is ready to be removed from the game
        public bool okToRemove()
        {
            return this.z_removeEnemy;
        }
	
	}

}
