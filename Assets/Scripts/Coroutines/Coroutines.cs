using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Coroutines
{
    public class Coroutines : MonoBehaviour
    {
        private const string NAME = "COROUTINE_MANAGER";
        private static Coroutines m_istance;

        private static Coroutines instance
        {
            get
            {
                if (m_istance == null)
                {
                    GameObject gameObject = new GameObject(NAME);
                    m_istance = gameObject.AddComponent<Coroutines>();
                    DontDestroyOnLoad(gameObject);
                }

                return m_istance;
            }
        }

        public static Coroutine StartRoutine(IEnumerator routine)
        {
            return instance.StartCoroutine(routine);
        }

        public static void StopRoutine(Coroutine routine)
        {
            if (routine != null)
            {
                instance.StopCoroutine(routine);
            }
        }
    }
}
