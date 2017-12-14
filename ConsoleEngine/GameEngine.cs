#define USE_EDITOR

using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using ConsoleEngine.Assets;
using ConsoleEngine.Components;
using ConsoleEngine.ConsoleGraphics;
using ConsoleEngine.Scenes;
using ConsoleEngine.Input;
using ConsoleEngine.Editor;


namespace ConsoleEngine.Core
{
    public static class GameEngine
    {

        #region EventManagement
        public static Action OnGameStart = delegate { };
        #endregion
        #region Core
        public const string GAME_SPRITES_PATH = "Resources\\Sprites\\";
        public const string GAME_SCENES_PATH = "Resources\\Scenes\\";
        public enum GameState { editorPause, active, stop }
        public static GameState gameState
        {
            get { return _gameState; }
            set
            {
                if (_gameState == GameState.editorPause && value == GameState.active)
                {
                    SceneManager.currentScene.CallOnCreate();
                }
                _gameState = value;
            }
        }
        private static GameState _gameState = GameState.active;

        public static bool withEditor;
        public static void Init(short width, short height, bool withEditor) //Метод инициализации
        {
            AssetManager.LoadAssetsByPath(GetDirectoryPath() + GAME_SPRITES_PATH); //Загружаются ассеты
            InputManager.Init(); //Запускается управление
            GraphicsManager.Init(width, height); //Запускается графика
            if (File.Exists(GAME_SCENES_PATH + "defalutScene.scn"))
            {
                SceneManager.LoadScene(GetDirectoryPath() + GAME_SCENES_PATH + "defalutScene.scn");
            }
            else
            {
                SceneManager.CreateNewScene(); //Создаётся пустая сцена
                SceneManager.SaveScene(GetDirectoryPath() + GAME_SCENES_PATH + "defalutScene.scn");
            }

            GameEngine.withEditor = withEditor;
            OnGameStart();
            if (!withEditor)
            {
                Application.Run();
            }
            else
            {
                GameEditor.Init();
            }
        }
        public static string GetDirectoryPath()
        {
            return Directory.GetCurrentDirectory() + "\\";
        }
        #endregion
    }
}
