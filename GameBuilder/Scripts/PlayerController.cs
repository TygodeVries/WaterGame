using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Levels;
using GameBuilder.Rendering;
using GameBuilder.User;
using GameBuilder.Particle;
using System;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using GameBuilder.Audio;

namespace GameBuilder.Scripts
{
    internal class PlayerController : Script
    {
        public RigidBody rigidBody;
        public Pupit pupit;
        public bool invincible = false;

        public void Kill()
        {
            AudioPlayer.PlayAudioSource("dead.wav");
            if (!invincible) LevelLoading.LoadFromFile(LevelLoading.LastLoaded);
            invincible = true;
        }

        public override void Start()
        {
            rigidBody = (RigidBody)gameObject.getScript("RigidBody");
            rigidBody.Weight = 4;

            if(Main.respawnPos != null)
            {
                gameObject.posistion = Main.respawnPos.copy();
            }
        }


        public override void LateUpdate()
        {

        }


        bool onGroundLastFrame = false;
        void tickLanding()
        {
            if (rigidBody.onGround)
            {
                if (!onGroundLastFrame)
                {
                    ControllerInput.Rumble(5000, 5000, Time.DeltaTime * 2);
                }

                onGroundLastFrame = true;
                hasRocketFuel = true;
            }
            else
            {
                onGroundLastFrame = false;
            }
        }

        public bool hasRocketFuel = false;

        public override void Update()
        {

            if(rigidBody.velocity.x > 4)
            {
                rigidBody.velocity.x = 4;
            }
            if (rigidBody.velocity.x < -4)
            {
                rigidBody.velocity.x = -4;
            }



            tickLanding();


            Nozzel.Tick();

            if (hasRocketFuel && InputManager.launchButton)
            {

             //   rigidBody.noGravityCount = 0.5f;

                hasRocketFuel = false;

                Random rand = new Random();

                Vector direction = new Vector((float) Math.Round(InputManager.leftStick.x), (float) Math.Min(-0.3f, Math.Round(-InputManager.leftStick.y)));

                direction.normalize();

                for (int i = 0; i < 100; i++)
                {
                    Vector d = -direction.copy();

                    d.x += (float) rand.NextDouble() * 2 - 1;
                    d.y += (float) rand.NextDouble() * 2 - 1;

                    d.normalize();

                    Nozzel.SpawnParticle(rigidBody.gameObject.posistion + new Vector(6, 6), d);
                }


                direction.y *= 1.4f;
                rigidBody.velocity.x += direction.x * 3;
                rigidBody.velocity.y = direction.y * 3;

                ControllerInput.Rumble(60000, 60000, 0.2f);
            }


            if (rigidBody.velocity.x > 2.3f)
            {
                rigidBody.velocity.x -= Time.DeltaTime;
            }

            if (rigidBody.velocity.x < -2.3f)
            {
                rigidBody.velocity.x += Time.DeltaTime;
            }

            if (InputManager.jumpButton && rigidBody.onGround && !InputManager.sprayButton)
            {
                rigidBody.velocity.y = -2.3f;
            }

            if (InputManager.jumpLastFrame && rigidBody.velocity.y < 0 && !InputManager.sprayButton)
            {
                rigidBody.Weight = 6;
            }
            else
            {
                rigidBody.Weight = 12;
            }

            if (InputManager.leftStick.x < -0.3 && !InputManager.sprayButton)
            {
                if (rigidBody.velocity.x > -1) rigidBody.velocity.x -= Time.DeltaTime * 4;
            }

            else if (InputManager.leftStick.x > 0.3 && !InputManager.sprayButton)
            {
                if (rigidBody.velocity.x < 1) rigidBody.velocity.x += Time.DeltaTime * 4;
            }
            else
            {

                if (Math.Round(rigidBody.velocity.x * 100) == 0)
                {
                    rigidBody.velocity.x = 0;
                }

                if (rigidBody.velocity.x > 0)
                {
                    rigidBody.velocity.x -= Time.DeltaTime * 3;
                }
                else if (rigidBody.velocity.x < 0)
                {
                    rigidBody.velocity.x += Time.DeltaTime * 3;
                }
            }

            this.gameObject.sprite.image = PlayerDrawing.GetPlayerSprite(this);
        }

        public void OnCollision(Collider collider)
        {
            if (!collider.ColliderData.Contains("deadly")) return;
        }
    }
}
