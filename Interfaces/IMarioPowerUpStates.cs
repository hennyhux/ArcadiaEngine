﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace GameSpace.Interfaces
{
    public interface IMarioPowerUpStates
    {

        //Kinematics PreviousKinematics { get; set; }
        public IMarioPowerUpStates previousPowerUpState { get; set; }

        void Enter(IMarioPowerUpStates previousPowerUpState);
        void Exit();

        void smallMarioTransformation();
        void bigMarioTransformation();
        void fireMarioTransformation();
        void DeadTransition();

        /*void StandingTransition();
        void CrouchingTransition();
        void WalkingTransition();
        void RunningTransition();//Longer you hold running you increase velocity and speed of animation
        void JumpingTransition();
        void FallingTransition();

        void FaceLeftTransition();
        void FaceRightTransition();

        void CrouchingDiscontinueTransition();//when you exit crouch, release down key
        void FaceLeftDiscontinueTransition();//generic entering walk and run, face left then start walking, then start running
        void FaceRightDiscontinueTransition();
        void WalkingDiscontinueTransition();//decelerata and go to standing
        void RunningDiscontinueTransition();//decelerate and go to walking dis
        void JumpingDiscontinueTransition();//abort jump or force jump to disc bc you reached apex of jump*/

        void Update(GameTime gametime);
        //void Update(GameTime gametime, GraphicsDeviceManager graphics);

        //Vector2 ClampVelocity(Vector2 velocity); // max velocity speed, clamp for each state speed
    }
}
