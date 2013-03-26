using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Ultimate2D;
using UltimateAnimate.Debug;
using UltimateAnimate.DebugHelping;
using UltimateAnimate.EntityModel;
using UltimateAnimate.VectorModel;

namespace UltimateAnimate
{
    public class State2 : GameState
    {

        private EntityBase entity;

        public override void LoadContent()
        {
            entity = new VectorSpriteEntity(new LineList(new Vector2( 100,  100), new Vector2( 100, 200),  new Vector2(200,  200), new Vector2(200,  100)),
                Texture2DLoader.GetTexture("Cell.png"));

            DebugWindow.CreateDebugWindow();

            DebugEntityState.Initialize(entity,
                new DebugCommandInput("change wireframe entity state"),
                new DebugCommandInput("change visible state"), 
                new DebugCommandInput("change logic state"));

            DebugEntityCache.Initialize(new DebugCommandInput("save entity cache"),
                new DebugCommandInput("load entity cache"), 
                entity);

            var ll = new LineList(new Vector2(100, 100), new Vector2(100, 200), new Vector2(200, 200),
                                  new Vector2(200, 100));

            var e1 = new VectorSpriteEntity(ll, null);
            var e2 = new VectorSpriteEntity(ll, null);
            var e3 = new VectorSpriteEntity(ll, null);
            var e4 = new VectorSpriteEntity(ll, null);
            var e5 = new VectorSpriteEntity(ll, null);
            var el = new List<EntityBase> {e2, e3, e1, e5, e4};

            DebugPositionBatch.Initialize(new DebugBatching(), new DebugCommandInput("batch testing entites (sort by index)"), el.ToArray());

        }

        public override void Update(TimeSpan delta)
        {
           entity.UpdateTick();
        }

        public override void Render(TimeSpan delta)
        {
            GraphicResourses.Graphics.Clear(Color.CornflowerBlue);
            entity.RenderTick();
        }
    }
}
