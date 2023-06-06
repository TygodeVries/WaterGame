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
        public static bool UseController { get; } = true;

        static Dictionary<string, bool> lastFrameStatus = new Dictionary<string, bool>();


        public static bool GetKey(string button)
        {
            if (button == "Jump" && JumpButtonDown())
            {
                return true;
            }
            if(button == "Left" && LeftButtonDown())
            {
                return true;
            }
            if (button == "Right" && RightButtonDown())
            {
                return true;
            }
            if (button == "FastFall" && DownButtonDown())
            {
                return true;
            }

            return false;
        }

        public static bool GetKeyDown(string button)
        {
            if (!GetKey(button))
            {
                if (!lastFrameStatus.ContainsKey(button))
                {
                    lastFrameStatus.Add(button, false);
                }

                lastFrameStatus[button] = false;

                return false;
            }

            if (!lastFrameStatus.ContainsKey(button))
            {
                lastFrameStatus.Add(button, true);
                return true;
            }


            if(lastFrameStatus[button] == true)
            {
                return false;
            }
            else
            {
                lastFrameStatus[button] = true;
                return true;
            }
        }

        private static bool DownButtonDown()
        {
            if (UseController)
            {
                if (ControllerInput.JoystickLeftY > 0.25f)
                {
                    return true;
                }
            }
            else
            {
                if (KeyboardInput.GetKey(Keys.S))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool RightButtonDown()
        {
            if (UseController)
            {
                if (ControllerInput.JoystickLeftX > 0.25f)
                {
                    return true;
                }
            }
            else
            {
                if (KeyboardInput.GetKey(Keys.D))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool LeftButtonDown()
        {
            if (UseController)
            {
                if (ControllerInput.JoystickLeftX < -0.25f)
                {
                    return true;
                }
            }
            else
            {
                if (KeyboardInput.GetKey(Keys.A))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool JumpButtonDown()
        {
            if (UseController)
            {
                if (ControllerInput.A)
                {
                    return true;
                }
            }
            else
            {
                if (KeyboardInput.GetKey(Keys.W))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
