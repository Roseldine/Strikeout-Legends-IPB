
using Boo.Lang;
using UnityEngine;

public class SpecialAres : Ability
{
    // every collision becomes pinned to the spear untill it reaches the wall
    [SerializeField] Transform _pinnedParent;
    List<Enemy> _enemyList;

    private void Awake()
    {
        _enemyList = new List<Enemy>();
    }

    private void Update()
    {
        foreach (Transform t in _pinnedParent)
            t.localPosition = Vector3.zero;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Found Enemy");
            Enemy _enemy = other.GetComponent<Enemy>();
            _enemy.CC_Pinned();
            other.transform.parent = _pinnedParent;

            if (_enemyList.Contains(_enemy) == false)
                _enemyList.Add(_enemy);
        }

        if (other.tag == "Wall")
        {
            foreach (Enemy e in _enemyList)
            {
                e.ChangeState(Enemy.enemyState.wonder);
                e.transform.parent = EnemyManager.Instance.EnemyContainer;
            }

            Despawn();
        }
    }
}
