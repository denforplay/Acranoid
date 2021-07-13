using Newtonsoft.Json;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Assets.Scripts.PlayerData;

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
            string jsonLevel = JsonConvert.SerializeObject(level);

            PlayerDataManager.GetInstance.SetLevelDataForKey(LevelManager.GetInstance.CurrentLevel.textAsset.name, jsonLevel);
        }
    }
}
