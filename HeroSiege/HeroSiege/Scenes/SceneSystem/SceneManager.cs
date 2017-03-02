using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.Scenes.SceneSystem
{
    public static class SceneManager
    {
        private static List<Scene> scenes;
        private static Stack<Scene> scenesToUpdate;

        public static GraphicsDeviceArcade GraphicsDevice { get; private set; }

        public static bool ShouldExit { get { return scenes.Count == 0; } }
        public static int ScenesCount { get { return scenes.Count; } }

        public static void Initialize(Game1 game)
        {
            GraphicsDevice = game.GraphicsDevice;

            scenes = new List<Scene>();
            scenesToUpdate = new Stack<Scene>();
        }

        public static void Update(float delta)
        {
            scenesToUpdate.Clear();

            foreach (Scene scene in scenes)
                scenesToUpdate.Push(scene);

            bool otherSceneHasFocus = false;
            bool coveredByOtherScene = false;

            // update all scenes in one update call
            while (scenesToUpdate.Count != 0)
            {
                Scene scene = scenesToUpdate.Pop();

                if (scene.State == SceneState.Active)
                {
                    scene.Update(delta, otherSceneHasFocus, coveredByOtherScene);

                    // let first scene handle input
                    if (!otherSceneHasFocus)
                    {
                        scene.HandleInput();
                        otherSceneHasFocus = true;
                    }

                    if (!scene.IsPopup)
                        coveredByOtherScene = true;
                }
            }
        }

        public static void Draw(SpriteBatch SB)
        {
            for (int i = 0; i < scenes.Count; ++i)
            {
                if (scenes[i].State == SceneState.Inactive)
                    continue;

                scenes[i].Draw(SB);
            }
        }

        public static void AddScene(Scene scene)
        {
            scene.Graphics = GraphicsDevice;
            scenes.Add(scene);
        }

        public static void RemoveScene(Scene scene)
        {
            scenes.Remove(scene);
        }

        public static void OnExiting()
        {
            for (int i = 0; i < scenes.Count; i++)
            {
                if (scenes[i] is GameScene)
                    scenes[i].OnExiting();
            }
        }
    }
}
