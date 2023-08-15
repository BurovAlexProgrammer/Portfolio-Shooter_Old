using _Project.Scripts.Main.Contexts;
using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Service;
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

                if (_targetDistance <= brainOwner.CharacterController.Data.MeleeRange)
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
            if (brainOwner.CharacterController.Attacker.ReadyToAttack == false) return;
            
            brainOwner.CharacterController.Attacker.StartAttack();
            await brainOwner.CharacterController.PlayAttack();
            brainOwner.CharacterController.Attacker.EndAttack();
            await brainOwner.CharacterController.Attacker.RunAttackDelay(brainOwner.CharacterController.CancellationToken);
        }

        private void MoveToTarget(BrainOwner brainOwner)
        {
            brainOwner.NavMeshAgent.SetDestination(brainOwner.TargetHealth.transform.position);
        }

        private void FindTarget(BrainOwner brainOwner)
        {
            var player = Context.GetSceneObject<Player>();
            
            if (player == null)
            {
                Debug.LogWarning($"No target Player for hunter brain.");
                return;
            }
            
            brainOwner.SetTarget(player.gameObject);
        }
    }
}