using Assets.Scripts.Abstracts.Repository;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.PopUps
{
    public class PopupRepository : Repository
    {
        public Stack<GameObject> popupStack = new Stack<GameObject>();
        public override void Initialize()
        {
            popupStack = new Stack<GameObject>();
        }
    }
}
