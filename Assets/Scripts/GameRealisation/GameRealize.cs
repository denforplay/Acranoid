using UnityEngine;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.GameRealisation
{
    public class GameRealize : MonoBehaviour
    {
        private void Awake()
        {
            Screen.SetResolution(1080, 1920, true);
            Game.Run();
        }
    }
}
