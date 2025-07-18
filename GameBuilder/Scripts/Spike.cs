﻿using GameBuilder.Game;
using GameBuilder.Levels;
using GameBuilder.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Scripts
{
    internal class Spike : Script
    {
        public Collider collider;
        bool hasFired = false;
        public override void Start()
        {
            collider = (Collider)gameObject.getScript("Collider");
            collider.Subscribe(OnCollisionEnter);
        }

        public override void Update()
        {
            
        }

        public override void LateUpdate()
        {

        }

        public void OnCollisionEnter(GameObject collider)
        {
            if (collider == Game.Main.playerController.gameObject)
            {
                if (!hasFired)
                {
                    PlayerController p = Main.playerController;
                    
                        p.Kill();
                    
                }

                hasFired = true;
            }
        }
    }
}
