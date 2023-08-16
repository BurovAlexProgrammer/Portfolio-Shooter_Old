using UnityEngine;

namespace Main.Game.Brain
{
    public abstract class Brain : ScriptableObject
    {
        public virtual void Initialize(BrainOwner brainOwner) {}
        public abstract void Think(BrainOwner brainOwner);
    }
}