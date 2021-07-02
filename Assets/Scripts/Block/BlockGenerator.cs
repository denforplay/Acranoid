﻿using UnityEngine;
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
        private float verticalDistance = (float)Screen.height / 1920f * 25f;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void ShowBlocks()
        {
            Level.Level level = LevelManager.instance.GetNextLevel();
            float horizontal = Screen.width / 2;
            float vertical = Screen.height / 2;
            for (int i = 1; i <= level.blocksCountInColumn; i++)
            {
                for (int j = 1; j <= level.blocksCountInRow; j++)
                {
                    Block block = Instantiate(blockPrefab, _parent.transform);
                    vertical += j * verticalDistance;
                    block.transform.position = _camera.ScreenToWorldPoint(new Vector3(horizontal, vertical, _camera.nearClipPlane));
                    block.SetData(_blockConfig);
                }
                vertical = Screen.height / 2;
                horizontal += i * horizontalDistance;
            }
        }

        private void OnEnable()
        {
            LevelManager.OnLevelsInitialized += ShowBlocks;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelsInitialized -= ShowBlocks;
        }
    }
}
