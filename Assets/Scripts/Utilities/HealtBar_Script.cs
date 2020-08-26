using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtBar_Script : MonoBehaviour
{

    public Image m_fillImage = null;
    [SerializeField]
    private float m_barUpdateSpeed = 1f;
    [SerializeField]
    private HealthSystem m_healthSystem = null;
    
private void Awake()
    {
        m_fillImage.enabled = true;
        m_healthSystem.HealthAmountChanged += HandleHealtChangeEvent;
        m_healthSystem.OnZeroHealthEvent += DisableFillImage;
    }
 
    private void HandleHealtChangeEvent(float _amount)
    {
        StartCoroutine(ChangeHealthPct(_amount));
    }
    public void SuscribeHealthEvent()
    {
        m_healthSystem.HealthAmountChanged += HandleHealtChangeEvent;

    }
    public void UnsuscribeHealthEvent()
    {
        m_healthSystem.HealthAmountChanged -= HandleHealtChangeEvent;

    }
    private void DisableFillImage()
    {
        m_fillImage.enabled = false;

    }

    private void EnableFillImage()
    {
        m_fillImage.enabled = true;

    }
    //smooth decreasing fill amount
    private IEnumerator ChangeHealthPct(float _amount)
    {
        float l_preChangeHealthPct = m_fillImage.fillAmount;
        float l_elapsedTime = 0f;

        while(l_elapsedTime < m_barUpdateSpeed)
        {
            l_elapsedTime += Time.deltaTime;
            m_fillImage.fillAmount = Mathf.Lerp(l_preChangeHealthPct, _amount, l_elapsedTime/m_barUpdateSpeed);
            yield return null;
        }

        m_fillImage.fillAmount = _amount;
    }
}
