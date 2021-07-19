using Assets.Scripts.Block;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    public class VerticalBombBonus : BaseBonus
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

            for (; BlocksManager.GetInstance.allBlocks.Count != 0 && i < BlocksManager.GetInstance.allBlocks.Count; i++)
            {
                int index = BlocksManager.GetInstance.allBlocks[i].Count - BlocksManager.GetInstance.allBlocks[row].Count + col;
                if (index < 0) continue;
                try
                {
                    if (BlocksManager.GetInstance.allBlocks[i][index] is ColorBlock && BlocksManager.GetInstance.allBlocks[i][index].gameObject.activeInHierarchy)
                    {
                        BlocksManager.GetInstance.ReturnBlock(BlocksManager.GetInstance.allBlocks[i][index]);
                    }
                }
                catch
                {
                }
            }
        }

        public override void Remove()
        {
        }
    }
}
