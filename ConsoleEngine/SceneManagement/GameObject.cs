using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleEngine.Components;

namespace ConsoleEngine.Scenes
{
    [Serializable]
    public class GameObject
    {
        public string name; //Имя объекта
        public string tag; //Тег объекта
        public Vector2 position; //Положение на сцене
        public List<GameComponent> components; //Список компонентов объекта

        public override string ToString()
        {
            return name;
        }
        public void OnDestroy() //Метод, вызывающийся при удалении объекта со сцены
        {
            foreach (GameComponent comp in components)
            {
                comp.OnDestroy();
            }
        }
        public void OnCreate() //Метод, вызывающийся при добавлении объекта на сцену
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (!components[i].created)
                {
                    components[i].OnCreate();
                    components[i].created = true;
                }
            }
        }
        public GameObject(string name, Vector2 position)
        {
            components = new List<GameComponent>();
            this.name = name;
            this.position = position;
        }
        public GameObject(string name, Vector2 position, List<GameComponent> components) //Конструктор
        {
            this.components = components;
            foreach (GameComponent c in this.components)
            {
                c.gameObject = this;
            }
            this.position = position;
            this.name = name;
        }
        public GameObject(string name, string tag, Vector2 position)
        {
            components = new List<GameComponent>();
            this.name = name;
            this.position = position;
            this.tag = tag;
        }
        public GameObject(string name, string tag, Vector2 position, List<GameComponent> components) //Конструктор
        {
            this.components = components;
            foreach (GameComponent c in this.components)
            {
                c.gameObject = this;
            }
            this.position = position;
            this.name = name;
            this.tag = tag;
        }

        public T AddComponent<T>() where T : GameComponent //Метод, добавляющий компонент к объекту
        {
            components.Add(Activator.CreateInstance<T>());
            GameComponent c = components.Last();
            c.gameObject = this;
            if (Core.GameEngine.gameState == Core.GameEngine.GameState.active)
            {
                c.OnCreate();
                c.created = true;
            }
            return c as T; 
        }
        public T GetComponent<T>() where T : GameComponent
        {
            foreach (GameComponent comp in components)
            {
                if (typeof(T) == comp.GetType())
                {
                    return comp as T;
                }
            }
            return null;
        }
        public void RemoveComponent<T>() where T : GameComponent
        {
            GameComponent comp = GetComponent<T>();
            if (comp != null)
            {
                comp.OnDestroy();
                components.Remove(comp);
            }
        }
    }

}
