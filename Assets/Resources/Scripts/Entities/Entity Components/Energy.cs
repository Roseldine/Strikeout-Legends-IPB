using System.Collections;
using UnityEngine;

public class Energy : MonoBehaviour
{
    // variables    
    [Header("Energy Variables")]
    [SerializeField] int _energyMax;
    [SerializeField] int _energy;

    [Header("Canvas Graphics")]
    [SerializeField] UnityEngine.UI.Image _energyImg;
    [SerializeField] UnityEngine.UI.Image _energyImgDelay;
    [SerializeField] UnityEngine.UI.Image _energyCharge;
    [SerializeField] float _delay;


    #region Variable Properties

    public int PlayerEnergy { get { return _energy; } }
    public UnityEngine.UI.Image EnergyCharge { get { return _energyCharge; } }

    #endregion


    //================================================== Setters & Getters
    public void SetEnergy(int ammount)
    {
        _energyMax = ammount;
        _energy = ammount;
        CalculateImgFill();
    }

    public void ResetEnergy()
    {
        _energy = _energyMax;
        CalculateImgFill();
    }

    void CalculateImgFill()
    {
        float _fill = (float)_energy / _energyMax;

        _energyImg.fillAmount = _fill;
        StartCoroutine(CR_Delay(_fill));
    }

    IEnumerator CR_Delay(float fill)
    {
        float t = 0;
        float _fill = _energyImgDelay.fillAmount;

        while (t < _delay)
        {
            _energyImgDelay.fillAmount = Mathf.Lerp(_fill, fill, t / _delay);

            t += Time.deltaTime;
            yield return null;
        }
    }

    public void ChargeStop() => StartCoroutine(CR_ChargeStop());

    IEnumerator CR_ChargeStop()
    {
        float t = 0;
        float _fill = _energyCharge.fillAmount;
        float _time = _delay * .5f;

        while (t < _delay)
        {
            _energyCharge.fillAmount = Mathf.Lerp(_fill, 0, t / _time);

            t += Time.deltaTime;
            yield return null;
        }
    }
}
