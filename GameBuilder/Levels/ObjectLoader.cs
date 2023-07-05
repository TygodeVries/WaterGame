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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Xml.Linq;

namespace GameBuilder.Levels
{
    internal class ObjectLoader
    {
        public static void LoadCoinAt(int x, int y)
        {
            GameObject o = new GameObject();
            o.sprite = SpriteManager.loadSprite("generic\\coin.png");
            o.posistion = new Vector(x * 16, y * 16);

            Collider collider = new Collider(new Vector(16, 16));
            collider.isTrigger = true;

            o.scripts.Add(collider);
            o.scripts.Add(new Coin());
            o.inizilize();

            PhysicsEngine.AddCollider(collider, false);
        }

        public static void LoadBuildingAt(int x, int y, string type)
        {
            GameObject o = new GameObject();
            o.sprite = SpriteManager.loadSprite($"buildings\\{type}.png");
            o.posistion = new Vector(x * 16, (y * 16) - 55 + 16);
            o.inizilize();
        }

        public static void LoadTreeAt(int x, int y)
        {
            GameObject o = new GameObject();
            o.sprite = SpriteManager.loadSprite("generic\\tree_dood.png");
            o.renderingOffset = new Vector(0, -13);
            o.posistion = new Vector(x * 16, y * 16);

            Collider collider = new Collider(new Vector(5, 64));
            collider.isTrigger = true;
            collider.offset = new Vector(32, 0);
            o.scripts.Add(collider);
            o.scripts.Add(new Waterable());
            o.scripts.Add(new Tree());
            o.inizilize();

            PhysicsEngine.AddCollider(collider, false);
        }
        public static void LoadVineAt(int x, int y, bool lastOne)
        {
            GameObject o = new GameObject();
            if(!lastOne) o.sprite = SpriteManager.loadSprite("decoration\\vine.png");
            else o.sprite = SpriteManager.loadSprite("decoration\\vine_end.png");
            o.posistion = new Vector(x * 16, y * 16);

            o.scripts.Add(new WindEffected());

            o.inizilize();
        }
        public static void LoadNpcAt(int x, int y, string text, string type)
        {
            GameObject o = new GameObject();
            if(type == "salamanderman")
                o.sprite = SpriteManager.loadSprite("npc\\krokodil.png");
            if(type == "andereman")
                o.sprite = SpriteManager.loadSprite("npc\\drup.png");
            o.renderingOffset = new Vector(0, -13);
            o.posistion = new Vector(x * 16, y * 16);

            Collider collider = new Collider(new Vector(16, 16));
            collider.bottomLeft = o.posistion + new Vector(0, 16);
            collider.topRight = o.posistion + new Vector(16, 0);
            collider.isTrigger = true;

            o.scripts.Add(collider);
            o.scripts.Add(new Character(text));
            o.inizilize();

            PhysicsEngine.AddCollider(collider, false);
        }
        public static void LoadSpikeAt(int x, int y)
        {
            GameObject o = new GameObject();
            o.sprite = SpriteManager.loadSprite("generic\\spike.png");
            o.posistion = new Vector(x * 16, y * 16);

            Collider collider = new Collider(new Vector(16, 1));
            collider.offset = new Vector(0, 15);
            collider.isTrigger = true;

            o.scripts.Add(collider);
            o.scripts.Add(new Spike());
            o.inizilize();

            PhysicsEngine.AddCollider(collider, false);
        }
        public static void LoadBouncePadAt(int x, int y)
        {
            GameObject o = new GameObject();
            o.sprite = SpriteManager.loadSprite("generic\\bouncepad.png");
            o.posistion = new Vector(x * 16, y * 16);

            o.layer = 2;

            Collider collider = new Collider(new Vector(14, 3));
            collider.offset = new Vector(1, 12);
            collider.isTrigger = true;

            o.scripts.Add(collider);
            o.scripts.Add(new Spring());
            o.inizilize();

            PhysicsEngine.AddCollider(collider, false);
        }
        public static void LoadDecortationAt(int x, int y, string type)
        {
            if (Math.Cos(x / 2f) > 0)
            {
                if (Math.Cos(x / 1f) > 0)
                {
                    return;
                }
            }
            
            GameObject o = new GameObject();

            Random rng = new Random();

            o.sprite = SpriteManager.loadSprite($"tileset\\{type}\\decorations\\1.png");
            o.scripts.Add(new WindEffected());
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

            Collider collider = new Collider(new Vector(16, 16));
            collider.bottomLeft = o.posistion + new Vector(0, 16);
            collider.topRight = o.posistion + new Vector(16, 0);

            o.scripts.Add(collider);
            o.inizilize();

            PhysicsEngine.AddCollider(collider, false);
        }
        public static void LoadFillerAt(int x, int y, string type)
        { 

            GameObject o = new GameObject();
            o.sprite = SpriteManager.loadSprite($"tileset\\{type}\\fill.png");

            o.posistion = new Vector(x * 16, y * 16);

            o.inizilize();
        }
        public static void LoadFireAt(int x, int y)
        {
            GameObject fireObject = new GameObject();
            fireObject.posistion = new Vector(x * 16, y * 16);

            fireObject.renderingOffset = new Vector(0, -8);


            Collider collider = new Collider(new Vector(14, 7));
            collider.offset = new Vector(1, 8);
            collider.LiveUpdate = false;
            collider.isTrigger = true;
            fireObject.scripts.Add(collider);

            fireObject.scripts.Add(new Waterable());

            Pupit pupit = new Pupit();
            pupit.ContentRoot = "fire";
            fireObject.scripts.Add(pupit);

            Fire fire = new Fire();
            fire.target = fireObject;
            fireObject.scripts.Add(fire);


            fireObject.scripts.Add(new Spike());

            fireObject.inizilize();

            PhysicsEngine.AddCollider(collider, true);
        }
        public static void LoadWaterAt(int x, int y)
        {
            GameObject o = new GameObject();
            o.sprite = SpriteManager.loadSprite($"generic\\water.png");

            o.posistion = new Vector(x * 16, y * 16);

            o.inizilize();  
        }
        public static void LoadWalkerAt(int x, int y)
        {
            GameObject gobject = new GameObject();
            gobject.name = "walker";
            gobject.layer = 0;

            Pupit pupit = new Pupit();
            pupit.ContentRoot = "groenebalman";
            gobject.scripts.Add(pupit);

            gobject.renderingOffset = new Vector(0, 5);
            gobject.size = new Vector(16f, 16f);
            gobject.posistion = new Vector(x * 16, y * 16);

            RigidBody body = new RigidBody();
            gobject.scripts.Add(body);

            gobject.scripts.Add(new Walker());

            //


            Collider collider = new Collider(new Vector(14, 7));
            collider.offset = new Vector(1, 8);
            collider.LiveUpdate = true;
            collider.isTrigger = true;
            gobject.scripts.Add(collider);

            body.collider = collider;

            gobject.scripts.Add(new Waterable());

            gobject.scripts.Add(new Spike());

            gobject.inizilize();

            PhysicsEngine.AddCollider(collider, true);
            pupit.SetAnimationState("walk_left");

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
