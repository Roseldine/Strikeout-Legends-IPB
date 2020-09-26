using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] float _lifeTime;
    [SerializeField] GameObject _objectToDeactivate;
    [SerializeField] Lean.Pool.LeanGameObjectPool _pool;
    [SerializeField] bool _delete;

    public Lean.Pool.LeanGameObjectPool Pool { get { return _pool; } set { _pool = value; } }

    private void OnEnable()
    {
        StartCoroutine(CR_Lifetime());
    }

    IEnumerator CR_Lifetime()
    {
        yield return new WaitForSeconds(_lifeTime);
        
        if (_delete == false)
        {
            if (_pool == null)
                _objectToDeactivate.SetActive(false);

            else
            {
                _pool.Despawn(_objectToDeactivate);
                Debug.Log(gameObject + " Lifetime reached");
            }
        }

        else
            Destroy(_objectToDeactivate);

        
    }
}
