using GameBuilder._Math;
using GameBuilder.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Rendering
{
    internal class Background
    {
        public static void DrawBackground(Graphics graphics)
        {
            Vector cameraLocation = new Vector(Camera.x, Camera.y);
            Image backgroundSprite = (SpriteManager.loadSprite("\\generic\\background.png")).image;


            graphics.DrawImage(backgroundSprite, new Point(0, 0)); 
        }
    }
}
