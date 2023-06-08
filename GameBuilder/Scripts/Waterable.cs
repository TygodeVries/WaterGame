using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBuilder.Levels;
namespace GameBuilder.Scripts
{
    internal class Waterable : Script
    {
        public override void LateUpdate()
        {
            
        }

        Collider myCollider = null;

        public override void Start()
        {
            myCollider = (Collider) gameObject.getScript("Collider");
            myCollider.Subscribe(OnCollision);
        }

        void OnCollision(GameObject collider)
        {
            if(collider.name != "water")
            {
                return;
            }

            GameObject.Destroy(collider);

            foreach (Action a in actions)
            {
                a();
            }
        }

        List<Action> actions = new List<Action>();

        // On water update
        public void Subscribe(Action action)
        {
            actions.Add(action);
        }

        public override void Update()
        {
            
        }
    }
}
