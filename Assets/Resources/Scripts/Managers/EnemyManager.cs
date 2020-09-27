
using Boo.Lang;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Singleton
    public static EnemyManager Instance;

    [Header("Containers")]
    [SerializeField] Transform _poolContainer;
    [SerializeField] Transform _enemyContainer;
    Lean.Pool.LeanGameObjectPool[] _pools;

    [Header("Spawner Randomizer")]
    [SerializeField] Transform _patternContainer;
    [SerializeField] Transform _randomPattern;
    int _enemiesSpawned, _enemiesDespawned;

    List<Enemy> _listEnemies;

    public int EnemiesSpawned { get { return _enemiesSpawned; } }
    public int EnemiesDespawned { get { return _enemiesDespawned; } }


    private void Awake()
    {
        Instance = GetComponent<EnemyManager>();
        StartPools();
        _listEnemies = new List<Enemy>();
    }


    private void Start()
    {
        GetRandomPattern();
    }



    //============================================ Spawn & Despawn Enemies
    /// <summary>
    /// archer, bomber, multiArrow
    /// </summary>
    public void SpawnEnemy(int id, Transform target)
    {
        var _enemy = _pools[id].Spawn(target.position, Quaternion.identity, _enemyContainer).GetComponent<Enemy>();
        _enemy.Pool = _pools[id];

        if (_listEnemies.Contains(_enemy) == false)
            _listEnemies.Add(_enemy);
    }

    public void DespawnEnemy(Enemy enemy)
    {
        for (int i = 0; i < _pools.Length; i++)
        {
            if (enemy.Pool == _pools[i])
            {
                _pools[i].Despawn(enemy.gameObject);
                _listEnemies.Remove(enemy);
                _enemiesDespawned ++;
                LevelManager.Instance.UpdateState();
            }
        }
    }

    public void DespawnAll()
    {
        foreach (Lean.Pool.LeanGameObjectPool pool in _pools)
            pool.DespawnAll();

        _enemiesDespawned = 0;
        _enemiesSpawned = 0;
    }





    //============================================ Enemy Management
    public void StartEnemies()
    {
        foreach (Enemy e in _listEnemies)
            e.ChangeState(Enemy.enemyState.wonder);
    }



    //============================================ Start Pools
    void StartPools()
    {
        _pools = new Lean.Pool.LeanGameObjectPool[_poolContainer.childCount];
        for (int i = 0; i < _poolContainer.childCount; i++)
            _pools[i] = _poolContainer.GetChild(i).GetComponent<Lean.Pool.LeanGameObjectPool>();
    }

    void SpawnRandom(Transform spawnerContainer)
    {
        foreach (Transform t in spawnerContainer)
        {
            SpawnEnemy(Random.Range(0, 3), t);
            _enemiesSpawned++;
        }
    }


    void GetRandomPattern()
    {
        _randomPattern = _patternContainer.GetChild(Random.Range(0, _patternContainer.childCount));
        SpawnRandom(_randomPattern);
    }


    public void RestartEnemies()
    {
        foreach (Lean.Pool.LeanGameObjectPool p in _pools)
            p.DespawnAll();

        _enemiesDespawned = 0;
        _enemiesSpawned = 0;

        SpawnRandom(_randomPattern);
    }
}
