using Assets.Scripts.LifeSystem;
using UnityEngine;
using Assets.Scripts.Abstracts.Game;
public class Test : MonoBehaviour
{
    private void Start()
    {
        Game.Run();
    }

    private void Update()
    {
        if (!LifeSystem.IsInitialized)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            LifeSystem.AddLife(this, 1);
            Debug.Log($"Life added (1), {LifeSystem.Lifes}");
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            LifeSystem.SpendLife(this, 1);
            Debug.Log($"Life spend (-1), {LifeSystem.Lifes}");
        }
    }
}
