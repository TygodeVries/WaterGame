using GameBuilder.Levels;
using GameBuilder.Physics;
using GameBuilder.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Scripts
{
    internal class Character : Script
    {
        public Collider collider;
        public string Text = "Hello World";

        

        public bool ShowDialog;
        public GameObject dialogBox;

        public override void Start()
        {
            collider = (Collider)gameObject.getScript("Collider");
            dialogBox = new GameObject();
            dialogBox.posistion = this.gameObject.posistion + new _Math.Vector(-16 - 8, -16 * 3);
            dialogBox.sprite = SpriteManager.loadSprite("\\generic\\dialog_window.png");

            dialogBox.scripts.Add(new Text());
            dialogBox.inizilize();

            collider.Subscribe(OnCollisionEnter);
        }

        public override void Update()
        {
            ShowDialog = false;
        }
        
        public override void LateUpdate()
        {
            if(ShowDialog)
            {
                dialogBox.IsVisible = true;
            }
            else
            {
                dialogBox.IsVisible = false;
            }
        }

        public void OnCollisionEnter(GameObject collider)
        {
            ShowDialog = true;
        }
    }
}
