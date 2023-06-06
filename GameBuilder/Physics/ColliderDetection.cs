using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Levels;
using GameBuilder.Rendering;
using GameBuilder.Scripts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Physics
{
    internal class ColliderDetection
    {
        public static bool BodyCollidingRight(RigidBody body)
        {
            Vector[] bodyCollisionPoints = body.getCollisionPoints().ToArray();

            for (int i = 3; i < 20; i += 4)
            {
                bool collision = ColliderDetection.PointIsOnCollider(bodyCollisionPoints[3] - new Vector(0, i), body);
                if (collision) return collision;
            }

            return false;
        }

        public static bool BodyCollidingLeft(RigidBody body)
        {
            Vector[] bodyCollisionPoints = body.getCollisionPoints().ToArray();

            for (int i = 3; i < 20; i += 4)
            {
                bool collision = ColliderDetection.PointIsOnCollider(bodyCollisionPoints[0] - new Vector(0, i), body);
                if(collision) return collision;
            }

            return false;
        }

        public static bool BodyCollidingDown(RigidBody body)
        {
            Vector[] bodyCollisionPoints = body.getCollisionPoints().ToArray();

            bool collisionLeft = ColliderDetection.PointIsOnCollider(bodyCollisionPoints[1], body);
            bool collisionRight = ColliderDetection.PointIsOnCollider(bodyCollisionPoints[2], body);

            return collisionLeft || collisionRight;
        }

        /// <summary>
        /// Detect if a point is inside a collider.
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <param name="body">The body of an physics object requesting the detection.</param>
        /// <returns></returns>
       


        public static bool PointIsOnCollider(Vector point, RigidBody body)
        {

            if (PhysicsEngine.ActiveColliders.ContainsKey((int)Math.Round(point.x / 16f)))
                foreach (Collider collider in PhysicsEngine.ActiveColliders[(int)Math.Round(point.x / 16f)])
                {
                    if (body.collider == collider) continue;

                    if (PointInSpesificCollider(point.x, point.y, collider))
                    {
                        if (body == Main.playerController.rigidBody) collider.onCollision();
                        if (!collider.isTrigger) return true;
                    }
                }

            if (PhysicsEngine.ActiveColliders.ContainsKey((int)Math.Round(point.x / 16f) - 1))
                foreach (Collider collider in PhysicsEngine.ActiveColliders[(int)Math.Round(point.x / 16f) - 1])
                {
                    if (body.collider == collider) continue;

                    if (PointInSpesificCollider(point.x, point.y, collider))
                    {
                        if(body == Main.playerController.rigidBody) collider.onCollision();
                        if (!collider.isTrigger) return true;
                    }
                }

            if (PhysicsEngine.ActiveColliders.ContainsKey((int)Math.Round(point.x / 16f) + 1))
                foreach (Collider collider in PhysicsEngine.ActiveColliders[(int)Math.Round(point.x / 16f) + 1])
                {
                    if (body.collider == collider) continue;

                    if (PointInSpesificCollider(point.x, point.y, collider))
                    {
                        if (body == Main.playerController.rigidBody) collider.onCollision();
                        if (!collider.isTrigger) return true;
                    }
                }

            return false;
        }
        private static bool PointInSpesificCollider(float x, float y, Collider collider)
        {
            Vector bottomLeft = collider.bottomLeft;
            Vector topRight = collider.topRight;

            bool inX = false;
            bool inY = false;

            if (x >= bottomLeft.x && x <= topRight.x)
                inX = true;

            if (y >= topRight.y && y <= bottomLeft.y)
                inY = true;

            return (inX && inY);
        }
    }
}
