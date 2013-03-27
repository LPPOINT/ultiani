using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Ultimate2D;
using UltimateAnimate.AnimationModel;
using UltimateAnimate.Debug;
using UltimateAnimate.DebugHelping;
using UltimateAnimate.EntityModel;
using UltimateAnimate.EntityModel.Animating;

namespace UltimateAnimate
{
    public class State2 : GameState
    {
        private TimeLine time;
        private BaseEntity entity;

        public override void LoadContent()
        {
            time = new TimeLine();
            DebugWindow.CreateDebugWindow();
            DebugHelp.Initialize();
            DebugHelp.SetupDefault(false, false, false, false, false, false);
            DebugEntityHandler.Initialize(DebugHelp.Sample, time);


            var dict = new Dictionary<TimeSpan, EntityAnimationInfo>(); 

            dict.Add(
                new TimeSpan(0, 0, 0, 0),
                new EntityAnimationInfo(new Vector2(0, 0), 0, Vector2.Zero, null));
            dict.Add(
                new TimeSpan(0, 0, 0, 1), 
                new EntityAnimationInfo(new Vector2(100, 100), 0, Vector2.Zero, null));
            dict.Add(
                new TimeSpan(0, 0, 0, 2),
                new EntityAnimationInfo(new Vector2(0, 0), 0, Vector2.Zero, null));
            dict.Add(
                new TimeSpan(0, 0, 0, 3),
                new EntityAnimationInfo(new Vector2(0, -100), 0, Vector2.Zero, null));

            var handler = new AnimationKeys(dict).CreateHandler(time);
            entity = new TexturableEntity(new Vector2(0, 0), Texture2DLoader.GetTexture("Cell.png"));
            entity.Handler = handler;
        }

        public override void Update(TimeSpan delta)
        {
            time.AddTime(delta);
            entity.UpdateTick();
            //DebugEntityHandler.Entity.UpdateTick();
        }

        public override void Render(TimeSpan delta)
        {
            GraphicResourses.Graphics.Clear(Color.CornflowerBlue);
            //DebugEntityHandler.Entity.RenderTick();
            entity.RenderTick();
        }
    }
}
