using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBuilder.Levels;

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
                GameObject.Destroy(gameObject); 
            }    
        }

        public override void Update()
        {
            
        }
    }
}
