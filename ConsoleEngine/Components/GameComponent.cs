using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleEngine.Core;
using ConsoleEngine.ConsoleGraphics;
using ConsoleEngine.Scenes;
using ConsoleEngine.Assets;
using ConsoleEngine.Input;

namespace ConsoleEngine.Components
{

    public interface IGraphicsComponent //Интерфейс для граф. компонента
    {
        void Draw(GameObject obj, ref GraphicsManager.CharInfo[] frame);
    }
    public abstract class GameComponent //Базовый абстракный класс игрового объекта
    {
        public bool created = false;
        public string name { get { return _name; } }
        private string _name;
        public GameComponent()
        {
            _name = GetType().Name;
        }
        public override string ToString()
        {
            return GetType().Name;
        }
        public GameObject gameObject;
        public virtual void OnDestroy() { }
        public virtual void OnCreate() { }
        public virtual void Update() { }
        public virtual void Start() { }
    }
    public class EditableProperty : Attribute //Аттрибут для доступных редактированию свойств компонентов
    {
        
    }
    public class TextShape : GameComponent, IGraphicsComponent //Компонент для отрисовки текстового спрайта
    {
        private List<string> shape;
        [EditableProperty]
        public string shapeName
        {
            set
            {
                _shapeName = value;
                shape = AssetManager.GetAsset(_shapeName);
            }
            get
            {
                return _shapeName;
            }
        }
        private string _shapeName;
        [EditableProperty] public ConsoleColor color { get; set; } = ConsoleColor.White;

        public void Draw(GameObject obj, ref GraphicsManager.CharInfo[] frame)
        {
            if (shape == null) return;
            for (int i = 0; i < shape.Count; i++) //Для каждой строки
            {
                int x = (int)Math.Floor(obj.position.x), y = (int)Math.Floor(obj.position.y);
                string _shape = shape[i];
                int leftX = 0;
                int rightX = 0;
                if (x < 0)
                {
                    leftX = Math.Abs(x);
                    if (leftX > _shape.Length)
                    {
                        return;
                    }
                }
                else if (x + _shape.Length > GraphicsManager.width)
                {
                    rightX = (x + _shape.Length) - GraphicsManager.width;
                    if(rightX > _shape.Length)
                    {
                        return;
                    }
                }
                //Проверка на выход за верхнюю и нижнюю границы
                if (y + i > 0 && y + i < GraphicsManager.height + 1 && x < GraphicsManager.width && x + shape[i].Length > 0) 
                {
                    int index = GraphicsManager.width * (y + i) - (GraphicsManager.width - x);
                    for (int j = leftX; j < _shape.Length - rightX; j++)
                    {
                        if (_shape[j] != GraphicsManager.trnsprChar)
                        {
                            if (index + j < frame.Length)
                            {
                                frame[index + j].Char.UnicodeChar = _shape[j];
                                frame[index + j].Attributes = (short)color;
                            }
                        }
                    }
                }

            }
        }
    }
    [Serializable]
    public struct Vector2
    {
        public override string ToString()
        {
            return "(" + x + ";" + y + ")";
        }
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x * b, a.y * b);
        }
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }
        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.x / b, a.y / b);
        }
        public static bool operator <(Vector2 a, Vector2 b)
        {
            return (a.x < b.x && a.y < b.y);
        }
        public static bool operator >(Vector2 a, Vector2 b)
        {
            return (a.x > b.x && a.y > b.y);
        }
        public static Vector2 zero = new Vector2(0, 0);
        public float x, y;
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    } //Струкутура с позицией
}
