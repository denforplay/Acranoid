
using UnityEngine;

namespace Assets.Scripts.Block
{
    public class BlocksManager : MonoBehaviour
    {
        public static BlocksManager instance;
        private BlocksController _blocksController;
        private bool _isInitialized;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        public void Initialize(BlocksController blocksController)
        {
            _blocksController = blocksController;
            _isInitialized = true;
        }


    }
}
