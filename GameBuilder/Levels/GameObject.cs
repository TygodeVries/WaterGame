using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Physics;
using GameBuilder.Rendering;
using GameBuilder.Scripts;
using GameBuilder.Particle;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GameBuilder.Levels
{
    internal class GameObject
    {
        public static void DeleteQueue()
        {
            for (int i = 0; i < scedualedForDeletion.Count; i++)
            {
                GameObject obj = scedualedForDeletion[i];

                Collider collider = (Collider)obj.getScript("Collider");

                if (collider != null)
                {
                    foreach (List<Collider> colliders in PhysicsEngine.ActiveColliders.Values)
                    {
                        colliders.Remove(collider);
                    }
                }

                RigidBody body = (RigidBody)obj.getScript("RigidBody");

                if (body != null)
                {
                    PhysicsEngine.bodies.Remove(body);
                }

                RenderingEngine.CurrentLoadedLevel.Objects.Remove(obj);
                RenderingEngine.CurrentLoadedLevel.Particles.Remove(obj);
                obj.posistion = new Vector(10000, 10000);
            }

            scedualedForDeletion.Clear();
        }

        static List<GameObject> scedualedForDeletion = new List<GameObject>();

        public static void Destroy(GameObject obj)
        {
            scedualedForDeletion.Add(obj);
        }

        public int layer = 1;

        /// <summary>
        /// Data of a particle.
        /// If object is not a particle. its null.
        /// </summary>
        public ParticleData particleData = null;

        public bool IsVisible = true;

        public Sprite sprite = new Sprite();
        public Vector posistion;
        public Vector size;

        public string name = "";

        public Vector renderingOffset = new Vector();

        public void TickLate()
        {
            foreach (Script s in scripts)
            {
                s.LateUpdate();
            }
        }

        // Called when a new object is created
        public void inizilize()
        {
            foreach (Script s in scripts)
            {
                s.gameObject = this;
            }

            foreach (Script s in scripts)
            {
                s.Start();
            }
            // Add object to render engine
            RenderingEngine.CurrentLoadedLevel.Objects.Add(this);
        }

        public void inizilizeAsParticle()
        {
            foreach (Script s in scripts)
            {
                s.gameObject = this;
                s.Start();
            }
            // Add object to render engine
            RenderingEngine.CurrentLoadedLevel.Particles.Add(this);
        }

        public void tick()
        {
            foreach (Script s in scripts)
            {
                s.Update();
            }
        }



        public List<Script> scripts = new List<Script>();


        /// <summary>
        /// Returns null if not found.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Script getScript(string type)
        {
            foreach (Script script in scripts)
            {

                if (script.GetType().ToString().Equals("GameBuilder.Scripts." + type))
                {
                    return script;
                }
            }

            return null;
        }


        public static void TickLateGameObjects()
        {
            if (LevelLoading.Loading) return;
            if (RenderingEngine.CurrentLoadedLevel == null) return;
            if (RenderingEngine.CurrentLoadedLevel.Objects.Count == 0) return;

            foreach (GameObject g in RenderingEngine.CurrentLoadedLevel.Objects)
            {
                g.TickLate();
            }
        }
        public static void TickGameObjects()
        {
            if (LevelLoading.Loading) return;
            if (RenderingEngine.CurrentLoadedLevel == null) return;
            if (RenderingEngine.CurrentLoadedLevel.Objects.Count == 0) return;


            try
            {
                for (int i = 0; i < RenderingEngine.CurrentLoadedLevel.Objects.Count; i++)
                {
                    GameObject g = RenderingEngine.CurrentLoadedLevel.Objects[i];
                    g.tick();
                }

                for (int i = 0; i < RenderingEngine.CurrentLoadedLevel.Particles.Count; i++)
                {
                    if (i < RenderingEngine.CurrentLoadedLevel.Particles.Count)
                        RenderingEngine.CurrentLoadedLevel.Particles[i].tick();
                }

            }
            catch (Exception e)
            {
                Debug.SendFatalErrorMessage("Something went wrong ticking gameobjects! " + e);
            }
        }

        public static GameObject Find(string Name)
        {
            foreach (GameObject o in RenderingEngine.CurrentLoadedLevel.Objects)
            {
                if (o.name != "" && o.name == Name)
                {
                    return o;
                }
            }

            Debug.SendFatalErrorMessage("Cant find gameobject " + Name);

            return null;
        }
    }
}
