using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpsManager : MonoBehaviour
{
    private static GameManager_Script _instance;
    public static GameManager_Script m_gameManagerInstance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("GameManager null");
            }
            return _instance;

        }
    }
    private List<PowerUp> m_powerUpsList = new List<PowerUp>();

    private PowerUp m_selectedPowerUp1;
    private PowerUp m_selectedPowerUp2;

    public event Action ReceivePowerUpEffect = delegate { };

    public TextMeshProUGUI m_powerUp1_DescriptionTextMesh;
    public TextMeshProUGUI m_powerUp2_DescriptionTextMesh;
     
    public Image m_powerUp1_Sprite;
    public Image m_powerUp2_Sprite;


    public event Action<PowerUp> GivePowerUpToPlayer = delegate { };
    public event Action<float> GiveShieldPowerUp = delegate { };
    public event Action UpgradePlayerWeapon = delegate { };

    private void OnEnable()
    {
        m_powerUpsList = Resources.LoadAll<PowerUp>("PowerUps").ToList();
        //select random powerup
        m_selectedPowerUp1 = SelectRandomPowerUp();
        m_selectedPowerUp2 = SelectRandomPowerUp();

        m_powerUp1_DescriptionTextMesh.text = m_selectedPowerUp1.m_powerUpDescription;
        m_powerUp2_DescriptionTextMesh.text = m_selectedPowerUp2.m_powerUpDescription;

        m_powerUp1_Sprite.sprite = m_selectedPowerUp1.m_powerUpSprite;
        m_powerUp2_Sprite.sprite = m_selectedPowerUp2.m_powerUpSprite;

    }

    void GivePowerUp(PowerUp _powerUpToGive)
    {
        if (_powerUpToGive.m_powerUpType == PowerUp.PowerUpType.AttackSpeedBoost || _powerUpToGive.m_powerUpType == PowerUp.PowerUpType.DamageBoost  
            || _powerUpToGive.m_powerUpType == PowerUp.PowerUpType.RadiusBoost || _powerUpToGive.m_powerUpType == PowerUp.PowerUpType.WeaponUpgrade)
        {
            GivePowerUpToPlayer(_powerUpToGive);
        }
        else if (_powerUpToGive.m_powerUpType == PowerUp.PowerUpType.ShieldUpgrade)
        {
            GiveShieldPowerUp(_powerUpToGive.m_addedEffect);
        }
       
    }
    private PowerUp SelectRandomPowerUp()
    {
        int l_random = UnityEngine.Random.Range(0, m_powerUpsList.Count());
        return m_powerUpsList[l_random];
    }

    public void GiveSelectedPowerUp_Button1()
    {
        GivePowerUp(m_selectedPowerUp1);
        ReceivePowerUpEffect();

    }

    public void GiveSelectedPowerUp_Button2()
    {
        GivePowerUp(m_selectedPowerUp2);
        ReceivePowerUpEffect();
    }
}
