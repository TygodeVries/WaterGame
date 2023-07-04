using GameBuilder.Levels;

namespace GameBuilder.Scripts
{
    abstract internal class Script
    {
        public GameObject gameObject;

        public virtual void Start()
        {

        }

        /// <summary>
        /// Called before the physics update.
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Called after the physics update.
        /// </summary>
        public virtual void LateUpdate()
        {
            
        }
    }
}
