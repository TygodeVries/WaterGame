using GameBuilder.Levels;

namespace GameBuilder.Scripts
{
    abstract internal class Script
    {
        public GameObject gameObject;

        public abstract void Start();

        /// <summary>
        /// Called before the physics update.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Called after the physics update.
        /// </summary>
        public abstract void LateUpdate();
    }
}
