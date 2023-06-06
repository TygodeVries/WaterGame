using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Levels;
using GameBuilder.Physics;
using GameBuilder.Rendering;
using GameBuilder.Scripts;
using GameBuilder.User;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Water
{
    internal class Nozzel
    {
        static bool ShotLastFrame = false;

        static float SprayTime = 0;

        public static void Tick()
        {
            GameObject player = Main.playerController.gameObject;
            RigidBody playerBody = Main.playerController.rigidBody;
            Vector direction = new Vector(ControllerInput.JoystickRightX, -ControllerInput.JoystickRightY);

            SprayTime -= Time.DeltaTime;

            if (direction.magnitude() > 0.3 && !ShotLastFrame)
            {
                SprayTime = 0.2f;
                ShotLastFrame = true;
            }
            else if (direction.magnitude() < 0.3)
            {
                ShotLastFrame = false;
            }

            if (SprayTime >= 0)
            {
                direction.normalize();

                Random rng = new Random();

                for (int i = 0; i < 10; i++)
                {
                    Vector origin = player.posistion + new Vector((player.size.x / 2), (player.size.y / 2)) + (direction * 4);

                    float spread = 40; // lower is more. higher is less.
                    Vector particleDirection = (direction + (new Vector((float) rng.NextDouble() * 2 - 1, (float)rng.NextDouble() * 2 - 1) / spread));
                    SpawnParticle(origin, particleDirection);
                }

                playerBody.velocity += -direction / 10;
            }
            else if(direction.magnitude() < 0.3)
            {
                ShotLastFrame = false;
            }
        }

        public static void SpawnParticle(Vector location, Vector Direction)
        {
            GameObject particle = new GameObject();
            particle.particleData = new ParticleData(Color.FromArgb(255 / 2, 10, 10, 255));
            particle.posistion = location;
            particle.size = new Vector(1, 1);

            RigidBody rigidBody = new RigidBody();
            rigidBody.DownOffset = 0;
            rigidBody.ShouldCorrect = false;
            rigidBody.velocity = Direction;
            particle.scripts.Add(rigidBody);

            particle.inizilizeAsParticle();
        }
    }
}
