using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class PlayerSpaceship : SpaceShip_BaseClass
{
    [Header("Player Parameters")]
    public bool m_canShoot;
    [HideInInspector]
    public float m_currentPlayerRadius;

    [SerializeField]
    private CustomJoystick m_movementJoystic = null;
    [SerializeField]
    private CustomJoystick m_shootJoystic = null;

    [SerializeField]
    private float m_basePlayerRadius = 30f;
    [SerializeField]
    private PowerUpsManager m_powerUpManager = null;

    private Transform m_playerReferencePosition = null;

    private List<Weapon> m_playerWeapons = null;
    private AudioSource m_audioSource = null;
    
    private void Awake()
    {
        m_playerWeapons = Resources.LoadAll<Weapon>("Weapons/PlayerWeapons").ToList();
        m_playerReferencePosition = FindObjectOfType<Earth_Script>().gameObject.transform;
        m_currentPlayerRadius = m_basePlayerRadius;
        m_audioSource = GetComponent<AudioSource>();
        
    }

    void Start()
    {
        GameManager_Script.m_gameManagerInstance.StopAllBehaviours += StopBehaviours;
        GameManager_Script.m_gameManagerInstance.ContinueAllBehaviours += ContinueBehaviours;
        m_powerUpManager.GivePowerUpToPlayer += GivePlayerPowerUp;
    }


    void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER //testing on pc purposes 
        m_moveDirection.x = Input.GetAxis("Horizontal");
        m_moveDirection.y = Input.GetAxis("Vertical"); 
        m_moveDirection.z = m_initialPlayerPosition.z;
        m_moveDirection.Normalize();
        transform.position += m_moveDirection * m_baseMoveSpeed * Time.deltaTime;
    

#elif UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE

        if (!m_stopAllBehaviours)
        {
            Rotate();

            Move(m_baseMoveSpeed);
         

            //getMouseButton works for all kind of inputs (touch and mouse)
            if (Input.GetMouseButton(0) && m_canShoot)
            {

                if (Time.time >= m_nextTimeToFire)
                {
                    m_nextTimeToFire = Time.time + 1f / m_equippedWeapon.m_currentWeaponAttackSpeed;
                    Shoot();

                }
            }
        }
#endif
    }

  
    public override void Move(float _moveSpeed)
    {

        Vector3 l_refVectorWithoutZ = new Vector3(m_playerReferencePosition.position.x, m_playerReferencePosition.position.y, 0);
        Vector3 l_vectorDirector = transform.position -  l_refVectorWithoutZ;
        float l_distanceToReference = l_vectorDirector.sqrMagnitude;

        if(l_distanceToReference > m_basePlayerRadius* m_basePlayerRadius)
        {

            transform.position = l_refVectorWithoutZ;

        }
        else
        {
            transform.position += m_movementJoystic.m_moveDirection.normalized * _moveSpeed * Time.deltaTime;
        }

    }

    public void Rotate()
    {
        float angle = Mathf.Atan2(m_shootJoystic.m_moveDirection.y, m_shootJoystic.m_moveDirection.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);

    }
    public override void Shoot()
    {
        m_audioSource.PlayOneShot(m_audioSource.clip);
        m_equippedWeapon.ShootWeapon(m_shootingPointTransformList,m_shootJoystic.m_moveDirection, LayerMask.NameToLayer("Player-Bullets"), "player");

    }



    public override void Die()
    {
   
    }

 
    private void GivePlayerPowerUp(PowerUp _powerUp)
    {
        if (_powerUp.m_powerUpType == PowerUp.PowerUpType.AttackSpeedBoost)            
        {
            m_equippedWeapon.m_currentWeaponAttackSpeed += _powerUp.m_addedEffect;
        }
        else if(_powerUp.m_powerUpType == PowerUp.PowerUpType.DamageBoost)
        {
            m_equippedWeapon.m_currentWeaponDamage += _powerUp.m_addedEffect;

        }
        else if(_powerUp.m_powerUpType == PowerUp.PowerUpType.RadiusBoost)
        {
            m_currentPlayerRadius += _powerUp.m_addedEffect;

        }
        else if(_powerUp.m_powerUpType == PowerUp.PowerUpType.WeaponUpgrade)
        {
            if(m_equippedWeapon.m_weaponName == "Simple")
            {
                m_equippedWeapon = m_playerWeapons.Find(x => x.m_weaponName == "Double");
                m_shootingPointTransformList[0].gameObject.SetActive(false);
                m_shootingPointTransformList[1].gameObject.SetActive(true);
                m_shootingPointTransformList[2].gameObject.SetActive(true);
            }
            else if(m_equippedWeapon.m_weaponName == "Double")
            {
                m_equippedWeapon = m_playerWeapons.Find(x => x.m_weaponName == "Triple");
                m_shootingPointTransformList[0].gameObject.SetActive(true);
                m_shootingPointTransformList[1].gameObject.SetActive(true);
                m_shootingPointTransformList[2].gameObject.SetActive(true);
            }
        }
       
    }


    private void StopBehaviours()
    {
        m_stopAllBehaviours = true;
    }
    private void ContinueBehaviours()
    {
        m_stopAllBehaviours = false;
    }

}
