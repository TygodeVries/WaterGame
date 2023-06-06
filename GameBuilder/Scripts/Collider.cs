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

        public void onCollision()
        { 
            foreach (Action a in actions)
            {
                a();
            }
        }

        List<Action> actions = new List<Action>();

        public void Subscribe(Action action)
        {
            actions.Add(action);
        }
    }
}
