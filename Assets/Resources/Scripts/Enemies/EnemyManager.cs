
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Singleton
    public static EnemyManager Instance;

    [Header("Containers")]
    [SerializeField] Transform _poolContainer;
    [SerializeField] Transform _enemyContainer;
    [SerializeField] Transform _spawnerContainer;
    Lean.Pool.LeanGameObjectPool[] _pools;


    private void Awake()
    {
        Instance = GetComponent<EnemyManager>();
        StartPools();
        SpawnRandom();
    }


    //============================================ Spawn & Despawn Enemies
    /// <summary>
    /// archer, bomber, multiArrow
    /// </summary>
    public void SpawnEnemy(int id, Transform target)
    {
        var _enemy = _pools[id].Spawn(target.position, Quaternion.identity, _enemyContainer);
    }

    public void DespawnEnemy(GameObject enemy)
    {
        for (int i = 0; i < _pools.Length; i++)
        {
            if (enemy == _pools[i].Prefab)
                _pools[i].Despawn(enemy);
        }
    }


    //============================================ Start Pools
    void StartPools()
    {
        _pools = new Lean.Pool.LeanGameObjectPool[_poolContainer.childCount];
        for (int i = 0; i < _poolContainer.childCount; i++)
            _pools[i] = _poolContainer.GetChild(i).GetComponent<Lean.Pool.LeanGameObjectPool>();
    }

    void SpawnRandom()
    {
        foreach (Transform t in _spawnerContainer)
            SpawnEnemy(Random.Range(0, 3), t);
    }
}
