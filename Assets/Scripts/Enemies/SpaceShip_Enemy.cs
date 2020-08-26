using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_Enemy : SpaceShip_BaseClass
{
    [Header("Enemy Parameters")]
    [SerializeField]
    private float m_pointsWhenDie = 10f;
    [SerializeField]
    private float m_attackRadius = 20f;
    [SerializeField]
    private float m_accelerationSpeed = 20f;
    [SerializeField]
    private float m_accelerationRadius = 40f;
    [SerializeField]
    private Material m_vanishingShaderMaterial = null;
    [SerializeField]
    private Material m_normalEnemyMaterial = null;

    private AudioSource m_audioSource = null;
    private Transform m_targetPosition = null;
    private void Awake()
    {
        GetComponent<HealthSystem>().OnZeroHealthEvent += Die;
        m_audioSource = GetComponent<AudioSource>();
        m_targetPosition = GameObject.FindGameObjectWithTag("EnemyTarget").transform;
    }
    private void OnEnable()
    {
        GetComponent<Collider>().enabled = true;
        GetComponentInChildren<MeshRenderer>().material = m_normalEnemyMaterial;

    }
    private void Start()
    {
        GameManager_Script.m_gameManagerInstance.StopAllBehaviours += StopBehaviours;
        GameManager_Script.m_gameManagerInstance.ContinueAllBehaviours += ContinueBehaviours;


    }

    void Update()
    {
        if (!m_stopAllBehaviours)
        {

            if (IsTargetInsideRadius(m_attackRadius))
            {
                if (Time.time >= m_nextTimeToFire)
                {
                    m_nextTimeToFire = Time.time + 1f / m_equippedWeapon.m_currentWeaponAttackSpeed;
                    Shoot();

                }
            }
            else
            {
                Rotate();

                if(IsTargetInsideRadius(m_accelerationRadius))
                {
                    Move(m_baseMoveSpeed);

                }
                else
                {
                    Move(m_accelerationSpeed);
                }

            }
        }
        else
        {
            //wait
        }
    }

   

    public bool IsTargetInsideRadius(float _radius)
    {
        Vector3 l_vectorDirector = m_targetPosition.position - transform.position;          
        return (l_vectorDirector.sqrMagnitude < _radius * _radius) ? true : false;
    }
    public override void Move(float _moveSpeed)
    {
        Vector3 l_vectorDirector = (m_targetPosition.position - transform.position).normalized;
        transform.position += l_vectorDirector * _moveSpeed * Time.deltaTime;
    }
    


    public void Rotate()
    {
    
        transform.GetChild(0).LookAt(m_targetPosition); 

    }
    public override void Shoot()
    {

        m_audioSource.PlayOneShot(m_audioSource.clip);
        Vector3 l_vectorDirector = (m_targetPosition.position - transform.position).normalized;
        m_equippedWeapon.ShootWeapon(m_shootingPointTransformList, l_vectorDirector, LayerMask.NameToLayer("Enemy-Bullets"),"enemy");


    }

    public override void Die()
    {
        StartCoroutine(DestroyGO());
    }

    private IEnumerator DestroyGO()
    {
        GetComponentInChildren<MeshRenderer>().material = m_vanishingShaderMaterial;
        GameManager_Script.m_gameManagerInstance.AddPoints(m_pointsWhenDie);
        GetComponent<Collider>().enabled = false;
        m_stopAllBehaviours = true;
        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
        //TODO:
        //instantiate sound and particles
        

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
