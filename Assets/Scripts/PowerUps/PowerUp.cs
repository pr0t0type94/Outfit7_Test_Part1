using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Power Ups")]
public class PowerUp : ScriptableObject
{

    public PowerUpType m_powerUpType;
    public string m_powerUpDescription;
    public float m_addedEffect;
    public Sprite m_powerUpSprite;
  
    public enum PowerUpType
    {
        AttackSpeedBoost,
        DamageBoost,
        RadiusBoost,
        ShieldUpgrade,
        WeaponUpgrade
    }


}
