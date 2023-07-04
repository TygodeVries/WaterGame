using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Levels;
using GameBuilder.Scripts;
using GameBuilder.Particle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices.ComTypes;

namespace GameBuilder.Rendering
{
    internal class RenderingEngine
    {
        static bool drawColliders = false;

        static public int X = (int)Math.Round(16f * 1.4f);
        static public int Y = (int)Math.Round(9f * 1.4f);

        public static Level CurrentLoadedLevel;
        static SolidBrush drawBrush;
        static Font drawFont;

        public static Bitmap bitmapRender16 = new Bitmap(2, 2);

        public static void Start()
        {
            Debug.SendDebugMessage($"Set game size to {X * 16}, {Y * 16}");
            drawBrush = new SolidBrush(Color.Black);
            drawFont = new Font("Raster Fonts", 6);
        }
        static void RenderLayer(int i, Graphics graphics16)
        {
            try
            {
                foreach (GameObject gameObject in CurrentLoadedLevel.Objects)
                {
                    if (!gameObject.IsVisible) continue;
                    if (gameObject.layer != i) continue;

                    if (gameObject.sprite == null || gameObject.sprite.image == null)
                    {
                        continue;
                    }

                    Vector v = WorldPointToScreen(gameObject.posistion + gameObject.renderingOffset);

                    if (v.x > bitmapRender16.Width) continue;
                    if (v.x < -16) continue;

                    if (v.y > bitmapRender16.Height) continue;
                    if (v.y < -16) continue;

                    Image render = gameObject.sprite.image;
                    graphics16.DrawImage(render, (int)v.x, (int)v.y);

                    Text text = (Text)gameObject.getScript("Text");
                    if (text != null)
                    {
                        graphics16.DrawString(text.text, drawFont, drawBrush, new Rectangle((int)WorldPointToScreen(gameObject.posistion).x, (int)WorldPointToScreen(gameObject.posistion).y, 64, 32));
                    }
                }
            } catch(Exception e)
            {
                Debug.SendErrorMessage("Error drawing game-object: " + e);
            }
        }

        public static void RenderFrame()
        {
            try
            {
                if (CurrentLoadedLevel == null || LevelLoading.Loading)
                {
                    return;
                }

                bitmapRender16 = new Bitmap(X * 16, Y * 16);

                Graphics graphics16 = Graphics.FromImage(bitmapRender16);
                graphics16.Clear(Color.Black);

                // Pass one, Render background
                Background.DrawBackground(graphics16);

                // Pass two, Render Objects
                for (int i = 0; i < 3; i++)
                {
                    RenderLayer(i, graphics16);
                }

                // Pass three, Render Particles
                foreach (GameObject particle in CurrentLoadedLevel.Particles)
                {
                    ParticleData particleData = particle.particleData;
                    Vector location = WorldPointToScreen(particle.posistion);

                    if (location.x > bitmapRender16.Width - 1) continue;
                    if (location.x < 0) continue;

                    if (location.y > bitmapRender16.Height - 1) continue;
                    if (location.y < 0) continue;

                    bitmapRender16.SetPixel((int)location.x, (int)location.y, particleData.color);
                }

                if(drawColliders)
                {
                    foreach (List<Collider> colliderList in Physics.PhysicsEngine.ActiveColliders.Values)
                    {
                        foreach (Collider collider in colliderList)
                        {
                            drawCollider(collider, graphics16);
                        }
                    }
                    foreach (Collider collider in Physics.PhysicsEngine.alwaysLoaded)
                    {
                        drawCollider(collider, graphics16);
                    }

                }


                // Render all graphics to the screen
                Graphics targetgraphics = Window.buffer.Graphics;
                targetgraphics.DrawImage(bitmapRender16, 0, 0, Window.windowWidth, Window.windowHeight);
                Window.buffer.Render();
            }
            catch (Exception e)
            {
                if((e+"").Contains("System.ArgumentException: Parameter is not valid."))
                {
                    Environment.Exit(0);
                }
                else
                Debug.SendFatalErrorMessage(e +"");
            }
        }



        public static float CameraX = 0;
        public static float CameraY = 0;

        public static void drawCollider(Collider collider, Graphics graphics16)
        {
            Vector bottemLeft = WorldPointToScreen(collider.bottomLeft);
            Vector topRight = WorldPointToScreen(collider.topRight);

            SolidBrush brush = new SolidBrush(Color.Orange);

            if (collider.isTrigger)
            {
                brush = new SolidBrush(Color.PowderBlue);
            }

            if (collider.gameObject.getScript("Spike") != null)
            {
                brush = new SolidBrush(Color.Red);
            }

            graphics16.DrawRectangle(new Pen(brush, 1), new Rectangle((int)bottemLeft.x, (int)topRight.y, (int)(topRight.x - bottemLeft.x), (int)(bottemLeft.y - topRight.y)));
        }

        public static Vector WorldPointToScreen(Vector vector)
        {
            float ax = vector.x;
            float ay = vector.y;

            ax -= CameraX;
            ay -= CameraY;
            return new Vector((float)Math.Floor(ax), (float)Math.Floor(ay));
        }
    }
}