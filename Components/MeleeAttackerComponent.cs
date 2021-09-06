using GoRogue.GameFramework;
using GoRogue.GameFramework.Components;

namespace RogueGame.Components
{
    public class MeleeAttackerComponent: IGameObjectComponent, IMeleeAttackerComponent
    {
        private readonly int _damage;
        
        public IGameObject Parent { get; set; }

        public MeleeAttackerComponent(int damage)
        {
            _damage = damage;
        }
        
        public float GetDamage()
        {
            return _damage;
        }
    }
}