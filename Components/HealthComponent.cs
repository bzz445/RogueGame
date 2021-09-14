using GoRogue.GameFramework;
using GoRogue.GameFramework.Components;
using RogueGame.Entities;
using RogueGame.Logging;

namespace RogueGame.Components
{
    public class HealthComponent : IGameObjectComponent, IHealthComponent
    {
        private float _health;
        
        public HealthComponent(float maxHealth)
            : this(maxHealth, maxHealth) { }

        public HealthComponent(float maxHealth, float health)
        {
            MaxHealth = maxHealth;
            Health = health;
        }

        /// <summary>
        /// e = previous health
        /// </summary>
        public event System.EventHandler<float> HealthChanged;
        
        public IGameObject Parent { get; set; }

        public float Health
        {
            get { return _health; }
            private set
            {
                if (value == _health)
                {
                    return;
                }

                var prevHealth = _health;
                _health = value;
                HealthChanged?.Invoke(this, prevHealth);
            }
        }
        public float MaxHealth { get; }

        public bool Dead => Health < 0.001;
        
        public void ApplyDamage(float damage, ILogManager logManager)
        {
            Health = System.Math.Max(0, Health - damage);
            if (Dead && Parent is McEntity mcParent)
            {
                logManager.EventLog($"{mcParent.ColoredName} was slain.");
                mcParent.Remove();
            }
        }

        public void ApplyHealing(float healing)
        {
            Health = System.Math.Min(MaxHealth, Health + healing);
        }
    }
}