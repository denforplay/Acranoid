using Assets.Scripts.Block;
using System;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    public class HorizontalBombBonus : BaseBonus
    {
        private void Awake()
        {
            isInstantlyActivated = true;
        }
        public override void Apply()
        {
            var allBlocks = BlocksManager.GetInstance.allBlocks;

            int row, col = 0;
            bool isFinded = false;
            for (row = 0; row < allBlocks.Count; row++)
            {
                for (col = 0; col < allBlocks[row].Count; col++)
                {
                    if (allBlocks[row][col] == BonusManager.GetInstance.CurrentDestroyedBlock)
                    {
                        isFinded = true;
                        break;
                    }
                }
                if (isFinded) break;
            }

            int i = 0;

            for (; BlocksManager.GetInstance.allBlocks.Count != 0 && i < BlocksManager.GetInstance.allBlocks[row].Count; i++)
            {
                if(BlocksManager.GetInstance.allBlocks[row][i] is ColorBlock && BlocksManager.GetInstance.allBlocks[row][i].gameObject.activeInHierarchy)
                BlocksManager.GetInstance.ReturnBlock(BlocksManager.GetInstance.allBlocks[row][i]);
            }
        }

        public override void Remove()
        {
        }
    }
}
