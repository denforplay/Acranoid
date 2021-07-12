﻿using UnityEngine;

namespace Assets.Scripts.Abstracts.Singeton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T GetInstance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new GameObject().AddComponent<T>();
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }

        protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this as T)
            {
                Destroy(gameObject);
            }
                    
            DontDestroyOnLoad(gameObject);
        }
    }
}
