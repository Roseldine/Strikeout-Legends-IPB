/*
 Effects: CC, Dot, Buff, Debuff.

 none = nothing
 cc = stun, root, silence, knockBack, knockUp
 dot = bleed, burn, poison
 buff = speedBoost, attackBoost, healthRegen, heal
 debuff = slow, damageTakenIncrease, healingDecreased
 terrainModifier = add walls and obstacles to terrain
 */

using UnityEngine;

public interface IEffect
{    
    
}

public interface IEffectCC
{
    void Stunned(float duration, GameObject effectGraphic);
    void Rooted(float duration, GameObject effectGraphic);
    void Silenced(float duration, GameObject effectGraphic);
    void KnockBack(float duration, float percentage, GameObject effectGraphic);
    void KnockUp(float duration, float percentage, GameObject effectGraphic);
}

public interface IEffectDot
{
    void Bleed();
    void Burned();
    void Poisoned();
}

public interface IEffectBuff
{
    void SpeedBoost();
    void AttackBoost();
    void HealthRegen();
    void Heal();
}

public interface IEffectDebuff
{
    void Slow();
    void DamageTakenIncreased();
    void HealingDecreased();
}

public interface IEffectTerrainGen
{
    void GenerateTerrain();
}
