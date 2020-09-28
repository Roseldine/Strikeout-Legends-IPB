
using UnityEngine;
using Lean.Pool;

public class ParticleManager : MonoBehaviour
{
    [Header("Pools")]
    [SerializeField] LeanGameObjectPool _poolAttackHitWall;
    [SerializeField] LeanGameObjectPool _poolAttackHitEnemy;

    public static ParticleManager Instance;

    private void Awake()
    {
        Instance = GetComponent<ParticleManager>();
    }


    public void SpawnHitWall(Vector3 pos)
    {
        var ps = _poolAttackHitWall.Spawn(pos, Quaternion.identity).GetComponent<VFXLifetime>();
        ps.Pool = _poolAttackHitWall;
    }

    public void SpawnHitEnemy(Vector3 pos)
    {
        var ps = _poolAttackHitEnemy.Spawn(pos, Quaternion.identity).GetComponent<VFXLifetime>();
        ps.Pool = _poolAttackHitEnemy;
    }
}
