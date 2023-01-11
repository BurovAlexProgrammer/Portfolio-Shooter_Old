using System.Runtime.CompilerServices;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Brain
{
    [CreateAssetMenu(menuName = "Custom/Brain/Hunter")]
    public class HunterBrain : Brain
    {
        public override void Think(BrainOwner brainOwner)
        {
            Debug.Log("think");
        }
    }
}