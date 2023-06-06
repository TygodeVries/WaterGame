using GameBuilder._Math;
using GameBuilder.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Physics
{
    internal class CollisionCorrection
    {
        public static void CorrectUp(RigidBody body)
        {
            if (body.velocity.y > -0.1f) return;

            Vector collisionPoint = body.getCollisionPoints()[4];
            if (ColliderDetection.PointIsOnCollider(collisionPoint - new Vector(3, 0), body))
            {
                body.velocity.y = 0;
                body.gameObject.posistion.y += 1;
            }

            Vector collisionPoint2 = body.getCollisionPoints()[5];
            if (ColliderDetection.PointIsOnCollider(collisionPoint2 - new Vector(-3, 0), body))
            {
                body.velocity.y = 0;
            }
        }

        public static void CorrectLeft(RigidBody body)
        {
            for (int i = 0; i < 25; i += 4)
            {

                float CorrectionDistance = PhysicsEngine.ShootRaycastLenght(body.getCollisionPoints()[0] - new Vector(-5, i), new Vector(-1, 0), body, 5);
                if (CorrectionDistance == -1) continue;

                CorrectionDistance = (int)Math.Round(CorrectionDistance);
                if (CorrectionDistance - 5 == 0) continue;

                CorrectionDistance += 0.5f;

                body.gameObject.posistion.x -= CorrectionDistance - 5;
                return;
            }

        }

        public static void CorrectRight(RigidBody body)
        {
            for (int i = 0; i < 25; i += 4)
            {
                float CorrectionDistance = PhysicsEngine.ShootRaycastLenght(body.getCollisionPoints()[3] - new Vector(5, i), new Vector(1, 0), body, 5);
                if (CorrectionDistance == -1) continue;

                CorrectionDistance = (int)Math.Round(CorrectionDistance);
                if (CorrectionDistance - 5 == 0) continue;

                CorrectionDistance += 0.5f;

                body.gameObject.posistion.x += CorrectionDistance - 5;
                return;
            }
        }




        public static void CorrectDown(RigidBody body)
        {
            float a = PhysicsEngine.ShootRaycastLenght(body.getCollisionPoints()[1] - new Vector(0, 10), new Vector(0, 1), body, 10);
            float b = PhysicsEngine.ShootRaycastLenght(body.getCollisionPoints()[2] - new Vector(0, 10), new Vector(0, 1), body, 10);

            float CorrectionDistance = -1;
            if (a > b) CorrectionDistance = a;
            else CorrectionDistance = b;
            
            if (CorrectionDistance == -1) return;
            
            CorrectionDistance = (int)Math.Round(CorrectionDistance);
            if (CorrectionDistance - 10 == 0) return;

            Console.WriteLine("Corrected collision by: y=" + (CorrectionDistance - 10)) ;

            body.gameObject.posistion.y += CorrectionDistance - 10;
        }
    }
}
