using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GameBuilder.User
{
    internal class KeyboardInput
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int key);

        public static bool GetKey(Keys key)
        {


            if (GetAsyncKeyState((int)key) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
