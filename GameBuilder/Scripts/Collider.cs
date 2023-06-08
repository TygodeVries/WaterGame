using GameBuilder._Math;
using GameBuilder.Levels;
using System;
using System.Collections.Generic;

namespace GameBuilder.Scripts
{
    internal class Collider : Script
    {
        // Additional collider data like:
        // - Deadly
        public string ColliderData = "";

        public Vector bottomLeft = new Vector();
        public Vector topRight = new Vector();

        public bool LiveUpdate = false;

        public bool isTrigger = false;

        public override void Start()
        {

        }

        public override void Update()
        {
            if(LiveUpdate)
            {
                bottomLeft = new Vector(0, 1) + this.gameObject.posistion;
                topRight = new Vector(1, 0) + this.gameObject.posistion;
            }
        }

        public override void LateUpdate()
        {

        }

        public void onCollision(GameObject collider)
        { 
            foreach (Action<GameObject> a in actions)
            {
                a(collider);
            }
        }

        List<Action<GameObject>> actions = new List<Action<GameObject>>();

        public void Subscribe(Action<GameObject> action)
        {
            actions.Add(action);
        }
    }
}
