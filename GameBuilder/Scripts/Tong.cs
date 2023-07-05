using GameBuilder.Game;
using GameBuilder.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Scripts
{
    internal class Tong : Script
    {
        public override void Start()
        {
            Collider collider = (Collider) gameObject.getScript("Collider");

            collider.Subscribe(onHit);
        }

        public void onHit(GameObject collidingObject)
        {
            if(collidingObject == Main.playerController.gameObject)
            {
                float dif = collidingObject.posistion.x - gameObject.posistion.x;
                if (dif < 0) Main.playerController.rigidBody.velocity.x -= 2;
                if (dif > 0) Main.playerController.rigidBody.velocity.x += 2;

            }
        }
    }
}
