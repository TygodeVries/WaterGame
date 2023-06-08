using GameBuilder.Levels;
using GameBuilder.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Scripts
{
    internal class Coin : Script
    {
        public Collider collider;

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

        public void OnCollisionEnter(GameObject collision)
        {
            GameObject.Destroy(this.gameObject);
            Console.WriteLine("CoinCollected");
            collider.bottomLeft = new _Math.Vector(-1000, -1000);
            collider.topRight = new _Math.Vector(-1000, -1001);
        }
    }
}
