﻿using GameBuilder.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBuilder.Levels;
namespace GameBuilder.Scripts
{
    internal class Spring : Script
    {
        public Collider collider;

        public override void Start()
        {
            collider = (Collider) gameObject.getScript("Collider");
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
            RigidBody body = (RigidBody) collider.getScript("RigidBody");
            body.velocity.y = -3f;
        }
    }
}
