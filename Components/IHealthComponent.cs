using GoRogue.GameFramework.Components;
using RogueGame.Logging;

namespace RogueGame.Components
{
    public interface IHealthComponent: IGameObjectComponent
    {
        event System.EventHandler<float> HealthChanged;
        
        float Health { get; }
        float MaxHealth { get; }

        bool Dead { get; }
        
        void ApplyDamage(float damage, ILogManager logManager);
        void ApplyHealing(float healing);
    }
}