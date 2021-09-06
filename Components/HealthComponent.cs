using GoRogue.GameFramework;
using GoRogue.GameFramework.Components;

namespace RogueGame.Components
{
    public class HealthComponent : IGameObjectComponent, IHealthComponent
    {
        private float _health;
        
        public HealthComponent(int maxHealth)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
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
        
        public void ApplyDamage(float damage)
        {
            var prevHealth = Health;
            Health = System.Math.Max(0, Health - damage);
        }

        public void ApplyHealing(float healing)
        {
            Health = System.Math.Min(MaxHealth, Health + healing);
        }
    }
}