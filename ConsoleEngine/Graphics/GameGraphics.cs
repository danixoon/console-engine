using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Diagnostics;
using Microsoft.Win32.SafeHandles;
using ConsoleEngine.Scenes;
using ConsoleEngine.Components;
using ConsoleEngine.Core;
using ConsoleEngine.Input;

namespace ConsoleEngine.ConsoleGraphics
{
    /// <summary>
    /// Класс, ответственный за графику в консоли
    /// </summary>
    public class GraphicsManager
    {
        //Низкоуровневые методы взаимодействия с консольным окном
        #region LowLevel Bla-bla 
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags); //Низкоуровневый метод для удаления у 
                                                                            //Консольного окна ресайзинга, фуллскрина и тд
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
               string fileName,
               [MarshalAs(UnmanagedType.U4)] uint fileAccess,
               [MarshalAs(UnmanagedType.U4)] uint fileShare,
               IntPtr securityAttributes,
               [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
               [MarshalAs(UnmanagedType.U4)] int flags,
               IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool WriteConsoleOutput( //Низкоуровневый метод рисования символов в консоли
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord //Структура координат для рендера
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct CharUnion //Структура с символом - либо в юникоде, либо в ANCII
        {
            [FieldOffset(0)] public char UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct CharInfo //Стркутра с информацией о символе и его аттрибуте (цвете)
        {
            [FieldOffset(0)] public CharUnion Char;
            [FieldOffset(2)] public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect //Прямоугольник, показывающий начальные и конечные координаты рендерного окна
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }
        #endregion 

        public static Thread renderThread; //Поток с рендером
        public const short backColor = (short)0;
        public const char trnsprChar = '░'; //Символ, считающийся как прозрачный
        public static short width, height; //Ширина и высота окна и буффера
        public static bool focused; //Стоит ли фокус на рендере?
        /// <summary>
        /// Метод инициализации
        /// </summary>
        /// <param name="width"> Ширина окна и буффера </param>
        /// <param name="height"> Высота окна и буффера </param>
        public static void Init(short width, short height) 
        {
            IntPtr handle = GetConsoleWindow(); //Получаем консольное окно
            IntPtr sysMenu = GetSystemMenu(handle, false); //Получаем меню этого окна(настройки)
            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND); //Удаляем кнопку фуллскрина
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND); //Удаляем ресайзинг окна
            }
            GraphicsManager.width = width; //Назначем ширину
            GraphicsManager.height = height; //И высоту
            Console.SetBufferSize(width, height); //Задаём размер буфферного окна соответствующий ширине и высоте
            Console.SetWindowSize(width, height); //Таким же образом задаём размер окна консоли
            Console.CursorVisible = false; //Отключаем курсор в консоли
            Console.Title = "SuperDuperGame"; //Ставим название окна
            renderThread = new Thread(new ThreadStart(Render)); //Создаём экземпляр потока
            renderThread.Start(); //Включаем его
        }
        private static void Render() //Метод рендера
        {
            int fps = 0; //Текущий фпс
            int currentStep = Environment.TickCount + 1000; //Время, через которое фпс обновится
            SafeFileHandle h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero); //Штука нужна
            while (GameEngine.gameState != GameEngine.GameState.stop) //Пока рендер включён
            {
                if (!h.IsInvalid) //Если что-то корректно
                {
                    if (SceneManager.currentScene != null) //Если сцена создана
                    {
                        
                        int size = width * height;
                        CharInfo[] frame = new CharInfo[size]; //Получаем буффер символов размером с консоль
                        for(int i = 0; i < size; i++)
                        {
                            frame[i].Attributes = backColor;
                        }   
                        SmallRect rect = new SmallRect() { Left = 0, Top = 0, Right = width, Bottom = height }; //Рисовать будем во всей консоли
                        List<GameObject> objects = SceneManager.currentScene.gameObjects; //Получаем список объектов со сцены
                        for (int j = 0; j < objects.Count; j++) //Для каждого объекта
                        {
                            for (int i = 0; i < objects[j].components.Count; i++) //Для каждого компонента
                            {
                                if (GameEngine.gameState != GameEngine.GameState.editorPause)
                                {
                                    objects[j].components[i].Update(); //Вызываем в нём Update()
                                }
                                if (objects[j].components[i] is IGraphicsComponent) //Если же этот компонент графический
                                {
                                    IGraphicsComponent c = objects[j].components[i] as IGraphicsComponent; //Получаем его интерфейс
                                    c.Draw(objects[j], ref frame); //И вызываем метод отрисовки, отдавая в качестве аргументов
                                }                           //объект, его вызываший и сам кадр
                            }
                        } //Рисуем это в консоли
                        bool b = WriteConsoleOutput(h, frame, new Coord() { X = rect.Right, Y = rect.Bottom }, new Coord() { X = 0, Y = 0 }, ref rect); 
                        fps++; //Добавляем фпс (кадр жи отрисовался)
                        if (Environment.TickCount > currentStep) //Если секунда с прошлого обновления прошла
                        {
                            currentStep = Environment.TickCount + 1000; //То добавляем ещё секунду к следующему апдейту кадра
                            if (ConsoleEngine.Core.GameEngine.withEditor) //Если у нас включён редактор
                            {
                                ConsoleEngine.Editor.GameEditorForm.gameFps = fps; //То ставим игровой фпс в нём на наш найденный фпс
                            }
                            fps = 0; //И фпс становится 0
                        }
                    }
                    Thread.Sleep(5); //Засыпаем на 5 мс, обеспечивая не более 120 фпс и снимаем нагрузку на процессор
                }
            }
        }
    }
}
