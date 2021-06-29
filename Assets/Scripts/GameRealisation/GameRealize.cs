using UnityEngine;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.GameRealisation
{
    public class GameRealize : MonoBehaviour
    {
        private void Start()
        {
            Game.Run();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("hello");
            }
        }
    }
}
