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
    class BulletPool1
    {
        //Instance Variables
        private List<PlayerMissile1> z_BulletPool;
        private ContentManager z_content;
        private Rectangle z_viewPort;
        private Texture2D z_image;
        private PlayerShip z_playerShip;
        private SpriteBatch z_spriteBatch;

        //Constructor
        public BulletPool1(ContentManager content, Rectangle viewPort, PlayerShip ship, SpriteBatch spriteBatch)
        {
            this.z_playerShip = ship;
            this.z_content = content;
            this.z_viewPort = viewPort;
            this.z_spriteBatch = spriteBatch;
            this.z_BulletPool = new List<PlayerMissile1>();
            this.z_image = z_content.Load<Texture2D>("Content\\Images\\Ball1");

            for (int i = 0; i < 100; i++)
            {
                this.z_BulletPool.Add(new PlayerMissile1(this.z_image, this.z_playerShip.Position, this.z_spriteBatch));
                this.z_BulletPool[i].IsAvailable=true;
            }
        }

        //Accessor
        public PlayerMissile1 getNextAvailableEnemy()
        {
            foreach (PlayerMissile1 missle in this.z_BulletPool)
            {
                if (missle.IsAvailable)
                {
                    missle.IsAvailable=false;
                    missle.Position = new Vector2(this.z_playerShip.Position.X
                                                                         + z_playerShip.Sprite.Width / 2
                                                                         - missle.Sprite.Width / 2,
                                                                 z_playerShip.Position.Y);
                    missle.IsAlive=true;
                    missle.upDateMissle();
                    return missle;
                }
            }
            //No enemies available from the pool, make a new one
            PlayerMissile1 missleTemp = new PlayerMissile1(this.z_image, this.z_playerShip.Position, this.z_spriteBatch);
            missleTemp.Position = new Vector2(this.z_playerShip.Position.X
                                                                         + z_playerShip.Sprite.Width / 2
                                                                         - missleTemp.Sprite.Width / 2,
                                                                 z_playerShip.Position.Y);
            missleTemp.IsAlive = true;
            return missleTemp;

        }

    }
}
