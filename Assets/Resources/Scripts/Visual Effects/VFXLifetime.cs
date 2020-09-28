
using UnityEngine;

public class VFXLifetime : MonoBehaviour
{
    public enum lifeTimeType { unlimited, limited }

    [Header("Lifetime")]
    [SerializeField] Lean.Pool.LeanGameObjectPool _pool;
    [SerializeField] lifeTimeType _lifeType;
    [SerializeField] float _lifeTime;


    public Lean.Pool.LeanGameObjectPool Pool { get { return _pool; } set { _pool = value; } }


    private void OnEnable()
    {
        Invoke("Despawn", _lifeTime);
    }


    public void Despawn() => _pool.Despawn(gameObject);



    private void OnDisable()
    {
        CancelInvoke("Despawn");
    }
}
