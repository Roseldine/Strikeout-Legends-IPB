
using System.Collections;
using UnityEngine;

public class SpecialZeus : Attack
{
    [SerializeField] AnimationCurve _cruve;
    [SerializeField] float _animTime;

    private void OnEnable()
    {
        StartCoroutine(CR_Anim());
    }

    IEnumerator CR_Anim()
    {
        float t = 0;
        float y = transform.position.y;
        float yAnim = 0;

        while (t < _animTime)
        {
            yAnim = Mathf.Lerp(y, 0, t / _animTime);
            transform.localPosition = new Vector3(0, yAnim, 0);

            t += Time.deltaTime;
            yield return null;
        }
    }
}
