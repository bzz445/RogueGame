using GoRogue;
using RogueGame.Components;
using RogueGame.Entities;
using RogueGame.Logging;
using RogueGame.Maps;

namespace RogueGame.GameSystems.Spells.SpellEffects
{
    public class DamageTargetSpellEffect : ISpellEffect
    {
        private readonly float _damage;

        public DamageTargetSpellEffect(float damage)
        {
            _damage = damage;
        }

        public void Apply(McEntity caster, DungeonMap map, Coord targetCoord, ILogManager logManager)
        {
            var target = map.GetEntity<McEntity>(targetCoord);
            var targetHealth = target?.GetGoRogueComponent<IHealthComponent>();
            if (targetHealth == null)
            {
                return;
            }

            targetHealth.ApplyDamage(_damage, logManager);
        }
    }
}