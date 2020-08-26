using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_Script : MonoBehaviour
{
    [SerializeField]
    private HealthSystem m_shieldSystem = null;
    [SerializeField]
    private HealtBar_Script m_shieldBarScript = null;
    [SerializeField]
    private HealthSystem m_healthSystem = null;
    [SerializeField]
    private HealtBar_Script m_HealthBarScript = null;

    [SerializeField]
    private PowerUpsManager m_powerUpManager = null;
    [SerializeField]
    private GameObject m_shieldShaderEffect = null;
    [SerializeField]
    private GameObject m_earthMesh = null;

    private float m_waitTimeToStartHealing = 3f;
    private float m_elapsedTimeSinceHit = 0f;
    private float m_regenerateShieldAmount = 2f;
    private bool m_regenerateShield;
    
    private void Awake()
    {
        m_powerUpManager.GiveShieldPowerUp += UpgradeShield;   
        m_shieldSystem.HasBeenAttacked += ShieldHasBeenAttacked;
        m_shieldSystem.OnZeroHealthEvent += ShieldEnded;
        m_shieldSystem.enabled = true;
        m_healthSystem.enabled = false;
    }
  
    void Update()
    {    

        if (m_shieldSystem.enabled == true)
        {
            if (m_regenerateShield)
            {
                m_elapsedTimeSinceHit += Time.deltaTime;

                if (m_elapsedTimeSinceHit >= m_waitTimeToStartHealing)
                {
                    m_shieldSystem.ModifyHealtValue(m_regenerateShieldAmount);
                    m_elapsedTimeSinceHit = 0;

                }
            }
        }
    } 

    private void UpgradeShield(float _amount)
    {
        m_shieldShaderEffect.SetActive(true);

        //if shield has been destroyed, activate it again when recived powerup and disable health
        if (m_shieldSystem.enabled == false)
        {
            m_healthSystem.enabled = false;

            m_shieldBarScript.m_fillImage.enabled = true;
            m_shieldSystem.m_currentHealth = m_shieldSystem.m_maxHealth;
            m_shieldSystem.enabled = true;

        }
        //else upgrade shield and restore maxshield
        else
        {
            m_shieldSystem.m_maxHealth += _amount;
            m_shieldSystem.m_currentHealth = m_shieldSystem.m_maxHealth;
        }
    }

    private void ShieldHasBeenAttacked()
    {
        //if shield has been attacked, restart regeneration timer
        m_elapsedTimeSinceHit = 0;
        m_regenerateShield = true;

    }
    // Update is called once per frame
    private void ShieldEnded()
    {
        //if shield is destroyed, disable shield system and enable health system
        m_shieldShaderEffect.SetActive(false);
        m_shieldSystem.enabled = false;
        //fast "blink" effect
        StartCoroutine(ShieldDestroyedRoutine());

        m_healthSystem.OnZeroHealthEvent += GameOver;

    }
    private IEnumerator ShieldDestroyedRoutine()
    {

        m_earthMesh.SetActive(false);
        m_HealthBarScript.m_fillImage.enabled = false;
        yield return new WaitForSeconds(0.1f);

        m_healthSystem.enabled = true;
        m_earthMesh.SetActive(true);
        m_HealthBarScript.m_fillImage.enabled = true;
        yield return new WaitForSeconds(0.1f);

        m_earthMesh.SetActive(false);
        m_HealthBarScript.m_fillImage.enabled = false;
        yield return new WaitForSeconds(0.1f);

        m_earthMesh.SetActive(true);
        m_HealthBarScript.m_fillImage.enabled = true;

    }

    private void GameOver()
    {

        GameManager_Script.m_gameManagerInstance.GameOver();

    }

}
