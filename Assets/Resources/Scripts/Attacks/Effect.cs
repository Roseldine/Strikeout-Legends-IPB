/*
 Effects: CC, Dot, Buff, Debuff.

 none = nothing
 cc = stun, root, silence, knockBack, knockUp
 dot = bleed, burn, poison
 buff = speedBoost, attackBoost, healthRegen
 debuff = slow, damageTakenIncrease, healingDecreased
 terrainModifier = add walls and obstacles to terrain
 */

using UnityEngine;

public class Effect: MonoBehaviour
{
    public EffectEnum.statusEffect type;

    public int damage;
    public float ammount;
    public float duration;
    public float percentage;
    public GameObject terrainObject;
    public GameObject effectGraphic;

    #region Constructors
    /// <summary>
    /// Simple CC
    /// </summary>
    public Effect(EffectEnum.statusEffect type, float duration, GameObject effectGraphic)
    {
        this.type = type;
        this.duration = duration;
        this.effectGraphic = effectGraphic;
    }

    /// <summary>
    /// Simple DoT
    /// </summary>
    public Effect(EffectEnum.statusEffect type, float duration, int damage, GameObject effectGraphic)
    {
        this.type = type;
        this.duration = duration;
        this.damage = damage;
        this.effectGraphic = effectGraphic;
    }

    /// <summary>
    /// Simple Buff / Debuff
    /// </summary>
    public Effect(EffectEnum.statusEffect type, float duration, float percentage, GameObject effectGraphic)
    {
        this.type = type;
        this.duration = duration;
        this.percentage = percentage;
        this.effectGraphic = effectGraphic;
    }

    /// <summary>
    /// Simple TerrainGen
    /// </summary>
    public Effect(EffectEnum.statusEffect type, float duration, GameObject terrainObject, GameObject effectGraphic)
    {
        this.type = type;
        this.duration = duration;
        this.terrainObject = terrainObject;
        this.effectGraphic = effectGraphic;
    }
    #endregion
}