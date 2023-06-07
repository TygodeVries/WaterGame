using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Particle;
using GameBuilder.Physics;
using GameBuilder.Rendering;
using GameBuilder.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Levels
{
    internal class ObjectLoader
    {
        public static void LoadCoinAt(int x, int y)
        {
            GameObject o = new GameObject();
            o.sprite = SpriteManager.loadSprite("generic\\coin.png");
            o.renderingOffset = new Vector(0, -13);
            o.posistion = new Vector(x * 16, y * 16);

            Collider collider = new Collider();
            collider.bottomLeft = o.posistion + new Vector(0, 16);
            collider.topRight = o.posistion + new Vector(16, 0);
            collider.isTrigger = true;

            o.scripts.Add(collider);
            o.scripts.Add(new Coin());
            o.inizilize();

            PhysicsEngine.AddCollider(collider);
        }

        public static void LoadNpcAt(int x, int y, string version)
        {
            GameObject o = new GameObject();
            o.sprite = SpriteManager.loadSprite("npc\\krokodil.png");
            o.renderingOffset = new Vector(0, -13);
            o.posistion = new Vector(x * 16, y * 16);

            Collider collider = new Collider();
            collider.bottomLeft = o.posistion + new Vector(0, 16);
            collider.topRight = o.posistion + new Vector(16, 0);
            collider.isTrigger = true;

            o.scripts.Add(collider);
            o.scripts.Add(new Character());
            o.inizilize();

            PhysicsEngine.AddCollider(collider);
        }
        public static void LoadSpikeAt(int x, int y)
        {
            GameObject o = new GameObject();
            o.sprite = SpriteManager.loadSprite("generic\\spike.png");
            o.posistion = new Vector(x * 16, y * 16);

            Collider collider = new Collider();
            collider.bottomLeft = o.posistion + new Vector(0, 16);
            collider.topRight = o.posistion + new Vector(16, 0);
            collider.isTrigger = true;

            o.scripts.Add(collider);
            o.scripts.Add(new Spike());
            o.inizilize();

            PhysicsEngine.AddCollider(collider);
        }
        public static void LoadBouncePadAt(int x, int y)
        {
            GameObject o = new GameObject();
            o.sprite = SpriteManager.loadSprite("generic\\bouncepad.png");
            o.posistion = new Vector(x * 16, y * 16);

            o.layer = 2;

            Collider collider = new Collider();
            collider.bottomLeft = o.posistion + new Vector(0, 16);
            collider.topRight = o.posistion + new Vector(16, 0);
            collider.isTrigger = true;

            o.scripts.Add(collider);
            o.scripts.Add(new Spring());
            o.inizilize();

            PhysicsEngine.AddCollider(collider);
        }

        public static void LoadDecortationAt(int x, int y, string type)
        {
            GameObject o = new GameObject();

            o.sprite = SpriteManager.loadSprite($"tileset\\{type}\\decorations\\1.png");
            o.posistion = new Vector(x * 16, y * 16);

            o.inizilize();
        }

        public static void LoadGroundAt(int x, int y, string type, bool topOpen)
        {
            GameObject o = new GameObject();
            if (topOpen)
            {
                o.sprite = SpriteManager.loadSprite($"tileset\\{type}\\top.png");
                LoadDecortationAt(x, y - 1, type);
            }
            else
            {
                o.sprite = SpriteManager.loadSprite($"tileset\\{type}\\fill.png");
            }
            o.posistion = new Vector(x * 16, y * 16);

            Collider collider = new Collider();
            collider.bottomLeft = o.posistion + new Vector(0, 16);
            collider.topRight = o.posistion + new Vector(16, 0);

            o.scripts.Add(collider);
            o.inizilize();

            PhysicsEngine.AddCollider(collider);
        }

        public static void LoadFillerAt(int x, int y, string type)
        {
            GameObject o = new GameObject();
            o.sprite = SpriteManager.loadSprite($"tileset\\{type}\\fill.png");

            o.posistion = new Vector(x * 16, y * 16);

            o.inizilize();
        }

        public static void LoadPlayerAt(int x, int y)
        {
            Console.WriteLine("Loaded player at " + x + ", " + y);

            // Create player object
            GameObject gobject = new GameObject();
            gobject.name = "player";
            gobject.layer = 0;

            Camera.tracking = gobject;

            // Load player sprite
            gobject.size = new Vector(16f, 25f); 

            // Create player controller
            PlayerController c = new PlayerController();
            gobject.renderingOffset = new Vector(0, 2);

            Main.playerController = c;

            // Add the PlayerController script to the gameObject
            gobject.scripts.Add(c);

            // Load up player animations
  
            /*
            Pupit pupit = new Pupit();
            pupit.ContentRoot = "player";
            gobject.scripts.Add(pupit);
            */


            // Add body.
            RigidBody body = new RigidBody();
            gobject.scripts.Add(body);

            // Update player posistion
            gobject.posistion = new Vector(x * 16, y * 16);

            // Inizilize gameobject to renderEngine.
            gobject.inizilize();
        }

    }
}
