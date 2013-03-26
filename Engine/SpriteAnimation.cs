using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ultimate2D
{
    public class SpriteAnimation
    {

       
        public enum PlayState
        {
            Playing,
            Paused,
            Stoped
        }

        private List<SpriteAnimationFrame> frames;
        public ReadOnlyCollection<SpriteAnimationFrame> Frames { get { return frames.AsReadOnly(); } }

        private PlayState currentPlayState;
        public PlayState CurrentPlayState
        {
            get { return currentPlayState; }
            set
            {
                currentPlayState = value;

                switch (value)
                {
                   case PlayState.Paused:
                        break;
                   case PlayState.Playing:
                        break;
                   case PlayState.Stoped:
                        CurrentFrame = Frames[0];
                        TotalPlayTime = new TimeSpan();
                        CurrentFrameTime = new TimeSpan();
                        currentPlayState = PlayState.Paused;
                        break;
                }

                OnPlayStateChanged();

            }
        }

        public SpriteAnimationFrame CurrentFrame { get; private set; }
        public TimeSpan TimePerFrame { get; private set; }
        public TimeSpan TotalPlayTime { get; private set; }
        public TimeSpan CurrentFrameTime { get; private set; }
        public int CurrentFrameIndex { get; private set; }
        public bool IsLooped { get; set; }
        public bool IsLastFrame { get { return CurrentFrameIndex == Frames.Count - 1; } }
        public bool IsFirstFrame { get { return CurrentFrameIndex == 0; } }

        private readonly bool isFactoryOrigin;
        private readonly ISpriteAnimationFrameFactory factory;
        private readonly int startFrameIndex;

        private SpriteInfo sprite;
        public SpriteInfo Sprite { 
            get
            {
                return sprite;
            }
            internal
            set
            {
                sprite = value;

                if (isFactoryOrigin)
                {
                    frames = new List<SpriteAnimationFrame>(factory.GetFrames(sprite));
                    CurrentFrame = Frames[startFrameIndex];
                }

                sprite.Source = CurrentFrame.FrameBounds;
            } 
        }

        public event EventHandler<EventArgs> AnimationDone;
        protected virtual void OnAnimationDone()
        {
            var handler = AnimationDone;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> FrameChanged;
        protected virtual void OnFrameChanged()
        {
            var handler = FrameChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> PlayStateChanged;
        protected virtual void OnPlayStateChanged()
        {
            var handler = PlayStateChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        internal void UpdateAnimation(TimeSpan delta)
        {
            if(currentPlayState != PlayState.Playing) return;
            CurrentFrameTime += delta;

            if (CurrentFrameTime > TimePerFrame)
            {
                  if(!IsLastFrame 
                      || IsLooped ) NextFrame();
                  else CurrentPlayState = PlayState.Stoped; 
            }

        }
        private void NextFrame()
        {
            CurrentFrameTime = new TimeSpan();

            if (CurrentFrameIndex < Frames.Count-1)
                CurrentFrameIndex++;
            else
            {
                OnAnimationDone();
                CurrentFrameIndex = 0;
            }

            CurrentFrame = Frames[CurrentFrameIndex];
            Sprite.Source = CurrentFrame.FrameBounds;
            OnFrameChanged();
        }

        public SpriteAnimation(IEnumerable<SpriteAnimationFrame> frames, TimeSpan timePerFrame, int startFrameIndex = 0, bool isLooped = true)
        {
            this.frames = new List<SpriteAnimationFrame>(frames);
            this.startFrameIndex = startFrameIndex;
            isFactoryOrigin = false;
            TimePerFrame = timePerFrame;

            CurrentFrame = startFrameIndex < Frames.Count
                ? Frames[startFrameIndex]
                : Frames[0];

            IsLooped = isLooped;
            CurrentFrameTime = new TimeSpan();
            TotalPlayTime = new TimeSpan();
            

        }
        public SpriteAnimation(ISpriteAnimationFrameFactory frameFactory, TimeSpan timePerFrame, int startFrameIndex = 0,
                               bool isLooped = false)
        {
            this.startFrameIndex = startFrameIndex;
            factory = frameFactory;
            isFactoryOrigin = true;
            TimePerFrame = timePerFrame;
            IsLooped = isLooped;
            TotalPlayTime = new TimeSpan();
            CurrentFrameTime = new TimeSpan();

        }

    }
}
