using _Project.Scripts.Main.Contexts;
using Cysharp.Threading.Tasks;
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
                    Vector3.Distance(brainOwner.transform.position, brainOwner.TargetHealth.transform.position);

                if (_targetDistance <= brainOwner.Character.Data.MeleeRange)
                {
                    AttackTarget(brainOwner).Forget();
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

        private async UniTask AttackTarget(BrainOwner brainOwner)
        {
            if (brainOwner.Character.Attacker.ReadyToAttack == false) return;
            
            brainOwner.Character.Attacker.StartAttack();
            await brainOwner.Character.PlayAttack();
            brainOwner.Character.Attacker.EndAttack();
            await brainOwner.Character.Attacker.RunAttackDelay(brainOwner.Character.CancellationToken);
        }

        private void MoveToTarget(BrainOwner brainOwner)
        {
            brainOwner.NavMeshAgent.SetDestination(brainOwner.TargetHealth.transform.position);
        }

        private void FindTarget(BrainOwner brainOwner)
        {
            var player = GamePlayContext.Player;
            if (player == null)
            {
                Debug.LogWarning($"No target Player for hunter brain.");
                return;
            }
            
            brainOwner.SetTarget(player.gameObject);
        }
    }
}