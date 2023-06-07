using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Levels;
using GameBuilder.Rendering;
using GameBuilder.User;
using GameBuilder.Particle;
using System;
using System.Drawing;

namespace GameBuilder.Scripts
{
    internal class PlayerController : Script
    {
        public RigidBody rigidBody;
        public Pupit pupit;
        public bool invincible = false;
        public void Kill()
        {
            if(!invincible) LevelLoading.LoadFromFile(LevelLoading.LastLoaded);
            invincible = true;
        }

        public override void Start()
        {
            rigidBody = (RigidBody)gameObject.getScript("RigidBody");
            rigidBody.Weight = 4;
        }


        public override void LateUpdate()
        {

        }

        public override void Update()
        {
            Nozzel.Tick();

            if (InputManager.leftStick.y == 1 && rigidBody.onGround)
            {
                rigidBody.velocity.y = -2.3f;
            }

            if (InputManager.leftStick.y == 1 && rigidBody.velocity.y < 0)
            {
                rigidBody.Weight = 6;
            }
            else
            {
                rigidBody.Weight = 12;
            }

            if (InputManager.leftStick.x == -1)
            {
                if(rigidBody.velocity.x > -1) rigidBody.velocity.x -= Time.DeltaTime * 4;
            }

            else if (InputManager.leftStick.x == 1)
            {
                if (rigidBody.velocity.x < 1) rigidBody.velocity.x += Time.DeltaTime * 4;
            }
            else
            {

                if(Math.Round(rigidBody.velocity.x * 100) == 0)
                {
                    rigidBody.velocity.x = 0;
                }

                if(rigidBody.velocity.x > 0)
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
