﻿using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.States.MarioStates
{
    internal class FireMarioCrouchingState : MarioActionStates//MarioPowerUpStates
    {
        public FireMarioCrouchingState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.MarioActionState = this;
            this.previousActionState = previousActionState;
            // Mario.marioPowerUpState = new FireMarioState(Mario);
            //Debug.WriteLine("MarioStandState(25) Enter, {0}", Mario.marioActionState);
            //Debug.WriteLine("MarioWalkingState(25) facing:, {0}", Mario.Facing);

            //Mario.Position = new Vector2(Mario.Position.X, Mario.Position.Y + 20);

            //Mario.Velocity = new Vector2(Mario.Velocity.X, 100);//TEMP
            //AABB aabb = Mario.AABB;
            //eFacing Facing = MarioStandingState.Facing;
            MarioDirection Facing = Mario.Facing;
            Mario.Facing = Facing;
            //Mario.Sprite = MarioStandingState.SpriteFactory.CreateSprite(MarioSpriteFactory.MarioSpriteType(this, currentPowerUpState));
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.MarioPowerUpState));
            Mario.Position = new Vector2(Mario.Position.X, Mario.Position.Y + 20);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(1);

        }

        public override void Exit() { Mario.Position = new Vector2(Mario.Position.X, Mario.Position.Y - 20); }


        public override void StandingTransition()
        {//going to crouch for now(going to superstand
         //currentActionState.Exit();
            /// Debug.WriteLine("Fire Standtrans");
            Exit();
            Mario.MarioActionState = new FireMarioStandingState(Mario);
            // Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(2);
            Mario.MarioActionState.Enter(this); // Changing states

        }
        public override void CrouchingTransition()
        {

        }//nothing
        public override void WalkingTransition()//Not Used Now, Used after Sprint2
        {
            //Exit();
            // Mario.marioActionState = new FireMarioWalkingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);
            //Mario.marioActionState.Enter(this); // Changing states
            //Mario.marioActionState.FireMarioWalkingState.Enter(this);
        }
        public override void RunningTransition()
        {
            //Exit();
            //Mario.marioActionState = new FireMarioRunningState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(4);
            //Mario.marioActionState.Enter(this); // Changing states
        } //Longer you hold running you increase velocity and speed of animation
        public override void JumpingTransition()
        {
            //Exit();
            //Mario.marioActionState = new FireMarioJumpingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(5);
            //Mario.marioActionState.Enter(this); // Changing states
        }
        public override void FallingTransition()
        {
            //Exit();
            //Mario.marioActionState = new FireMarioFallingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(6);
            //Mario.marioActionState.Enter(this); // Changing states
        }

        public override void FaceLeftTransition()
        {

            if (Mario.Facing == MarioDirection.LEFT)
            {
                RunningTransition();
            }
            // WalkingTransition(); bc no walking
            else
            {
                Mario.Facing = MarioDirection.LEFT;
            }
        }
        public override void FaceRightTransition()
        {

            if (Mario.Facing == MarioDirection.RIGHT)
            {
                RunningTransition();
            }
            // WalkingTransition();
            else
            {
                Mario.Facing = MarioDirection.RIGHT;
            }
        }

        public override void UpTransition()
        {
            StandingTransition();
        }
        public override void DownTransition()
        {

        }

        public override void SmallPowerUp()
        {
            Exit();
            Mario.MarioActionState = new SmallMarioStandingState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void BigPowerUp()
        {
            Exit();
            Mario.MarioActionState = new BigMarioCrouchingState(Mario);
            Mario.MarioActionState.Enter(this);
        }
        public override void FirePowerUp()
        {

        }
        public override void DeadPowerUp()
        {

        }

        public override void CrouchingDiscontinueTransition()
        {
            StandingTransition();
        }//when you exit crouch, release down key
        public override void FaceLeftDiscontinueTransition() { }//generic entering walk and run, face left then start walking, then start running
        public override void FaceRightDiscontinueTransition() { }
        public override void WalkingDiscontinueTransition() { }//decelerata and go to standing
        public override void RunningDiscontinueTransition() { }//decelerate and go to walking dis
        public override void JumpingDiscontinueTransition() { }//abort jump or force jump to disc bc you reached apex of jump

        public override void Update(GameTime gametime)
        {

        }

        //void Update(GameTime gametime, GraphicsDeviceManager graphics);

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            return Vector2.Zero;
        }
        // max velocity speed, clamp for each state speed
    }
}
