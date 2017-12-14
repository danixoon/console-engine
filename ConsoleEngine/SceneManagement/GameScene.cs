using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using ConsoleEngine.Components;


namespace ConsoleEngine.Scenes
{
    /// <summary>
    /// Класс ответственный за управление сценой
    /// </summary>
    public static class SceneManager
    {
        public static Scene currentScene; //Текущая активная сцена

        public static void SaveScene(string path) //Метод сохранение сцены в файл
        {
            SceneData data = new SceneData(currentScene);
            using (Stream stream = File.Create(path))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, data);
            }
        }
        public static void LoadScene(string path) //Метод загрузки сцены из файлы
        {
            if (Core.GameEngine.withEditor) { Core.GameEngine.gameState = Core.GameEngine.GameState.editorPause; }
            SceneData loadedData; //Загруженные данные сцены
            using (Stream stream = File.OpenRead(path)) //Открываем сцену по пути
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                loadedData = (SceneData)binaryFormatter.Deserialize(stream); //Загружаем в переменную
                stream.Dispose(); //Освобождаем поток
            }
            if (Core.GameEngine.gameState != Core.GameEngine.GameState.editorPause)
            {
                if (currentScene != null) //Если сцена не пуста
                {
                    foreach (GameObject obj in currentScene.gameObjects) //Для каждого объекта на сцене
                    {
                        obj.OnDestroy(); //Вызываем метод уничтожения объекта
                    }
                }
                currentScene = loadedData.GetSceneData(); //Загружаем в текущую сцену инфу с загруженных данных сцены
                foreach (GameObject obj in currentScene.gameObjects) //И для каждого объекта уже на новой сцене
                {
                    obj.OnCreate(); //Вызываем OnCreate();
                }
            }
            else
            {
                currentScene = loadedData.GetSceneData();
            }
        }
        public static void CreateNewScene() //Метод создающий пустую сцену
        {
            currentScene = new Scene(new List<GameObject>());
        }
        [Serializable]
        public struct SceneData //Структура, хранящая в себе сцену, объекты и их компоненты
        {
            private List<SceneObjectData> gameObjects;
            public SceneData(Scene scene)
            {
                gameObjects = new List<SceneObjectData>();
                foreach (GameObject obj in scene.gameObjects)
                {
                    gameObjects.Add(new SceneObjectData(obj));
                }
            }
            public Scene GetSceneData()
            {
                List<GameObject> sceneObjects = new List<GameObject>();
                foreach (SceneObjectData data in gameObjects)
                {
                    List<GameComponent> components = new List<GameComponent>();
                    foreach (SceneObjectData.ObjectComponentData comp in data.components)
                    {
                        components.Add(comp.GetComponent());
                    }
                    sceneObjects.Add(new GameObject(data.name,data.tag, data.position, components));
                }
                return new Scene(sceneObjects);
            }
            [Serializable]
            private struct SceneObjectData
            {
                public string name;
                public string tag;
                public Vector2 position;
                public List<ObjectComponentData> components;
                public SceneObjectData(GameObject gameObject)
                {
                    name = gameObject.name;
                    position = gameObject.position;
                    tag = gameObject.tag;
                    components = new List<ObjectComponentData>();
                    foreach (GameComponent c in gameObject.components)
                    {
                        components.Add(new ObjectComponentData(c));
                    }
                }
                [Serializable]
                public struct ObjectComponentData
                {
                    private Type type;
                    private List<PropertyInfo> info;
                    private List<object> values;
                    public ObjectComponentData(GameComponent component)
                    {
                        type = component.GetType();
                        values = new List<object>();
                        info = type.GetProperties().Where(p => p.IsDefined(typeof(EditableProperty), false)).ToList();
                        foreach (PropertyInfo inf in info)
                        {
                            values.Add(inf.GetValue(component));
                        }
                    }
                    public GameComponent GetComponent()
                    {
                        GameComponent comp = Activator.CreateInstance(type) as GameComponent;
                        try
                        {
                            for (int i = 0; i < info.Count; i++)
                            {
                                info[i].SetValue(comp, values[i]);
                            }
                        }
                        catch (Exception) { }
                        return comp;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Класс сцены, содержащий в себе объекты и методы для манипулирования ею
    /// </summary>
    public class Scene
    {
        public List<GameObject> gameObjects; //Объекты на сцене
        public Scene(List<GameObject> gameObjects)
        {
            this.gameObjects = gameObjects;
        }
        public void CallOnCreate()
        {
            foreach (GameObject obj in gameObjects)
            {
                obj.OnCreate();
            }
        } //Вызывается при снятии игры с паузы для вызова OnCreate() для всех объектов, которые были добавлены на протяжении паузы
        public GameObject AddGameObject(GameObject obj) //Метод добавление объекта в сцену
        {
            gameObjects.Add(obj);
            GameObject addedObj = gameObjects.Last();
            if (Core.GameEngine.gameState != Core.GameEngine.GameState.editorPause)
            {
                addedObj.OnCreate();
            }
            return addedObj;
        }
        public void RemoveGameObject(string name) //Метод удаления объекта из сцены
        {
            GameObject deletedObject = FindGameObject(name);
            if (deletedObject != null)
            {
                gameObjects.Remove(deletedObject);
                if (Core.GameEngine.gameState != Core.GameEngine.GameState.editorPause)
                {
                    deletedObject.OnDestroy();
                }
            }
        }
        public GameObject FindGameObject(string name) //Метод поиска объекта на сцене
        {
            foreach (GameObject obj in gameObjects)
            {
                if (obj.name == name)
                {
                    return obj;
                }
            }
            return null;
        }
        public GameObject[] FindGameObjectsWithTag(string tag)
        {
            List<GameObject> objects = new List<GameObject>();
            for(int i = 0; i < gameObjects.Count; i++)
            {
                if(gameObjects[i].tag == tag)
                {
                    objects.Add(gameObjects[i]);
                }
            }
            return objects.ToArray();
        } //Ищет объекты на сцене с заданным тегом и возвращает массив из них
    }
}
