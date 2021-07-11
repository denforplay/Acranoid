using Newtonsoft.Json;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class JsonParser
    {
        public Level LoadJsonData(TextAsset textAsset)
        {
            return JsonConvert.DeserializeObject<Level>(textAsset.text);
        }

        public void SetJsonData(Level level)
        {
            level.isCompleted = true;
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(AssetDatabase.GetAssetPath(level.textAsset)))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, level);
            }
        }
    }
}
