using Assets.Scripts.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    public class Bomb : BaseBonus
    {
        private int _damage = 1;
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

            col--;
            row--;
            for (int i = row; i < row + 3; i++)
            {
                for (int j = col; j < col + 3; j++)
                {
                    try
                    {
                        BlocksManager.GetInstance.ReturnBlock(allBlocks[i][j]);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        public override void Remove()
        {
        }
    }
}
