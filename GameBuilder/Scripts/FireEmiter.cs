using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBuilder.Levels;
using GameBuilder.Particle;
namespace GameBuilder.Scripts
{
    internal class FireEmiter : Script
    {
        public override void LateUpdate()
        {
            
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            GameObject gObject = new GameObject();
            gObject.particleData = new ParticleData(System.Drawing.Color.Firebrick);
            gObject.size = new _Math.Vector(1, 1);

            gObject.posistion = gameObject.posistion.copy();
            gObject.scripts.Add(new FireParticle());

            gObject.inizilizeAsParticle();

        }
    }
}
