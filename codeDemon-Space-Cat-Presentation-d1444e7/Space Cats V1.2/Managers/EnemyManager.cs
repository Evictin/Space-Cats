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
using System.IO;

namespace Space_Cats_V1._2
{
    class EnemyManager
    {
        /*
         * Logic Note: A lot of these variables should be converted into lists. Because 
         * Each wave will probably need to have it's own counters if we decide to allow
         * multiple waves to spawn at the same time.
         * */
        //Instance Variables
        private List<IEnemyShip> z_enemyShips;
        private ContentManager z_content;
        private SpriteBatch z_spriteBatch;
        private Rectangle z_viewPort;
        //A counter for the Update Method
        private float z_counter;
        //another counter for spreading out the enemies as they spawn
        private float z_interval;
        //A counter for keeping track how many enemies are spawned
        private int z_EnemiesSpawn;
        //Booleans for activating a type of wave of enemies
        //E1W1 stands for Enemey 1, Wave 1
        private bool z_ActivateE1W1;
        private PlayerShip playerShip = PlayerShip.getInstance(null, Vector2.Zero);
        private static EnemyManager z_instance = null;
        private List<IArtificialIntelligence> z_AIList;

        public static EnemyManager getInstance()
        {
            return z_instance;
        }

        public static EnemyManager getInstance(ContentManager content, SpriteBatch spriteBatch, Rectangle viewPort)
        {
            if (z_instance == null)
                z_instance = new EnemyManager(content, spriteBatch, viewPort);
            return z_instance;
        }

        //Constructor
        private EnemyManager(ContentManager content, SpriteBatch spriteBatch, Rectangle viewPort)
        {
            BinaryReader br;
            string input;
            int fileID;
            Rectangle fileViewport = new Rectangle(0,0,0,0);
            if (z_instance == null)
                z_instance = this;

            this.z_enemyShips = new List<IEnemyShip>();
            this.z_content = content;
            this.z_spriteBatch = spriteBatch;
            this.z_viewPort = viewPort;
            this.z_ActivateE1W1 = false;
            this.z_counter = 0;
            this.z_interval = 0;
            this.z_EnemiesSpawn = 0;

            this.z_AIList = new List<IArtificialIntelligence>();
            br = new BinaryReader(File.OpenRead(content.RootDirectory + "\\AI\\Mission 2.msn"));
            try
            {
                fileID = br.ReadInt32();
                if (fileID == 12)
                {
                    fileViewport.Width = br.ReadInt32();
                    fileViewport.Height = br.ReadInt32();
                    do
                    {
                        input = br.ReadString();
                        if (input.CompareTo("AI_SCRIPT") == 0)
                        {
                            z_AIList.Add(new AI_Script(fileViewport, br));
                        }
                    } while (input.CompareTo("EOF") != 0);
                }
            }
            finally
            {
                br.Close();
            }

            // Initialize the enemy1 pool
            Enemy1.Initialize(this.z_content);
            EnemySimpleBullet.Initialize(this.z_content, this.z_viewPort);
        }

       
        //Accessors
        public List<IEnemyShip> getEnemiesList()
        {
            return this.z_enemyShips;
        }
 
        public bool getE1W1()
        {
            return this.z_ActivateE1W1;
        }

        //Mutators
        public void setE1W1(bool set)
        {
            this.z_ActivateE1W1 = set;
        }

        //Populate three enemy1
        private void populateEnemy1Wave1(GameTime gameTime)
        {
            IEnemyShip enemy;
            this.z_interval += (float)gameTime.ElapsedGameTime.Milliseconds;
            if (this.z_interval >= 600)
            {
                if (this.z_EnemiesSpawn < z_AIList.Count)
                {
                    z_enemyShips.Add(Enemy1.getNewEnemy(z_AIList[z_EnemiesSpawn % z_AIList.Count]));
                    this.z_EnemiesSpawn++;
                }
                else
                {
                    this.z_EnemiesSpawn = 0;
                    this.z_ActivateE1W1 = false;
                }
                this.z_interval = 0;
            }
        }

        //Update all Enemies in the list method
        public void mainUpdate(GameTime gameTime)
        {
            this.z_counter += (float)gameTime.ElapsedGameTime.Milliseconds;

            //if (z_enemyShips.Count<3)
            //    this.z_ActivateE1W1 = true;
            if (this.z_counter > 5000)
            {
                this.z_ActivateE1W1 = true;
                this.z_counter = 0;
            }

            for (int i = 0; i< this.z_enemyShips.Count;i++)
            {
                this.z_enemyShips[i].AIUpdate(gameTime);
                if (!this.z_enemyShips[i].IsAlive)
                {   
                    this.z_enemyShips[i].returnToPool();
                    this.z_enemyShips.RemoveAt(i);
                }
            }

            for (int i = 0; i < this.z_enemyShips.Count; i++)
            {
                //Check for collision with player ship
                if (!this.playerShip.IsInvincible &&
                    this.z_enemyShips[i].HitCircle.Intersects(playerShip.HitCircle))
                {
                    // a collision will reduce the enemies health to zero
                    this.z_enemyShips[i].reduceHealth(this.z_enemyShips[i].Health);
                    // but will reduce the players life by the amount damage the enemy deals (it could be a missile, etc)
                    this.playerShip.Health -= this.z_enemyShips[i].Damage;
                }
            }

            if (this.z_ActivateE1W1)
                this.populateEnemy1Wave1(gameTime);

        }

        //Draw Method
        public void draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (IEnemyShip enemy in z_enemyShips)
                enemy.Draw(spriteBatch, gameTime);
        }

        static public int totalEnemyCount()
        {
            return z_instance.z_enemyShips.Count;
        }

        public void spawnEnemy(IEnemyShip enemy)
        {
            this.z_enemyShips.Add(enemy);
        }

        //Main reset
        public void resetAllEnemies()
        {
            while (this.z_enemyShips.Count>0)
            {
                z_enemyShips[0].returnToPool();
                z_enemyShips.RemoveAt(0);
            }
            this.z_ActivateE1W1 = false;
            this.z_EnemiesSpawn = 0;
        }
    }
}
