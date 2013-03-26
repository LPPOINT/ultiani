using System;
using Microsoft.Xna.Framework;
using Ultimate2D;
using UltimateAnimate.AnimationModel;
using UltimateAnimate.Common;
using UltimateAnimate.Debug;
using UltimateAnimate.DebugHelping;

namespace UltimateAnimate
{
    public class State2 : GameState
    {
        private TimeLine time;
        private FpsCutManager fpsCut;

        public override void LoadContent()
        {
            time = new TimeLine();
            DebugWindow.CreateDebugWindow();
            DebugHelp.Initialize();
            DebugHelp.SetupDefault(false, false, false, false, false, false);
            DebugEntityHandler.Initialize(DebugHelp.Sample, time);
            fpsCut = new FpsCutManager(31);
            fpsCut.FpsUpdate += (sender, args) => DebugWindow.AddLine("fps: " + args.Fps);
        }

        public override void Update(TimeSpan delta)
        {
            time.AddTime(delta);
            DebugEntityHandler.Entity.UpdateTick();
            fpsCut.AddTime(delta);
        }

        public override void Render(TimeSpan delta)
        {
            GraphicResourses.Graphics.Clear(Color.CornflowerBlue);
            DebugEntityHandler.Entity.RenderTick();
        }
    }
}
