using GameBuilder._Math;
using GameBuilder.Audio;
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

namespace GameBuilder.Particle
{
    internal class Nozzel
    {
        static bool ShotLastFrame = false;

        static float SprayTime = 0;

        public static float SprayTimeLeft = 99999999;

        public static void Tick()
        {
            GameObject player = Main.playerController.gameObject;
            Vector direction = new Vector(InputManager.leftStick.x, -InputManager.leftStick.y);

            SprayTime -= Time.DeltaTime;

            if (direction.magnitude() > 0.3 && SprayTimeLeft > 0)
            {
                SprayTime = 0.2f;
            }

            if (SprayTime >= 0 && InputManager.sprayButton)
            {


                direction.normalize();

                SprayTime -= Time.DeltaTime;

                Random rng = new Random();

                for (int i = 0; i < 10; i++)
                {
                    Vector origin = player.posistion + new Vector((player.size.x / 2), (player.size.y / 2)) + (direction * 4f);

                    float spread = 40; // lower is more. higher is less.
                    Vector particleDirection = (direction + (new Vector((float) rng.NextDouble() * 2 - 1, (float)rng.NextDouble() * 2 - 1) / spread));
                    SpawnParticle(origin, particleDirection);
                }

               // playerBody.velocity += -direction / 10;
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
            particle.name = "water";
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
