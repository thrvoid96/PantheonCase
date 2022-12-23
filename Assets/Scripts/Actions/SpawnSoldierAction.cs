using System.Collections;
using UnityEngine;

namespace Actions
{
    public class SpawnSoldierAction : BaseAction
    {
        public override void StartAction()
        {
            base.StartAction();
        }

        public override bool DoAction()
        {
            base.DoAction();
            var spawnPoint = PlayerController.Instance.selectedInteractable.GetSpawnPointObj();
            Transform interactableTrans;
            var newSoldier = ObjectPool.Instance.SpawnFromPool("Soldier",
                (interactableTrans = PlayerController.Instance.selectedInteractable.transform).position,
                Quaternion.identity, interactableTrans).GetComponent<Interactable>();
            
            newSoldier.TryPathfinding(true,spawnPoint.transform.position);
            return true;
        }

        public override void CancelAction()
        {
            base.CancelAction();
        }
        
    }
}
