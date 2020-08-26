using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthSystem : MonoBehaviour
{
    public float m_maxHealth = 100f;
    public float m_currentHealth;

    public event Action<float> HealthAmountChanged = delegate { };
    public event Action HasBeenAttacked = delegate { };
    public event Action OnZeroHealthEvent = delegate { };

    private void Awake()
    {
        m_currentHealth = m_maxHealth;
    }
    // Update is called once per frame
    public void ModifyHealtValue(float _amount)
    {
             
        HasBeenAttacked();
        
        m_currentHealth += _amount;

        if (m_currentHealth >= m_maxHealth) m_currentHealth = m_maxHealth;
        if (m_currentHealth <= 0) m_currentHealth = 0;

        if (m_currentHealth <= 0)
        {
            OnZeroHealthEvent();
        }
        else
        {
            float l_currHealthPct = m_currentHealth / m_maxHealth;
            HealthAmountChanged(l_currHealthPct);

        }



    }

  

  
}
