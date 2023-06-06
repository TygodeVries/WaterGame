using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Levels;
using GameBuilder.Scripts;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameBuilder.Physics
{
    internal class PhysicsEngine
    {
        public static void Unload()
        {
            ActiveColliders.Clear();
            bodies.Clear();
        }

        public static Dictionary<int, List<Collider>> ActiveColliders = new Dictionary<int, List<Collider>>();

        public static List<RigidBody> bodies = new List<RigidBody>();

        public static void AddCollider(Collider collider)
        {
            List<Collider> list;

            if (!ActiveColliders.ContainsKey((int)Math.Round(collider.gameObject.posistion.x / 16f)))
            {
                list = new List<Collider>();
                ActiveColliders.Add((int)Math.Round(collider.gameObject.posistion.x / 16f), list);
            }
            else
            {
                list = ActiveColliders[(int)Math.Round(collider.gameObject.posistion.x / 16f)];
            }

            list.Add(collider);
        }

        static bool LoadingLastFrame = true;

        // Physics tick is called once a frame
        public static void Tick()
        {
            if (!LevelLoading.Loading && LoadingLastFrame)
            {
                Time.Tick();
                Time.Tick();
            }

            if (!LevelLoading.Loading)
            {
                ApplyGravity();
                ApplyForces();
                LoadingLastFrame = false;
            }
            else
            {
                LoadingLastFrame = true;
            }
        }

        // Apply grafity to all objects
        static void ApplyGravity()
        {
            foreach (RigidBody rb in bodies)
            {
                rb.velocity.y += (Time.DeltaTime * rb.Weight);
            }
        }

        // Apply the forces to all objects and move them if needed
        static void ApplyForces()
        {
            for (int i = 0; i < bodies.Count; i++)
            {

                UpdateBodyVelocity(bodies[i]);
                bodies[i].gameObject.posistion = bodies[i].gameObject.posistion + (bodies[i].velocity * (Time.DeltaTime * 100));

            }

        }

        public static float ShootRaycastLenght(Vector origin, Vector direction, RigidBody ignore, int lenght)
        {
            float index = 0;
            bool hitSucess = false;

            while (index < lenght && !hitSucess)
            {
                hitSucess = ColliderDetection.PointIsOnCollider(origin + (direction * index), ignore);
                index += 0.1f;
            }

            if (index < lenght) return index;
            else return -1;
        }

        public static Vector ShootRaycast(Vector origin, Vector direction, RigidBody ignore, int lenght)
        {
            float index = 0;
            bool hitSucess = false;

            while (index < lenght && !hitSucess)
            {
                hitSucess = ColliderDetection.PointIsOnCollider(origin + (direction * index), ignore);
                index += 0.1f;
            }

            if (index < lenght) return origin + (direction * index);
            else return null;
        }

        static void UpdateBodyVelocity(RigidBody body)
        {
            CollisionCorrection.CorrectUp(body);

            if (body.velocity.y >= 0 && ColliderDetection.BodyCollidingDown(body))
            {
                body.onGround = true;
                body.velocity.y = 0;
                if (body.ShouldCorrect)
                    CollisionCorrection.CorrectDown(body);
            }
            else
                body.onGround = false;

            if (body.velocity.x < 0 && ColliderDetection.BodyCollidingLeft(body))
            {
                body.velocity.x = 0;
                if (body.ShouldCorrect)
                    CollisionCorrection.CorrectLeft(body);
            }

            if (body.velocity.x > 0 && ColliderDetection.BodyCollidingRight(body))
            {
                body.velocity.x = 0;
                if (body.ShouldCorrect)
                    CollisionCorrection.CorrectRight(body);
            }
        }
    }
}
