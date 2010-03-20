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
using System.Text;

namespace Space_Cats_V1._2
{
    class GameObject
    {
        //Declare Instance Variables ----------------------------------------------------------------------------
        private int z_ID;
        private Texture2D z_sprite;
        private Vector2 z_position;
        private Vector2 z_velocity;
        private float z_speed;
        private bool z_isAlive;
        //For Hit Detection Purposes
        private bool z_isKillerObject;
        private bool z_isPickUp;
        private int z_pointValue;
        private bool z_canTakeDamage;

        private static Random z_random;
        protected Random RandomGen
        { get { return z_random; } }

        #region Public properties
        public int ID
        {
            get { return z_ID; }
            set { z_ID = value; }
        }
        public float Speed
        {
            get { return z_speed; }
            set { z_speed = value; }
        }
        public Vector2 Position
        {
            get { return z_position; }
            set { z_position = value; }
        }
        public Vector2 DrawPosition
        { get { return new Vector2(z_position.X - z_sprite.Width / 2, z_position.Y - z_sprite.Height / 2); } }
        public Vector2 Velocity
        {
            get { return z_velocity; }
            set { z_velocity = value; }
        }
        public Vector2 VelocityWithSpeed
        { get { return z_velocity * z_speed; } }
        
        public bool IsAlive
        { 
            get { return z_isAlive; }
            set { z_isAlive = value; }
        }
        public bool CanTakeDamage
        {
            get { return z_canTakeDamage; }
            set { z_canTakeDamage = value; }
        }
        public bool IsKillerObject
        {
            get { return z_isKillerObject; }
            set
            {
                z_isKillerObject = value;
                if (value)
                    z_isPickUp = false;
            }
        }
        public bool IsPickUp
        { 
            get { return z_isPickUp; }
            set
            {
                z_isPickUp = value;
                if (value)
                    z_isKillerObject = false;
            }
        }
        public Texture2D Sprite
        {
            get { return z_sprite; }
            set { z_sprite = value; }
        }
        public int PointValue
        {
            get { return z_pointValue; }
            set { z_pointValue = value; }
        }
        virtual public Circle HitCircle
        { get { return new Circle(z_position, (z_sprite.Width < z_sprite.Height ? z_sprite.Width : z_sprite.Height) / 2); } }
        virtual public Rectangle HitRec
        { get { return new Rectangle((int)z_position.X - z_sprite.Width / 2, (int)z_position.Y - z_sprite.Height / 2, z_sprite.Width, z_sprite.Height); } }
        public float Left
        {
            get { return z_position.X - z_sprite.Width / 2; }
            set { z_position.X = value + z_sprite.Width / 2; }
        }
        public float Right
        {
            get { return z_position.X + z_sprite.Width / 2; }
            set { z_position.X = value - z_sprite.Width / 2; }
        }
        public float Top
        {
            get { return z_position.Y - z_sprite.Height / 2; }
            set { z_position.Y = value + z_sprite.Height / 2; }
        }
        public float Bottom
        {
            get { return z_position.Y + z_sprite.Height / 2; }
            set { z_position.Y = value - z_sprite.Height / 2; }
        }
        #endregion

        //Constructor -------------------------------------------------------------------------------------------
        public GameObject(Texture2D loadedTexture)
        {
            //Initialize all instance variables
            this.z_sprite = loadedTexture;
            this.z_position = Vector2.Zero;
            this.z_velocity = Vector2.Zero;
            this.z_speed = 1.0f;
            this.z_isAlive = false;
            this.z_isKillerObject = false;
            this.z_isPickUp = false;
            if (z_random == null)
                z_random = new Random();
        }

        //Access Methods ----------------------------------------------------------------------------------------

        //Mutator Methods ---------------------------------------------------------------------------------------
        //Other Methods -----------------------------------------------------------------------------------------
        public void upDatePosition()
        {
            this.z_position += this.z_velocity;
        }

        //Use this method for updating position if a speed is set
        public void upDatePositionWithSpeed()
        {
            this.z_position += z_velocity * z_speed;
        }

        virtual public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (IsAlive)
                spriteBatch.Draw(this.z_sprite, this.DrawPosition, Color.White);
        }


    }
}
