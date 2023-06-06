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

            int offSet = (int) Math.Floor(cameraLocation.x / (float)backgroundSprite.Width);


            graphics.DrawImage(backgroundSprite, (int) Math.Floor(-(cameraLocation.x / 2) + (offSet * (backgroundSprite.Width))), 0);
            graphics.DrawImage(backgroundSprite, (int) Math.Floor(-(cameraLocation.x / 2) + backgroundSprite.Width + (offSet * (backgroundSprite.Width))), 0);
            graphics.DrawImage(backgroundSprite, (int) Math.Floor(-(cameraLocation.x / 2) - backgroundSprite.Width + (offSet * (backgroundSprite.Width))), 0);
        }
    }
}
