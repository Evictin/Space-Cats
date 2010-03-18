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

namespace Space_Cats_V1._2.Objects
{
    class EnemyBullet1 : IEnemyShip
    {
        //Instance Variables
        private static Random zs_randomGen;
        private static List<EnemyBullet1> zs_pool;
        private static Texture2D zs_image;

        private static PlayerShip zs_playerShip;

        //Constructor - this is private to force ppl to call the static function
        private EnemyBullet1(Texture2D loadedSprite, IArtificialIntelligence ai)
            : base(loadedSprite)
        {
            this.setIsKillerObject(true);
            this.setIsPickUp(false);
            if (ai != null)
            {
                setAI(ai);
                this.setPosition(this.getAI().getStartingPosition());
            }
        }

        //Accessors

        //Mutators

        // This function initializes the internal pool and loads in the image to use to create all the 
        // 'Enemy1' type ships. It also creates a few bullets in the pool for fast retrieval.
        public static void Initialize(ContentManager content)
        {
            zs_pool = new List<EnemyBullet1>();
            zs_image = content.Load<Texture2D>("Content\\Images\\Missiles\\Ball1");
            zs_randomGen = new Random();
            zs_playerShip = PlayerShip.getInstance();
            for (int i = 0; i < 50; i++)
            {
                zs_pool.Add(new EnemyBullet1(zs_image, null));
            }
        }

        // Retrieves a bullet from the pool. If the pool is empty, it creates one from scratch.
        public static EnemyBullet1 getNewBullet(IArtificialIntelligence ai)
        {
            EnemyBullet1 bullet;
            // This must be called with an AI for the bullet
            if (ai == null)
                return null;
            // if there are any bullets in the pool, use one of them
            if (zs_pool.Count > 0)
            {
                bullet = zs_pool[zs_pool.Count - 1];
                bullet.setAI(ai);
                zs_pool.RemoveAt(zs_pool.Count - 1);
            }
            else
                bullet = new EnemyBullet1(zs_image, ai); // pool was empty, so create a new enemy

            // reset the enemy before use
            bullet.setIsAlive(true);
            return bullet;
        }

        // return this enemy to the pool
        public override void returnToPool()
        {
            this.reset();
            zs_pool.Add(this);
        }

        public override void AIUpdate(GameTime gameTime)
        {
            float time = (float)gameTime.TotalGameTime.TotalMilliseconds;
            if (this.getAI().okToRemove())
            {
                this.setIsAlive(false);
                return;
            }

            this.setVelocity(this.getAI().calculateNewVelocity(this.getPosition(), gameTime));

            this.upDatePosition();
            this.setHitRec(new Rectangle((int)this.getPosition().X, (int)this.getPosition().Y,
                          (int)this.getSprite().Width, (int)this.getSprite().Height));
        }

        override public void reset()
        {
            this.setVelocity(Vector2.Zero);
            this.setSpeed(1.0f);
            this.setIsAlive(false);
            this.setHitRec(new Rectangle(0, 0, 0, 0));
            this.setIsKillerObject(false);
            this.setIsPickUp(false);
            this.getAI().reset();
            this.setPosition(getAI().getStartingPosition());
        }
    }
}
