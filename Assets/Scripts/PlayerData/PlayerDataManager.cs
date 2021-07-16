using Assets.Scripts.Abstracts.Singeton;
using UnityEngine;
using Newtonsoft.Json;

namespace Assets.Scripts.PlayerData
{
    public class PlayerDataManager : Singleton<PlayerDataManager>
    {
        public void SetStringDataForKey(string key, string data)
        {
            PlayerPrefs.SetString(key, data);
        }

        public void SetIntDataForKey(string key, int data)
        {
            PlayerPrefs.SetInt(key, data);
        }

        public int GetIntDataForKey(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public string GetStringDataForKey(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public Level.Level GetLevelDataForKey(string key)
        {
            return JsonConvert.DeserializeObject<Level.Level>(PlayerPrefs.GetString(key));
        }
    }
}
