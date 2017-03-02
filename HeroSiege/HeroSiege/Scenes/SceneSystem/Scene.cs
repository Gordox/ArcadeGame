using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace HeroSiege.Scenes.SceneSystem
{
    // current state the scene is in
    public enum SceneState
    {
        Active,
        Inactive,
    }

    // current state the scene is in
    public abstract class Scene
    {
        public bool IsActive { get { return !otherSceneHasFocus && State == SceneState.Active; } }
        public bool IsPopup { get; protected set; }
        public SceneState State { get; protected set; }

        // Member variables
        private bool otherSceneHasFocus;

        public GraphicsDeviceArcade Graphics { get; set; }

        public Game1 Game { get; set; }


        public Scene() { State = SceneState.Active; }
        public abstract void Init();
        public virtual void HandleInput() { }
        public virtual void Update(float delta, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            this.otherSceneHasFocus = otherSceneHasFocus;
            State = (coveredByOtherScene) ? SceneState.Inactive : SceneState.Active;
        }

        public abstract void Draw(SpriteBatch SB);

        public virtual void OnExiting() { }

        protected void AddScene(Scene scene, bool isPopup = false)
        {
            if (!isPopup)
                SceneManager.RemoveScene(this);

            SceneManager.AddScene(scene);
        }
    }
        
}
