﻿using GameSpace.Abstracts;
using GameSpace.Camera2D;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.ItemObjects;//TEMP
using GameSpace.Sprites;//TEMP
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Machines
{
    public class HUDHandler : Handler
    {
        private SpriteFont HeadsUpDisplay;
        private Texture2D chungus;
        private Texture2D gameOver;
        private Vector2 HudPosition;
        private Vector2 HealthBarPosition;
        private Vector2 ExpBarPosition;
        public static long ticks;
        public static long seconds;
        public static long ticksMax = 4010000000;
        public static long convertToSeconds = 10000000;
        public static int bonusPoints;

        private HealthBar healthBar;
        private ExpBar expBar;

        private static readonly HUDHandler instance = new HUDHandler();
        public static HUDHandler GetInstance()
        {
            return instance;
        }

        private HUDHandler()
        {

        }

        #region Death Timer
        public void ResetTimer()
        {
            timer = 0;
        }

        public void UpdateSecondsAfterWinning()
        {
            seconds = 0;
        }


        public void UpdateTimer()
        {
            timer += internalGametime.ElapsedGameTime.Ticks;
            ticks = ticksMax - timer;
            seconds = ticks / convertToSeconds;
            if (seconds == 50)
            {
                MusicHandler.GetInstance().PlaySoundEffect(11);
            }

            if (seconds == 0 || game.GetMario.MarioActionState is GameSpace.States.MarioStates.DeadMarioState)
            {
                if (seconds <= 0 && game.CurrentState is GameSpace.States.GameStates.PlayingGameState)// AND YOU DIDNT COMPLETE OBJECTIVE
                {
                    mario.DeadTransition();//Lose a life if timer reaches 0
                }
                ResetTimer();
            }
            else
            {
                bonusPoints = (int)seconds;
                //Add bonusPoints to Point Tracking System
            }
        }
        #endregion

        public void LoadContent(ContentManager content, GameRoot gameRoot)
        {
            HeadsUpDisplay = content.Load<SpriteFont>("font");
            chungus = content.Load<Texture2D>("Background/Untitled");
            gameOver = content.Load<Texture2D>("Background/game-over-screen");
            HudPosition.X = 10;
            HudPosition.Y = 10;
            HealthBarPosition = new Vector2(25, 90);
            ExpBarPosition = new Vector2(10, 120);
            healthBar = new HealthBar(content, HealthBarPosition);
            expBar = new ExpBar(content, ExpBarPosition);
            game = gameRoot;
            ResetTimer();
        }

        public void LoadGameTime(GameTime gameTime)
        {
            internalGametime = gameTime;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Coin HUDCoin = new Coin(new Vector2(0, 0));
            spritebatch.DrawString(HeadsUpDisplay, "[" + mario.Player + "]\n", HudPosition, Color.White);
            spritebatch.DrawString(HeadsUpDisplay, "Score: " + mario.score.ToString("D6"), new Vector2(HudPosition.X, HudPosition.Y + 40), Color.White);
            spritebatch.DrawString(HeadsUpDisplay, "Time\n" + seconds, new Vector2(HudPosition.X + 200, HudPosition.Y), Color.White);
            spritebatch.DrawString(HeadsUpDisplay, "World\n  1-1", new Vector2(HudPosition.X + 320, HudPosition.Y), Color.White);


            spritebatch.DrawString(HeadsUpDisplay, "X  " + mario.numCoinsCollected, new Vector2(HudPosition.X + 460, HudPosition.Y + 20), Color.White); //Update to display coins

            SpriteEffects facing = SpriteEffects.None;
            if (MarioHandler.mario.Facing == MarioDirection.RIGHT)
            {
                facing = SpriteEffects.FlipHorizontally; //float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
            }

            spritebatch.Draw(MarioHandler.mario.sprite.Texture, new Vector2((int)HudPosition.X + 640, (int)HudPosition.Y + 25), MarioHandler.mario.sprite.getCurrentSpriteRect(), Color.White,
                0, new Vector2(1, 1), new Vector2(1, 1), facing, 1);
            spritebatch.DrawString(HeadsUpDisplay, "X  " + MarioHandler.marioLives, new Vector2(HudPosition.X + 660, HudPosition.Y + 20), Color.White);
            healthBar.DrawHealthBar(spritebatch);
            expBar.DrawExpBar(spritebatch);
            UpdateHudPosition();
        }

        public void DrawStartingPanel(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(HeadsUpDisplay, "PRESS N FOR NEW GAME", HudPosition, Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "PRESS L TO LOAD GAME (FUTURE FEATURE)", new Vector2(HudPosition.X, HudPosition.Y + 40), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "PRESS Q TO QUIT GAME", new Vector2(HudPosition.X, HudPosition.Y + 80), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "MAIN MENU WIP", new Vector2(HudPosition.X, HudPosition.Y + 120), Color.Black);
            spritebatch.Draw(chungus, new Vector2(HudPosition.X, HudPosition.Y + 200), Color.White);
        }

        public void DrawEndingResetPanel(SpriteBatch spritebatch)
        {

            spritebatch.DrawString(HeadsUpDisplay, "[" + mario.Player + "]\n", new Vector2(0, 0), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Score: " + mario.score.ToString("D6"), new Vector2(0, 40), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Time\n" + 0, new Vector2(200, 0), Color.Black); //Seconds is 0
            spritebatch.DrawString(HeadsUpDisplay, "World\n  1-1", new Vector2(320, 0), Color.Black);
            Coin HUDCoin = new Coin(new Vector2(0, 0));
            HUDCoin.Sprite.Draw(spritebatch, new Vector2(460, 20));//Draws Unanimated Coin

            spritebatch.DrawString(HeadsUpDisplay, "X  " + mario.numCoinsCollected, new Vector2(480, 20), Color.Black); //Update to display coins

            spritebatch.Draw(MarioHandler.mario.sprite.Texture, new Vector2(640, 25), MarioHandler.mario.sprite.getCurrentSpriteRect(), Color.White);
            spritebatch.DrawString(HeadsUpDisplay, "X  " + MarioHandler.marioLives, new Vector2(660, 20), Color.Black);//Lives

            spritebatch.DrawString(HeadsUpDisplay, "PRESS R FOR NEW GAME", new Vector2(0, 120), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "PRESS Q TO QUIT GAME", new Vector2(400, 120), Color.Black);
            UpdateHudPosition();
        }

        public void DrawGameOver(SpriteBatch spritebatch)
        {
            spritebatch.Draw(gameOver, new Vector2(0, 0), Color.White);
            spritebatch.DrawString(HeadsUpDisplay, "[" + mario.Player + "]\n", new Vector2(0, 0), Color.White);
            spritebatch.DrawString(HeadsUpDisplay, "Score: " + mario.score.ToString("D6"), new Vector2(0, 40), Color.White);
            spritebatch.DrawString(HeadsUpDisplay, "Time\n" + seconds, new Vector2(200, 0), Color.White);
            spritebatch.DrawString(HeadsUpDisplay, "World\n  1-1", new Vector2(320, 0), Color.White);

            Coin HUDCoin = new Coin(new Vector2(0, 0));
            HUDCoin.Sprite.Draw(spritebatch, new Vector2(460, 20));
            spritebatch.DrawString(HeadsUpDisplay, "X  " + mario.numCoinsCollected, new Vector2(480, 20), Color.White); //Update to display coins

            spritebatch.Draw(MarioHandler.mario.sprite.Texture, new Vector2(640, 25), MarioHandler.mario.sprite.getCurrentSpriteRect(), Color.White);
            spritebatch.DrawString(HeadsUpDisplay, "X  " + MarioHandler.marioLives, new Vector2(660, 20), Color.White);//Lives

            spritebatch.DrawString(HeadsUpDisplay, "PRESS R FOR NEW GAME", new Vector2(0, 120), Color.White);
            spritebatch.DrawString(HeadsUpDisplay, "PRESS Q TO QUIT GAME", new Vector2(400, 120), Color.White);
            UpdateHudPosition();

        }

        private void UpdateHudPosition()
        {
            if (cameraCopy.Position.X + 10 > HudPosition.X || HudPosition.X > cameraCopy.Position.X)
            {
                HudPosition.X = cameraCopy.Position.X + 10;
                healthBar.UpdateHealthBarLocation(cameraCopy);
                expBar.UpdateExpBarLocation(cameraCopy);
            }
        }

        public void UpdateHealth()
        {
            healthBar.DecrementHealth(mario);
        }

        public void UpdateExp(int exp)
        {
            expBar.IncrementExp(exp);
        }

    }

    public class HealthBar : Handler
    {
        private readonly Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle; //Instead of some int value that denotes health we will use rectangles

        public HealthBar(ContentManager content, Vector2 position)
        {
            texture = content.Load<Texture2D>("HealthBar");
            this.position = position;
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public void DrawHealthBar(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, rectangle, Color.White);
        }

        public void UpdateHealthBarLocation(Camera camera)
        {
            position.X = camera.Position.X + 10;
        }

        public void DecrementHealth(Mario mario)
        {
            rectangle.Width -= damageTakenScale;
            if (rectangle.Width <= 1)
            {
                mario.MarioPowerUpState.DeadTransition();
                gameRoot.RestartCurrentState();
                rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            }
        }
    }

    public class ExpBar : Handler
    {
        private readonly Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;

        public ExpBar(ContentManager content, Vector2 position)
        {
            texture = content.Load<Texture2D>("ExpBar");
            this.position = position;
            rectangle = new Rectangle(0, 0, 0, texture.Height);
        }

        public void DrawExpBar(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, rectangle, Color.White);
        }

        public void UpdateExpBarLocation(Camera camera)
        {
            position.X = camera.Position.X + 10;
        }

        public void IncrementExp(int exp)
        {
            rectangle.Width += (int)(experinceScale * exp);

            if (rectangle.Width > 96 && currentMarioLevel < 3)
            {
                MusicHandler.GetInstance().PlaySoundEffect(14);
                MarioHandler.GetInstance().LevelMarioUp();
                rectangle.Width = 0;
            }
        }
    }

}
