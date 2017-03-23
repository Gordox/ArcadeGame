using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FTexture2D.FAnimation
{
    public class FrameAnimation : Animation
    {
        private Texture2D texture;
        private Point origin;
        private Point walker;
        private Point walkerAt;
        private float stateTime;
        private int width;
        private int height;
        private int frameCounter;
        private int frames;
        private float frameDuration;
        private bool reversed;
        private bool loop;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tex">Spritesheet</param>
        /// <param name="x">start pos in x-led in spritesheet</param>
        /// <param name="y">start pos in y-led in spritesheet</param>
        /// <param name="width">The width if the frame</param>
        /// <param name="height">The height if the frame</param>
        /// <param name="frames">How many frames it is</param>
        /// <param name="frameDuration">How long each frame is visible</param>
        /// <param name="walker">How the spritesheet is designed EX: (2x4 or 4x2)</param>
        /// <param name="loop">Chall the animation loop</param>
        /// <param name="reversed">reversed order of the animation</param>
        public FrameAnimation(Texture2D tex, int x, int y, int width, int height, int frames, float frameDuration, Point walker, bool loop = true, bool reversed = false)
        {
            this.origin = new Point(x, y);
            this.walker = walker;
            this.walkerAt = new Point(0, 0);
            this.reversed = reversed;
            this.loop = loop;
            this.width = width;
            this.height = height;
            this.stateTime = 0;
            this.frames = frames;
            this.frameDuration = frameDuration;
            this.texture = tex;
            this.frameCounter = 0;
        }

        public override bool HasNext()
        {
            return lastFrame != currentFrame;
        }

        public override void Update(float delta)
        {
            lastFrame = currentFrame;
            stateTime += delta;

            if (loop)
            {
                if (reversed)
                    currentFrame = (frames - (int)(stateTime / (float)frameDuration)) % frames;
                else
                    currentFrame = (int)(stateTime / (float)frameDuration) % frames;
            }
            else
            {
                if (reversed)
                    currentFrame = Math.Max(frames - (int)(stateTime / (float)frameDuration) - 1, 0);
                else
                    currentFrame = Math.Min((int)(stateTime / (float)frameDuration), frames - 1);
            }

            if (currentFrame > lastFrame)
            {
                ++walkerAt.X;
                if (walkerAt.X >= walker.X)
                {
                    walkerAt.X = 0;
                    ++walkerAt.Y;
                    if (walkerAt.Y >= walker.Y)
                        walkerAt.Y = 0;
                }
                ++frameCounter;
            }
            if(frameCounter >= frames)
            {
                this.walkerAt = new Point(0, 0);
                frameCounter = 0;
            }

            if (currentFrame < lastFrame)
            {
                //Fix so it can reverse
            }
        }

        public override float GetPercent()
        {
            if (loop)
            {
                return 0;
            }
            else
            {
                if (reversed)
                    return 1.0f - ((currentFrame) / (float)(frames - 1));
                else
                    return currentFrame / (float)(frames - 1);
            }
        }

        public override TextureRegion GetRegion()
        {
            return new TextureRegion(
                texture,
                origin.X + walkerAt.X * width,
                origin.Y + walkerAt.Y * height,
                width,
                height
           );
        }


        public override void ResetAnimation()
        {
            lastFrame = -1;
            frameCounter  = currentFrame = 0;
            stateTime = 0;
            this.walkerAt = new Point(0, 0);
        }
    }
}
