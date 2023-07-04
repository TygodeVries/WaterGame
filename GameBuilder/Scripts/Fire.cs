using GameBuilder.Audio;
using GameBuilder.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Scripts
{
    internal class Fire : Script
    {
        public GameObject target;

        public override void Start()
        {
            Waterable waterable = (Waterable) gameObject.getScript("Waterable");
            waterable.Subscribe(onExtinguised);
        }


        int hp = 1000;

        public void onExtinguised()
        {
            hp -= 1;

            if (hp < 0)
            {
                GameObject.Destroy(this.gameObject);
                AudioPlayer.PlayAudioSource("fire_off.wav");
            }
        }

        public override void Update()
        {
            if (hp < 1000)
            {
                hp++;
            }
            gameObject.posistion = target.posistion;
        }
    }
}
