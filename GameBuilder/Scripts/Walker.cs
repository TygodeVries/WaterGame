using GameBuilder._Math;
using GameBuilder.Levels;
using GameBuilder.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GameBuilder.Scripts
{
    internal class Walker : Script
    {
        RigidBody rigidBody;
        Pupit pupit;
        Collider collider;
        public override void LateUpdate()
        {
            
        }

        public override void Start()
        {
            rigidBody = (RigidBody) gameObject.getScript("RigidBody");
            collider = (Collider)gameObject.getScript("Collider");
            pupit = (Pupit)gameObject.getScript("Pupit");
        }

        float walkspeed = 0.4f;
        bool walkingLeft = false;

        float attackEvery = 5;

        float TimeSinceLastAttack = 0;
        float attackTime = 0;

        GameObject LastTong;


        public override void Update()
        {
            if (walkingLeft) rigidBody.velocity = new _Math.Vector(-walkspeed, rigidBody.velocity.y);
            else rigidBody.velocity = new _Math.Vector(walkspeed, rigidBody.velocity.y);

            float lenght = 0;
            Vector v = new Vector();

            if (attackTime > 0)
            {
                walkspeed = 0;
                if (walkingLeft) pupit.SetAnimationState("attack_left");
                if (!walkingLeft) pupit.SetAnimationState("attack_right");


                if (attackTime == 1.2f)
                {
                    GameObject gameObject = new GameObject();

                    Pupit tongpupit = new Pupit();
                    tongpupit.ContentRoot = "tong";

                    gameObject.scripts.Add(tongpupit);

 //                   gameObject.scripts.Add(new Tong());

                    if (!walkingLeft)
                        gameObject.posistion = this.gameObject.posistion + new Vector(16, 12);
                    else
                    {
                        gameObject.posistion = this.gameObject.posistion + new Vector(-48, 12);
                    }
                    gameObject.inizilize();

                    LastTong = gameObject;

                    if (walkingLeft)
                        tongpupit.SetAnimationState("left");

                    if (!walkingLeft)
                        tongpupit.SetAnimationState("right");
                }
            }
            else
            {
                if (LastTong != null)
                {
                    GameObject.Destroy(LastTong);
                    LastTong = null;
                }

                walkspeed = 0.4f;
                if (!walkingLeft)
                {
                    pupit.SetAnimationState("walk_right");
                    v = new _Math.Vector(1, 0);
                }
                else
                {
                    pupit.SetAnimationState("walk_left");
                    v = new _Math.Vector(-1, 0);
                }
            }

            attackTime -= Time.DeltaTime;

            if (TimeSinceLastAttack > attackEvery)
            {
                TimeSinceLastAttack = 0;
                attackTime = 1.2f;
            }

            TimeSinceLastAttack += Time.DeltaTime;

            lenght = PhysicsEngine.ShootRaycastLenght(gameObject.posistion + new Vector(8, 8), v, rigidBody, 64);

            if (lenght < 10 && lenght != -1) walkingLeft = !walkingLeft;
        }
    }
}
