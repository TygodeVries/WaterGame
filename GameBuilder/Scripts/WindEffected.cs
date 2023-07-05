using GameBuilder._Math;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Scripts
{
    internal class WindEffected : Script
    {
        float m = 0;

        float startX;

        public override void Start()
        {
            startX = gameObject.posistion.x;
            m = startX - (gameObject.posistion.y / 20);
        }

        public override void Update()
        {
            m += Time.DeltaTime;
            gameObject.posistion.x = startX + (float) Math.Cos(m);
        }
    }
}
