using UnityEngine;

namespace _Project.Scripts.Main.Game.Brain
{
    [CreateAssetMenu(menuName = "Custom/Brain/Hunter")]
    public class HunterBrain : Brain
    {
        public override void Think(BrainOwner brainOwner)
        {
            if (brainOwner.IsTargetExist)
            {
                brainOwner.NavMeshAgent.SetDestination(brainOwner.Target.transform.position);
            }
            else
            {
                FindTarget(brainOwner);
            }
        }
        
        private void FindTarget(BrainOwner brainOwner)
        {
            brainOwner.SetTargetHealth(brainOwner.Player.Health);
        }
    }
}