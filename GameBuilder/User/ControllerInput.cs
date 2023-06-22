using GameBuilder.Game;
using SharpDX.XInput;
using System;
using System.Threading;

namespace GameBuilder.User
{
    internal class ControllerInput
    {

        public static void Start()
        {
            Thread thr = new Thread(LookForControllers);
            thr.Start();
        }

        static Controller controller;
        public static void Rumble(ushort left, ushort right, int time)
        {
            Vibration v = new Vibration();
            v.LeftMotorSpeed = left;
            v.RightMotorSpeed = right;
            SharpDX.Result result = controller.SetVibration(v);

            Thread.Sleep(time);

            Vibration v2 = new Vibration();
            v2.LeftMotorSpeed = 0;
            v2.RightMotorSpeed = 0;
            SharpDX.Result result2 = controller.SetVibration(v2);
        }

        public static void LookForControllers()
        {
            InputManager.usingController = false;


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

            InputManager.usingController = true;

            Rumble(1000, 1000, 500);


            State state;
            controller.GetState(out state);

            while(controller.IsConnected)
            {
                controller.GetState(out state);

                JoystickLeftX = (float) state.Gamepad.LeftThumbX / 32768f;
                JoystickLeftY = (float) state.Gamepad.LeftThumbY / 32768f;


                JoystickRightX = (float)state.Gamepad.RightThumbX / 32768f;
                JoystickRightY = (float)state.Gamepad.RightThumbY / 32768f;


                GamepadButtonFlags buttonFlags = state.Gamepad.Buttons;

                A = (buttonFlags == GamepadButtonFlags.A);
                X = (buttonFlags == GamepadButtonFlags.X);
                Thread.Sleep(10);
            }

            Console.WriteLine("CONTROLLER DISCONNECTED!");
            LookForControllers();

        }



        public static bool A;
        public static bool X;

        public static float JoystickLeftX;
        public static float JoystickLeftY;

        public static float JoystickRightX;
        public static float JoystickRightY;


    }
}
