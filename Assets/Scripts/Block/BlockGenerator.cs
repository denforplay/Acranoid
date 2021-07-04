using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Level;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.Block
{
    public class BlockGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _parent;
        [SerializeField] private Block blockPrefab;
        [SerializeField] private BlockConfig _blockConfig;

        private float horizontalDistance = (float)Screen.width / 1080f * 150f;
        private float verticalDistance = (float)Screen.height / 1920f * 50f;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void ShowBlocks()
        {
            Level.Level level = LevelManager.instance.GetCurrentLevel();
            float horizontal = Screen.width / 2 - level.blocksCountInRow * verticalDistance / 2;
            float vertical = Screen.height / 2 + level.blocksCountInColumn * horizontalDistance / 2;
            for (int i = 1; i <= level.blocksCountInColumn; i++)
            {
                for (int j = 1; j <= level.blocksCountInRow; j++)
                {
                    Block block = BlocksManager.instance.GetBlock();
                    vertical += verticalDistance;
                    block.transform.position = _camera.ScreenToWorldPoint(new Vector3(horizontal, vertical, _camera.nearClipPlane));
                    block.SetData(_blockConfig);
                }
                vertical = Screen.height / 2 + level.blocksCountInColumn * horizontalDistance / 2; ;
                horizontal += horizontalDistance;
            }
        }

        private void OnEnable()
        {
            Game.GetRepository<BlocksRepository>().OnBlocksRepoInitialied += ShowBlocks;
        }

        private void OnDisable()
        {
            Game.GetRepository<BlocksRepository>().OnBlocksRepoInitialied -= ShowBlocks;
        }
    }
}
