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

        public Pupit pupit;

        public override void Start()
        {
            Waterable waterable = (Waterable) gameObject.getScript("Waterable");

            pupit = (Pupit) gameObject.getScript("Pupit");
            waterable.Subscribe(onExtinguised);
        }


        int hp = 500;

        public void onExtinguised()
        {
            hp -= 1;

            pupit.SetAnimationState(((int) hp / 100 + 1) + "");

            if (hp < 0)
            {
                GameObject.Destroy(this.gameObject);
                AudioPlayer.PlayAudioSource("fire_off.wav");
            }
        }

        public override void Update()
        {
            if (hp < 500)
            {
                hp++;
            }
            gameObject.posistion = target.posistion;
        }
    }
}
