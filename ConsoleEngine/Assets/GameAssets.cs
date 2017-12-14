using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ConsoleEngine.Components;
using ConsoleEngine.Scenes;

namespace ConsoleEngine.Assets
{
    public static class AssetManager //Класс ассетов
    {
        public static bool loaded = false; //Загружены ли?

        private const char prefix = '/'; //Префикс для разделения спрайтов в txt-файле

        public static Dictionary<string, List<string>> assets; //Загруженные ассеты

        public static void LoadAssetsByPath(string path) //Метод загрузки ассетов
        {
            loaded = false;
            assets = new Dictionary<string, List<string>>();
            if (File.Exists(path + "sprites.txt")) //Если файлик со спрайтами имеется, то ищем в нём символы
            {
                StreamReader reader = new StreamReader(path + "sprites.txt", Encoding.Unicode);
                string spriteName = "";
                int maxLineLength = 0;
                List<string> textSprite = new List<string>();
                while (!reader.EndOfStream) //Сканирует файл и загружает в мапу, разделяя
                {
                    string line = reader.ReadLine(); //Прочитанная строка с файла
                    if (line.Length > 0 && line[0] == prefix) //Если длина прочитанной строки больше нуля и в начале стоит префикс
                    {
                        if (textSprite.Count == 0) //Если же текущий текстовый спрайт пуст
                        {
                            spriteName = line.Remove(0, 1); //Берём имя для него не учитывая префикс
                            line = reader.ReadLine(); //Читаем следующую, что бы не добавлять имя к спрайту
                            textSprite.Add(line);
                            maxLineLength = line.Length;
                        }
                        else //Если же в спрайт уже что-то добавлено, не впервой
                        {
                            //И ставим, что размер найден
                            for (int i = 0; i < textSprite.Count; i++) //Для каждой строки в спрайте равняем её под длину макс. длинной строки
                            {
                                if (textSprite[i].Length < maxLineLength)
                                {
                                    string empty = new string(ConsoleGraphics.GraphicsManager.trnsprChar, maxLineLength - textSprite[i].Length);
                                    textSprite[i] = textSprite[i] + empty;
                                }
                            }
                            assets.Add(spriteName, textSprite); //Добавляем к мапе спрайт с ключём как его имя
                            textSprite = new List<string>(); //Очищаем спрайт
                            spriteName = line.Remove(0, 1); //Берём имя для него не учитывая префикс
                            line = reader.ReadLine(); //Читаем следующую, что бы не добавлять имя к спрайту
                            textSprite.Add(line);
                            maxLineLength = line.Length;
                        }
                    }
                    else //Если же длина строки меньше или равна нулю, или же у нас первый элемент не префикс
                    {
                        if (line.Length > maxLineLength) //Если размер строки меньше макс. размера
                        {
                            maxLineLength = line.Length;
                        }
                        textSprite.Add(line);
                    }
                }
                assets.Add(spriteName, textSprite);
                reader.DiscardBufferedData();

            }
            else //Иначе создаём его пустым
            {
                Directory.CreateDirectory(path);
                using (StreamWriter sw = new StreamWriter(File.Open(path + "sprites.txt", FileMode.Create), Encoding.Unicode))
                { //Создаём файлик со спрайтами
                }
            }
            loaded = true;
        }
        public static List<string> GetAsset(string name) //Метод получения спрайта из мапы с загуженными спрайтами
        {
            if (name != null) //Имя не пусто
            {
                List<string> shape;
                assets.TryGetValue(name, out shape); //Пробуемс получить ассет
                return shape; //И возвращаем
            }
            return null;
        }
        public static void SavePrefab(GameObject obj, string path) //Метод сохранения игрового объекта (ПЕРЕДЕЛАТЬ)
        {
            using (Stream stream = File.Create(path))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, obj);
            }
        }
        public static GameObject LoadPrefab(string path) //Это тоже переделать
        {
            GameObject loadedObj;
            using (Stream stream = File.OpenRead(path))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                loadedObj = (GameObject)binaryFormatter.Deserialize(stream);
            }
            return loadedObj;
        }

    }
}
