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
    class MissleManager
    {
        /*
         * 
         * The Missle Manager for the time being will only handle FriendlyMissles fired by the
         * human player. Later on the EnemyMissles should be implemented in a way such that missle 
         * manager can interact with enemy AI classes.
         * 
         * */


        //Instance Variables ---------------------------------------------------------
        private List<MissileObject> z_missles;
        private Rectangle z_viewPort;
        private List<IEnemyShip> z_EnemyShipList;
        //The Enemy Manager
        private EnemyManager z_enemyManager;
        public static MissleManager z_instanceOf;
        private PlayerShip z_playerShip;

        public static MissleManager getInstance()
        {
            return z_instanceOf;
        }

        //Constructor ----------------------------------------------------------------
        public MissleManager(Rectangle newViewPort, ContentManager content, SoundEffect sound, SpriteBatch spriteBatch, PlayerShip playerShip)
        {
            this.z_playerShip = playerShip;
            this.z_viewPort = newViewPort;
            this.z_enemyManager = EnemyManager.getInstance(content, spriteBatch, newViewPort);
            this.z_EnemyShipList = z_enemyManager.getEnemiesList();
            this.z_missles = new List<MissileObject>();
            PlayerMissile1.Initialize(content);
            z_instanceOf = this;
        }

        //Accessor Methods -----------------------------------------------------------
        public Rectangle getViewPort()
        {
            return this.z_viewPort;
        }
        public int getTotalMissileCount()
        {
            return z_missles.Count;
        }
        //Mutator Methods ------------------------------------------------------------
        public void setViewPort(Rectangle viewPort)
        {
            this.z_viewPort = viewPort;
        }

        //Update and Draw Methods --------------------------------------------------------------

        //Main Update Method for Keyboard
        public void MissleManagerUpdateFriendlyKeyboard(KeyboardState currentKeyState, KeyboardState previousKeyState,
                                                        PlayerShip playerShip, SpriteBatch spriteBatch)
        {
            //The Alogrithm:
            //Determine if the player shot a missle
            //If so then add it the List
            //If the list is not empty, update each missle
            //While checking each missle, make sure it hasn't left the screen or collided with something
            //If so, remove it from the list

            if (currentKeyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) && playerShip.IsAlive)
            {
                //Create and add a new Missle Object
                this.z_missles.Add(PlayerMissile1.GetNextMissile(new Vector2(playerShip.Position.X, playerShip.Top)));
            }

            //If List is not empty, update everything
            if (this.z_missles.Count > 0)
                this.UpdateFriendlyList(spriteBatch);
            
        }


        //Main Update Method for GamePad
        public void MissleManagerUpdateFriendlyGamepad(GamePadState currentPadState, GamePadState previousPadState,
                                                        PlayerShip playerShip, SpriteBatch spriteBatch)
        {
            //For the simple collision checking
            //this.z_EnemyShipList = enemyList;
            //Same Algorithm as before, but with a gamePad controller [Fire = right Trigger]
            if (currentPadState.Triggers.Right >= .5f && previousPadState.Triggers.Right == 0 && playerShip.IsAlive)
            {
                this.z_missles.Add(PlayerMissile1.GetNextMissile(new Vector2(playerShip.Position.X, playerShip.Top)));
            }

            //If List is not empty, update everything
            if (this.z_missles.Count > 0)
                this.UpdateFriendlyList(spriteBatch);

        }


        //Helper Method for Updating all Missles in the FriendlyMissles List
        private void UpdateFriendlyList(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < this.z_missles.Count; i++)
            {
                if (this.z_viewPort.Contains(new Point((int)z_missles[i].Position.X,(int) z_missles[i].Position.Y)))
                {
                    this.z_missles[i].upDateMissle();
                }
                else
                {
                    this.z_missles[i].returnToPool();
                    this.z_missles.RemoveAt(i);
                    //Since a Missle was just removed from the list, ensure i is poitning to the next missle
                    i--;
                    continue;
                }

                //Do some simple collision checking

                foreach (IEnemyShip enemy in this.z_EnemyShipList)
                {
                    if (enemy.CanTakeDamage)
                    {
                        if (this.z_missles[i].HitCircle.Intersects(enemy.HitCircle))
                        {
                            enemy.reduceHealth(this.z_missles[i].Damage);
                            if (!enemy.IsAlive)
                            {
                                this.z_playerShip.score += enemy.PointValue;
                            }

                            this.z_missles[i].returnToPool();
                            this.z_missles.Remove(this.z_missles[i]);
                            i--;
                            if (i < 0)
                                break;
                            continue;
                        }
                    }
                }


            }
        }

        //Main Draw Method for drawing all missles
        public void MissleManagerDrawAllMissles(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //That means there is something to draw in the missle list
            foreach (MissileObject missle in this.z_missles)
            {
                missle.Draw(spriteBatch, gameTime);
            }
        }

        public void reset()
        {
            foreach (MissileObject missile in z_missles)
                missile.returnToPool();
            this.z_missles.Clear();
        }

    }
}
