using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    // variables    
    [Header("Health Variables")]
    [SerializeField] int _healthMax;
    [SerializeField] int _health;
    bool _isDead;

    [Header("Canvas Graphics")]
    [SerializeField] UnityEngine.UI.Image _healthImg;
    [SerializeField] UnityEngine.UI.Image _healthImgDelay;
    [SerializeField] float _delay;

    #region Variable Properties

    public int PlayerHealth { get { return _health; } }

    #endregion

    //================================================== Setters & Getters
    private void OnEnable()
    {
        CalculateImgFill();
    }

    public void SetHealth(int ammount)
    {
        _healthMax = ammount;
        _health = ammount;
        CalculateImgFill();
    }

    public void ResetHealth()
    {
        _health = _healthMax;
        CalculateImgFill();
        _isDead = false;
    }

    public int GetHealth() => _health;
    public bool GetIsDead() => _isDead;

    void CalculateImgFill()
    {
        float _fill = (float)_health / _healthMax;

        _healthImg.fillAmount = _fill;
        StartCoroutine(CR_HealthDelay(_fill));
    }

    IEnumerator CR_HealthDelay(float fill)
    {
        float t = 0;
        float _fill = _healthImgDelay.fillAmount;

        while (t < _delay)
        {
            _healthImgDelay.fillAmount = Mathf.Lerp(_fill, fill, t / _delay);

            t += Time.deltaTime;
            yield return null;
        }
    }





    //================================================== Damage & Healing
    public void Damage(int ammount)
    {
        if (_isDead == false)
        {
            _health -= ammount;
            _health = Mathf.Clamp(_health, 0, _healthMax);

            CalculateImgFill();

            if (_health <= 0)
                _isDead = true;
        }
    }

    public void Heal(int ammount)
    {
        if (_isDead == false)
        {
            _health += ammount;
            _health = Mathf.Clamp(_health, 0, _healthMax);

            if (_health > 0)
                _isDead = false;
        }
    }

    public void Death()
    {

    }
}
