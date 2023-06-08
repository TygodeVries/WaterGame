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
            if (!hasFired)
            {
                PlayerController p = (PlayerController)PhysicsEngine.bodies[0].gameObject.getScript("PlayerController");
                p.Kill();
            }

            hasFired = true;
        }
    }
}
