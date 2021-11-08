﻿using GameSpace.EntityManaging;
using GameSpace.States.GameStates;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameSpace
{
    public class KeyboardInput : IController
    {

        private KeyboardState previousState;
        private protected CommandList commands;
        private protected CommandListStart commandsStart;
        private readonly ICommand executeCommand;
        private readonly Dictionary<Keys, ICommand> command;

        public KeyboardInput(GameRoot game)
        {
            previousState = new KeyboardState();

                commands = new CommandList(game);


  
                commandsStart = new CommandListStart(game);
            
        }

        // due to the lack of command design this is smelly... I smell the smelly smell 
        public void Update()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] keysPressed = currentKeyboardState.GetPressedKeys();
            GameRoot game = FinderHandler.GetInstance().FindGameRoot();

            foreach (Keys key in keysPressed)
            {
                if (!previousState.IsKeyDown(key))
                {
                    try
                    {
                        if (game.CurrentState is PlayingGameState) commands.GetCommand[key].Execute();

                        else if (game.CurrentState is StartGameState) commandsStart.GetCommand[key].Execute();

                        
                    }

                    catch (KeyNotFoundException)
                    {

                    }
                }
            }
            previousState = currentKeyboardState;
        }
    }
}
