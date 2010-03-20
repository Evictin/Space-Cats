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
    class Asteroid : GameObject
    {
        //Instance Variables
        private Vector2 z_center;
        private Vector2 z_centerPosition;
        private float z_rotation;
        private float z_rotationSpeed;
        private bool z_hasBeenhit;

        //Constructor
        public Asteroid(Texture2D loadedSprite)
            : base(loadedSprite)
        {
            this.z_rotation = 0.0f;
            this.z_center = new Vector2(this.Sprite.Width / 2, this.Sprite.Height / 2);
            this.Velocity = Vector2.UnitY * 2;
            this.z_rotationSpeed = 0f;
            this.z_hasBeenhit = false;
        }


        //Accessor Methods
        public Vector2 getCenter()
        {
            return this.z_center;
        }
        public float getRotation()
        {
            return this.z_rotation;
        }
        public float getRotationSpeed()
        {
            return this.z_rotationSpeed;
        }
        public bool getHasBeenHit()
        {
            return this.z_hasBeenhit;
        }

        //Mutator Methods
        public void setCenter(Vector2 newCenter)
        {
            this.z_center = newCenter;
        }
        public void setRotation(float newRotation)
        {
            this.z_rotation = newRotation;
        }
        public void setRotationSpeed(float newRotSpeed)
        {
            this.z_rotationSpeed = newRotSpeed;
        }
        public void sethasBeenHit(bool newBool)
        {
            this.z_hasBeenhit = newBool;
        }
        public void setAstroPosition(Vector2 newPosition)
        {
            this.z_centerPosition = newPosition;
            this.Position=newPosition;
        }

        //Asteroid Update Method
        public void AstroUpdate()
        {
            this.z_rotation += this.z_rotationSpeed;
            this.upDatePositionWithSpeed();
            if (this.z_centerPosition != null)
            {
                this.z_centerPosition += this.VelocityWithSpeed;
            }
        }

    }
}
