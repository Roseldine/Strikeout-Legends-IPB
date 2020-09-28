
using Boo.Lang;
using System.Collections;
using UnityEngine;

public class SpecialPoseidon : Ability
{
    [Header("Wave Crowd Control")]
    [SerializeField] AnimationCurve _curve;
    [SerializeField] float _ccDuration;
    [SerializeField] float _kockUpHeight;

    [Header("Speed")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;

    List<Enemy> _enemyList;

    private void Awake()
    {
        _enemyList = new List<Enemy>();
    }

    private void OnEnable()
    {
        _rb.velocity = transform.forward * _speed;
    }

    private void OnDisable()
    {
        _rb.velocity = Vector3.zero;
        StopAllCoroutines();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Debug.Log("Found Enemy");
            Enemy _enemy = other.GetComponent<Enemy>();
            _enemy.CC_KockUp();
            StartCoroutine(CR_Kockup(_enemy));
        }
    }


    IEnumerator CR_Kockup(Enemy enemy)
    {
        float t = 0;
        Vector3 initPos = enemy.transform.position;

        Keyframe[] _keyframes = _curve.keys;
        _keyframes[1].time = _ccDuration / 2;
        _keyframes[1].value = _kockUpHeight;
        _keyframes[2].time = _ccDuration;
        _curve.keys = _keyframes;

        while (t < _ccDuration)
        {
            enemy.transform.position = new Vector3(initPos.x, _curve.Evaluate(t), initPos.z);

            t += Time.deltaTime;
            yield return null;
        }

        enemy.ChangeState(Enemy.enemyState.wonder);
    }
}
