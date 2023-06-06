using GameBuilder._Math;
using GameBuilder.Scripts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Rendering
{
    internal class PlayerDrawing
    {
        public static Vector armJoinedOne;
        public static Vector Hand;

        static int WeastHeight = 0;
        static int HeadHeight = 0;
        static int ArmHight = 0;
        static int BodyHeight = 0;

        static Pen pen = new Pen(new SolidBrush(Color.Red));

        public static Bitmap GetPlayerSprite(PlayerController controller)
        {
            Bitmap map = new Bitmap(16, 25);
            Graphics graphics = Graphics.FromImage(map);

            Breath();
            DrawLeggs(graphics, controller);
            DrawBody(graphics);
            DrawHead(graphics);

            return map;
        }

        static float time;

        static void Breath()
        {
            time += Time.DeltaTime;

            WeastHeight = (int) (13 + (Math.Cos(time * 2) * 2));
            HeadHeight = (int) (4 + (Math.Cos(time * 2) * 2));
            ArmHight = (int) (9 + (Math.Cos(time * 2) * 2));
            BodyHeight = (int) (7 + (Math.Cos(time * 2) * 2));

            HeadHeight = (int)(Math.Cos(time * 2) * 2);
        }

        static void DrawHead(Graphics map)
        {
            map.DrawEllipse(pen, 4, 1 + HeadHeight, 6, 6);
        }


        static void DrawBody(Graphics map)
        {
            map.DrawImage(SpriteManager.loadSprite(@"player\body.png").image, 4, BodyHeight);
        }

        static float WalkCycle;
        static void DrawLeggs(Graphics map, PlayerController controller)
        {

            if (controller.rigidBody.velocity.x == 0) WalkCycle = 0;

            WalkCycle -= (Time.DeltaTime * controller.rigidBody.velocity.x) * 10;

            map.DrawLine(pen, 7, WeastHeight, ((float)Math.Cos(WalkCycle) * 3) + 7, 25 - ((float)Math.Sin(WalkCycle) * 6));

            map.DrawLine(pen, 7, WeastHeight, ((float)Math.Cos((WalkCycle) + 3f) * 3) + 7, 25 - ((float)Math.Sin((WalkCycle) + 3f) * 6));
        }
    }
}
