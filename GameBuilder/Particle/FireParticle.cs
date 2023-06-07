using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBuilder.Scripts;
using GameBuilder.Levels;
using GameBuilder._Math;

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

        public override void Update()
        {
            LifeTime += Time.DeltaTime;
            if (LifeTime > 3f) GameObject.Destroy(gameObject);

            gameObject.posistion.y -= Time.DeltaTime * 4;

            gameObject.posistion.x += (float) rng.NextDouble() - 0.5f;
        }
    }
}
