using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBuilder.Levels;
using GameBuilder.Rendering;

namespace GameBuilder.Scripts
{
    internal class Tree : Script
    {
        public override void LateUpdate()
        {
            
        }

        Waterable waterable = null;

        public override void Start()
        {
            waterable = (Waterable)gameObject.getScript("Waterable");
            waterable.Subscribe(OnWatering);
        }

        int wateringStage = 0;

        public void OnWatering()
        {
            wateringStage += 1;

            if(wateringStage > 100)
            {
                gameObject.sprite = SpriteManager.loadSprite("generic\\tree.png");
            }    
        }

        public override void Update()
        {
            
        }
    }
}
