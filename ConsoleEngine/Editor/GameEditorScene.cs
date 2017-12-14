using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using ConsoleEngine.Scenes;
using ConsoleEngine.Components;
using ConsoleEngine.ConsoleGraphics;
using ConsoleEngine.Core;

namespace ConsoleEngine.Editor
{
    public static class GameEditorScene
    {
        public static bool enable = true;
        private static bool disposing = false;
        public static float sceneScale = 2;

        private static Vector2 objectSize = new Vector2(5, 5);
        public static Vector2 cameraPos = Vector2.zero;
        public static List<GameObject> sceneObjects;
        
        private static Control screen;
        private static Thread renderThread;
        public static void Init(Control screen)
        {
            GameEditorScene.screen = screen;
            UpdateScene();
            graphics = screen.CreateGraphics();
            renderThread = new Thread(new ThreadStart(Render));
            renderThread.Start();
        }
        public static void Stop()
        {
            renderThread.Abort();
        }

        public static void OnResizeEnd()
        {
            disposing = true;
        }

        public static void MouseWheel(int delta)
        {
            if (sceneScale > 1)
            {
                sceneScale += Math.Sign(delta);
            }
            else if(Math.Sign(delta) == 1)
            {
                sceneScale += 1;
            }
        } //Поворот колёсика мыши
        public static void MouseUp(MouseEventArgs e) //Отпуск мыши
        {
            if (choosedIndex != -1 && e.Button == MouseButtons.Left)
            {
                choosedIndex = -1;
            }
        } 
        public static void MouseDown(MouseEventArgs e)
        {
            lastMousePos = new Vector2(e.X, e.Y);
        }
        public static void MouseMove(MouseEventArgs e)
        {
            mousePos = new Vector2(e.X, e.Y);
            if (e.Button == MouseButtons.Middle)
            {
                cameraPos += mousePos - lastMousePos;
                lastMousePos = mousePos;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (choosedIndex != -1)
                {
                    sceneObjects[choosedIndex].position = FromSceneToGame(mousePos + choosedObjectMouseOffset);
                }
            }

        }
        public static Vector2 mousePos;
        public static Vector2 lastMousePos; 
        public static bool mouseDown = false;

        public static Vector2 FromSceneToGame(Vector2 pos)
        {
            return pos / sceneScale - cameraPos / sceneScale;
        }
        public static Vector2 FromGameToScene(Vector2 pos)
        {
            return pos * sceneScale + cameraPos;
        }
        public static void UpdateScene() //Копирует объекты со сцены 
        {
            sceneObjects = SceneManager.currentScene.gameObjects;
        }

        private static Bitmap bitmap;
        private static Graphics frame;
        private static Graphics graphics;

        public static int choosedIndex = -1;
        private static Vector2 choosedObjectMouseOffset;
        private static void Render() //Рендер сцены в редакторе
        {
            bitmap = new Bitmap(screen.Size.Width, screen.Size.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            frame = Graphics.FromImage(bitmap);
            graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            int currentTick = 0;
            int fps = 0;
            while (enable)
            {
                if (!disposing)
                {
                    frame.Clear(Color.White);
                    frame.DrawRectangle(Pens.Red, cameraPos.x, cameraPos.y, GraphicsManager.width * sceneScale, GraphicsManager.height * sceneScale);
                    for (int i = 0; i < sceneObjects.Count; i++)
                    {
                        if (mouseDown)
                        {
                            Vector2 objPos = FromGameToScene(sceneObjects[i].position);
                            if (mousePos.x > objPos.x && mousePos.x < objPos.x + objectSize.x * sceneScale && mousePos.y > objPos.y && mousePos.y < objPos.y + objectSize.y * sceneScale)
                            {
                                if (choosedIndex == -1)
                                {
                                    choosedIndex = i;
                                    choosedObjectMouseOffset = objPos - mousePos;
                                }
                            }
                        }
                        float x = sceneObjects[i].position.x;
                        float y = sceneObjects[i].position.y;
                        frame.DrawRectangle(Pens.Red, x * sceneScale + cameraPos.x , y * sceneScale + cameraPos.y , objectSize.x * sceneScale, objectSize.y * sceneScale);
                        frame.DrawString(sceneObjects[i].name, SystemFonts.DefaultFont, Brushes.Red, (x + objectSize.x) * sceneScale + cameraPos.x, (y + objectSize.y) * sceneScale + cameraPos.y);
                    }
                    graphics.DrawImage(bitmap, 0, 0);
                    fps++;
                    if (Environment.TickCount > currentTick)
                    {
                        currentTick = Environment.TickCount + 1000;
                        GameEditorForm.editorFps = fps;
                        fps = 0;
                    }
                    UpdateScene();
                    Thread.Sleep(10);
                }
                else
                {
                    Dispose();
                    graphics = screen.CreateGraphics();
                    bitmap = new Bitmap(screen.Size.Width, screen.Size.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                    frame = Graphics.FromImage(bitmap);
                    disposing = false;
                }
            }
        }
        public static void Dispose() //Метод освобождения ресурсов из памяти
        {
            bitmap.Dispose();
            frame.Dispose();
            graphics.Dispose();
        }
    }
}
