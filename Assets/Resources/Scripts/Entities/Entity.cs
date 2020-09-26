
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Entity Components")]
    [SerializeField] protected string _entityName;
    [Range(1, 8)] [SerializeField] protected int _rank;
    public int _entityId { get;}
}
