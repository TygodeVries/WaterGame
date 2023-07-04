using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Rendering;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GameBuilder.User
{
    internal class InputManager
    {
        public static bool usingController = false;

        public static Vector rightStick = new Vector(0, 0);
        public static Vector leftStick = new Vector(0, 0);

        public static bool jumpButton;
        public static bool sprayButton;
        public static bool launchButton;

        public static void Tick()
        {
            if (usingController) controller();
            else keyBoard();
        }


        public static bool jumpLastFrame;
        static bool launchLastFrame;

        static void controller()
        {
            float deadzone = 0.3f;
            sprayButton = ControllerInput.X;
            jumpButton = !jumpLastFrame && ControllerInput.A;
            launchButton = !launchLastFrame && ControllerInput.Y;

            jumpLastFrame = ControllerInput.A;
            launchLastFrame = ControllerInput.Y;


            rightStick = new Vector(ControllerInput.JoystickRightX, ControllerInput.JoystickRightY);
            if(rightStick.magnitude() > deadzone)
            {
                rightStick.normalize();
                rightStick = rightStick.Round();
                rightStick.normalize();
            }
            else
            {
                rightStick = new Vector(0, 0);
            }


            leftStick = new Vector(ControllerInput.JoystickLeftX, ControllerInput.JoystickLeftY);
            if (leftStick.magnitude() > deadzone)
            {
                leftStick.normalize();
            }
            else
            {
                leftStick = new Vector(0, 0);
            }
        }

        static void keyBoard()
        {
            if (KeyboardInput.GetKey(Keys.L)) rightStick.x = 1;
            else if (KeyboardInput.GetKey(Keys.J)) rightStick.x = -1;
            else rightStick.x = 0;

            if (KeyboardInput.GetKey(Keys.I)) rightStick.y = 1;
            else if (KeyboardInput.GetKey(Keys.K)) rightStick.y = -1;
            else rightStick.y = 0;

            if (KeyboardInput.GetKey(Keys.D)) leftStick.x = 1;
            else if (KeyboardInput.GetKey(Keys.A)) leftStick.x = -1;
            else leftStick.x = 0;

            if (KeyboardInput.GetKey(Keys.W)) leftStick.y = 1;
            else if (KeyboardInput.GetKey(Keys.S)) leftStick.y = -1;
            else leftStick.y = 0;

            sprayButton = KeyboardInput.GetKey(Keys.LShiftKey);
            jumpButton = KeyboardInput.GetKey(Keys.W);
        }
        
    }
}
