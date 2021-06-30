using UnityEngine;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.GameRealisation
{
    public class GameRealize : MonoBehaviour
    {


        private void Awake()
        {
            Game.Run();
        }
    }
}
