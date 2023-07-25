using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Utils
{
    public static class TextUtils
    {
        public static Dictionary<int, T> GetModels<T>() where T : BaseModel
        {
            var jsonData = GetTextFromLocalStorage<T>();
            return FillDictionary<T>(jsonData);
        }

        public static string GetTextFromLocalStorage<T>()
        {
            var path = GetConfigPath<T>();
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            var text = File.ReadAllText(path);
            return text;
        }

        public static T FromJson<T>(string value)
        {
            var obj = JsonConvert.DeserializeObject<T>(value);
            return obj;
        }

        public static string GetConfigPath<T>()
        {
            var path = Path.Combine("Assets/Maps", $"{typeof(T).Name}.json");
            return path;
        }

        public static Dictionary<int, TValue> FillDictionary<TValue>(string jsonData) where TValue : BaseModel
        {
            var fromJson = FromJson<List<TValue>>(jsonData);
            var result = new Dictionary<int, TValue>();
            foreach (var unit in fromJson)
            {
                result.Add(unit.Id, unit);
            }

            return result;
        }
    }
}