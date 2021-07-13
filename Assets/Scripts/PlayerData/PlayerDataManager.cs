using Assets.Scripts.Abstracts.Singeton;
using UnityEngine;
using Newtonsoft.Json;

namespace Assets.Scripts.PlayerData
{
    public class PlayerDataManager : Singleton<PlayerDataManager>
    {
        public void SetLevelDataForKey(string key, string data)
        {
            PlayerPrefs.SetString(key, data);
        }

        public Level.Level GetLevelDataForKey(string key)
        {
            return JsonConvert.DeserializeObject<Level.Level>(PlayerPrefs.GetString(key));
        }
    }
}
