using GameBuilder._Math;
using GameBuilder.Game;
using SharpDX.Win32;
using SharpDX.XInput;
using System;
using System.IO;
using System.Threading;
using System.Windows.Media.Media3D;

namespace GameBuilder.User
{
    internal class ControllerInput
    {

        public static void Start()
        {
            Thread thr = new Thread(LookForControllers);
            thr.Start();
        }


        static float rumbleTimeLeft = 0;

        static Controller controller;
        public static void Rumble(ushort left, ushort right, float time)
        {
            if (controller == null) return;


            Vibration v = new Vibration();
            v.LeftMotorSpeed = left;
            v.RightMotorSpeed = right;
            SharpDX.Result result = controller.SetVibration(v);

            rumbleTimeLeft = time;
        }

        public static void Tick()
        {
            bool wasLeft = rumbleTimeLeft > 0;

            rumbleTimeLeft -= Time.DeltaTime;

            if(!(rumbleTimeLeft > 0) && wasLeft)
            {
                Vibration v = new Vibration();
                v.LeftMotorSpeed = 0;
                v.RightMotorSpeed = 0;
                SharpDX.Result result = controller.SetVibration(v);
            }
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

            Rumble(1000, 1000, 0.1f);


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
                Y = (buttonFlags == GamepadButtonFlags.Y);


                rightShoulder = state.Gamepad.RightTrigger > 100;
                Thread.Sleep((int) 16);
            }

            Console.WriteLine("CONTROLLER DISCONNECTED!");
            LookForControllers();

        }


        public static bool Y;
        public static bool A;
        public static bool X;

        public static float JoystickLeftX;
        public static float JoystickLeftY;

        public static float JoystickRightX;
        public static float JoystickRightY;

        public static bool rightShoulder;


    }
}
