using GameBuilder._Math;
using GameBuilder.Game;
using GameBuilder.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Scripts
{
    internal class Pupit : Script
    {
        public string ContentRoot = null;
        string AnimationState = "";

        List<Sprite> CurrentFrames = new List<Sprite>();

        int CurrentFrameIndex = 0;

        public override void LateUpdate()
        {
            
        }
        public void SetAnimationState(string newState)
        {
            if (newState == AnimationState) return;

            AnimationState = newState;
            CurrentFrameIndex = 0;
            CurrentFrames = AnimationTypesToSprite[newState];
            if(CurrentFrames == null)
            {
                Debug.SendFatalErrorMessage($"Animation {newState} not found.");
            }
        }

        private Dictionary<string, List<Sprite>> AnimationTypesToSprite = new Dictionary<string, List<Sprite>>();

        public override void Start()
        {
            string AnimationDir = Program.AssetPath + "\\sprite\\\\animation\\" + ContentRoot;
            string[] AnimationPaths = Directory.GetDirectories(AnimationDir);
            int TotalLoaded = 0;

            foreach(string AnimationPath in AnimationPaths)
            {
                string[] FrameFiles = Directory.GetFiles(AnimationPath);
                string CurrentState = AnimationPath.Replace(AnimationDir + "\\", "");
                Debug.SendDebugMessage($"Found {FrameFiles.Length} frames for animation state {CurrentState} on {ContentRoot}");
                TotalLoaded += FrameFiles.Length;

                List<Sprite> s = new List<Sprite>();

                foreach (string frameFile in FrameFiles)
                {
                    s.Add(SpriteManager.loadSpriteRaw(frameFile));
                }

                AnimationTypesToSprite.Add(CurrentState, s);

            }

            Debug.SendDebugMessage($"Loaded {TotalLoaded} frames for animation root {ContentRoot}.");

            SetAnimationState("idle");
        }

        float TimeSinceLastSpriteChance = 0;

        public override void Update()
        {
            // Time things that will make sure we dont update every frame.
            TimeSinceLastSpriteChance += Time.DeltaTime;
            if (TimeSinceLastSpriteChance < 0.4f) return;
            TimeSinceLastSpriteChance = 0;

            // Update sprite.
            gameObject.sprite = CurrentFrames[CurrentFrameIndex];

            // Go to next frame
            CurrentFrameIndex++;

            // Reset the frame count if we hit the last frame
            if (CurrentFrameIndex == CurrentFrames.Count )
                CurrentFrameIndex = 0;
        }
    }
}
