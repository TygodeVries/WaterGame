﻿using GameBuilder._Math;
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
            alwaysLoaded.Clear();
            bodies.Clear();
        }

        public static Dictionary<int, List<Collider>> ActiveColliders = new Dictionary<int, List<Collider>>();
        public static List<Collider> alwaysLoaded = new List<Collider>();


        public static List<RigidBody> bodies = new List<RigidBody>();

        public static void AddCollider(Collider collider, bool moving)
        {
            if (!moving)
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
            else
            {
                alwaysLoaded.Add(collider);
            }


        }

        static bool LoadingLastFrame = true;

        static Random rng = new Random();

        // Physics tick is called once a frame
        public static void Tick()
        {
            while(bodies.Count > 501)
            {
                RigidBody gmobj = bodies[rng.Next(0, bodies.Count)];
                if (gmobj.gameObject.name == "water")
                {
                    GameObject.Destroy(gmobj.gameObject);
                    bodies.Remove(gmobj);
                }
            }

            if (!LevelLoading.Loading && LoadingLastFrame)
            {
                Time.Tick();
                Time.Tick();
                Time.Tick();

                foreach (RigidBody rb in bodies)
                {
                    rb.velocity = new Vector(0, 0);
                }
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
                float maxFallSpeed = 2;
                if (rb.velocity.y < maxFallSpeed)
                {
                    rb.velocity.y += (Time.DeltaTime * rb.Weight);
                }
            }
        }

        // Apply the forces to all objects and move them if needed
        static void ApplyForces()
        {
            for (int i = 0; i < bodies.Count; i++)
            {
                if (!(i > bodies.Count))
                {
                    UpdateBodyVelocity(bodies[i]);

                    bodies[i].gameObject.posistion = bodies[i].gameObject.posistion + (bodies[i].velocity * (Time.DeltaTime * 100));
                    UpdateBodyVelocity(bodies[i]);
                }
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
