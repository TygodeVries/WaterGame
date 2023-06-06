using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Physics;
using GameBuilder.Rendering;
using GameBuilder.Scripts;
using GameBuilder.Water;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GameBuilder.Levels
{
    internal class GameObject
    {
        public static void Destroy(GameObject obj)
        {
            RenderingEngine.CurrentLoadedLevel.Objects.Remove(obj);
            obj.posistion = new Vector(10000, 10000);
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


        public void Destroy()
        {
            // Handle Destoring object?
        }

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
                foreach (GameObject g in RenderingEngine.CurrentLoadedLevel.Objects)
                {
                    g.tick();
                }

                foreach (GameObject g in RenderingEngine.CurrentLoadedLevel.Particles)
                {
                    g.tick();
                }
            } catch(Exception e)
            {
                Console.WriteLine("Lost Tick.");
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
