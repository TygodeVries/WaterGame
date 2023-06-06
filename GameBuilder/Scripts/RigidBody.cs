using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameBuilder.Scripts
{
    internal class RigidBody : Script
    {
        public Vector velocity = new Vector();

        public bool onGround = true;

        public float Weight = 1;

        public bool ShouldCorrect = true;

        public override void LateUpdate()
        {

        }

        public float DownOffset = 2;

        public List<Vector> getCollisionPoints()
        {
            List<Vector> collisionPoints = new List<Vector>();

            float Indent = 1;

            // Point 1
            collisionPoints.Add(new Vector(gameObject.posistion.x - 1f, gameObject.posistion.y + gameObject.size.y));


            // Point 2
            collisionPoints.Add(new Vector(gameObject.posistion.x + Indent, gameObject.posistion.y + (gameObject.size.y + DownOffset)));

            // Point 3
            collisionPoints.Add(new Vector(gameObject.posistion.x + gameObject.size.x - Indent, gameObject.posistion.y + (gameObject.size.y + DownOffset)));

            // Point 4
            collisionPoints.Add(new Vector(gameObject.posistion.x + gameObject.size.x + 1, gameObject.posistion.y + gameObject.size.y));

            collisionPoints.Add(new Vector(gameObject.posistion.x + gameObject.size.x, gameObject.posistion.y ));

            collisionPoints.Add(new Vector(gameObject.posistion.x, gameObject.posistion.y));

            return collisionPoints;
        }

        public Collider collider;

        public override void Start()
        {
            PhysicsEngine.bodies.Add(this);
        }

        public override void Update()
        {
            if(velocity.x == 0)
            {
                gameObject.posistion.x = (float) Math.Round(gameObject.posistion.x);
            }
        }
    }
}
