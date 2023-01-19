using UnityEngine;

namespace _Project.Scripts.Main.Game.Brain
{
    [CreateAssetMenu(menuName = "Custom/Brain/Hunter")]
    public class HunterBrain : Brain
    {
        private float _targetDistance;
        
        public override void Think(BrainOwner brainOwner)
        {
            if (brainOwner.IsTargetExist)
            {
                _targetDistance =
                    Vector3.Distance(brainOwner._transform.position, brainOwner.TargetHealth._transform.position);

                if (_targetDistance <= brainOwner.Character.Data.MeleeRange)
                {
                    AttackTarget(brainOwner);
                }
                else
                {
                    MoveToTarget(brainOwner);
                }
            }
            else
            {
                FindTarget(brainOwner);
            }
        }

        private void AttackTarget(BrainOwner brainOwner)
        {
            brainOwner.Character.PlayAttack(brainOwner.Target);
        }

        private void MoveToTarget(BrainOwner brainOwner)
        {
            brainOwner.NavMeshAgent.SetDestination(brainOwner.TargetHealth.transform.position);
        }

        private void FindTarget(BrainOwner brainOwner)
        {
            brainOwner.SetTarget(brainOwner.Player.gameObject);
        }
    }
}