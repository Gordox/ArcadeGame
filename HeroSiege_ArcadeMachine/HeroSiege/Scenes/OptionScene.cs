using HeroSiege.Scenes.SceneSystem;
using HeroSiege.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace HeroSiege.Scenes
{
    class OptionScene : Scene
    {
        public OptionScene() : base() {  }

        public override void Init(){}

        public override void Update(float delta, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            base.Update(delta, otherSceneHasFocus, coveredByOtherScene);
        }

        public override void Draw(SpriteBatch SB) { }

    }
}
