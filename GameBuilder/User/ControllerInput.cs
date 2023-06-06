using GameBuilder.Game;
using SharpDX.XInput;
using System;
using System.Threading;

namespace GameBuilder.User
{
    internal class ControllerInput
    {

        static Controller controller;


        public static void Rumble(float left, float right)
        {
            Vibration v = new Vibration();
            v.LeftMotorSpeed = 4000;
            v.RightMotorSpeed = 4000;
            controller.SetVibration(v);
        }

        public static void LookForControllers()
        {
            // Initialize XInput
            var controllers = new[] { new Controller(UserIndex.One), new Controller(UserIndex.Two), new Controller(UserIndex.Three), new Controller(UserIndex.Four) };

            // Get 1st controller available
            controller = null;
            foreach (var selectControler in controllers)
            {
                if (selectControler.IsConnected)
                {
                    controller = selectControler;
                    break;
                }
            }

            if(controller == null)
            {
                Thread.Sleep(1000);
                LookForControllers();
            }

            Vibration v = new Vibration();
            v.LeftMotorSpeed = 4000;
            v.RightMotorSpeed = 4000;
            controller.SetVibration(v);


            State state;
            controller.GetState(out state);

            while(controller.IsConnected)
            {
                controller.GetState(out state);

                JoystickLeftX = (float) state.Gamepad.LeftThumbX / 32768f;
                JoystickLeftY = (float)state.Gamepad.LeftThumbY / 32768f;


                JoystickRightX = (float)state.Gamepad.RightThumbX/ 32768f;
                JoystickRightY = (float)state.Gamepad.RightThumbY / 32768f;

                GamepadButtonFlags buttonFlags = state.Gamepad.Buttons;

                A = (buttonFlags == GamepadButtonFlags.A);

                Thread.Sleep(10);
            }

            Console.WriteLine("CONTROLLER DISCONNECTED!");
            LookForControllers();

            /*


            while (true)
            {
                stick.GetCurrentState(ref joystickState);
                A = joystickState.Buttons[0];
                X = joystickState.Buttons[2];
                JoystickLeftX = ((float)joystickState.X) / (65535f / 2f) - 1f;
                JoystickLeftY = ((float)joystickState.Y) / (65535f / 2f) - 1f;

                int m = 0;
                foreach (var i in stick.GetObjects())
                {

                    Console.WriteLine(m + " : " + i.Name);
                    m++;
                }
                // Wait a bit so we dont kill the game
                Thread.Sleep(40);
            }

            */
        }



        public static bool A;
        public static bool X;

        public static float JoystickLeftX;
        public static float JoystickLeftY;

        public static float JoystickRightX;
        public static float JoystickRightY;


    }
}
