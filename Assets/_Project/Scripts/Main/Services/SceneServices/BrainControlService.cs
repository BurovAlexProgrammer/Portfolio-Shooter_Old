using System.Collections.Generic;
using Main.Game.Brain;
using UnityEngine;

namespace Main.Services
{
    public class BrainControlService : MonoBehaviour
    {
        private readonly LinkedList<BrainOwner> _brains = new ();
        private LinkedListNode<BrainOwner> _brainNode;

        
        private void Update()
        {
            if (_brains.Count == 0) return;

            _brainNode = _brainNode?.Next ?? _brains.First;
            _brainNode.Value.Think();
        }

        public void AddBrain(BrainOwner brainOwner)
        {
            _brains.AddLast(brainOwner);
        }

        public void RemoveBrain(BrainOwner brainOwner)
        {
            _brains.Remove(brainOwner);
        }

    }
}