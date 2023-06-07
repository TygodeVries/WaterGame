using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBuilder.Scripts;
using GameBuilder.Levels;
using GameBuilder._Math;
using System.Drawing;

namespace GameBuilder.Particle
{
    internal class FireParticle : Script
    {
        public override void LateUpdate()
        {
            
        }

        Random rng = new Random();

        public override void Start()
        {
            
        }

        float LifeTime = 0;

        float offsetFromCenter;

        public override void Update()
        {
            LifeTime += Time.DeltaTime;
            float flameHight = 10;
            float absoffsetFromCenter = Math.Abs(offsetFromCenter);

            if (absoffsetFromCenter < 1) gameObject.particleData.color = Color.White;
            else if (absoffsetFromCenter < 2) gameObject.particleData.color = Color.Red;
            else (absoffsetFromCenter < 2) gameObject.particleData.color = Color.DarkRed;

            if (LifeTime > flameHight / absoffsetFromCenter) GameObject.Destroy(gameObject);

            gameObject.posistion.y -= Time.DeltaTime * 4;


            float offsetaddition = (float)rng.NextDouble() - 0.5f;
            gameObject.posistion.x += offsetaddition;
            offsetFromCenter += offsetaddition;
        }
    }
}
