using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    public Material m_playerBulletMaterial;
    public Material m_enemyBulletMaterial;

    [HideInInspector]
    public Vector3 m_moveDirection;
    [HideInInspector]
    public float m_bulletDamage;
    [HideInInspector]
    public MeshRenderer m_meshRender;
    [HideInInspector]
    public float m_bulletMoveSpeed = 1f;

    private Transform m_initialBulletPosition;
    private bool m_stopBehaviour;

    private void Awake()
    {
        m_meshRender = this.gameObject.GetComponentInChildren<MeshRenderer>();

    }
    private void OnEnable()
    {
        float l_angle = Mathf.Atan2(m_moveDirection.y, m_moveDirection.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(l_angle, Vector3.forward);
        transform.rotation = rotation;
        m_initialBulletPosition = this.transform;
    }
    private void Start()
    {
        GameManager_Script.m_gameManagerInstance.StopAllBehaviours += StopBehaviours;
        GameManager_Script.m_gameManagerInstance.ContinueAllBehaviours += ContinueBehaviours;
    }

    void Update()
    {
        if(!m_stopBehaviour)
        transform.position += m_bulletMoveSpeed * m_moveDirection * Time.deltaTime;


    }
    private void StopBehaviours()
    {
        m_stopBehaviour = true;
    }
    private void ContinueBehaviours()
    {
        m_stopBehaviour = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponents<HealthSystem>() != null)
        {
            HealthSystem[] l_hSystems = other.gameObject.GetComponents<HealthSystem>();
            for (int i = 0; i < l_hSystems.Length; i++)
            {
                if(l_hSystems[i].isActiveAndEnabled)
                {
                    l_hSystems[i].ModifyHealtValue(-m_bulletDamage);

                }
            }

            this.gameObject.SetActive(false);
            gameObject.transform.position = m_initialBulletPosition.position;
        }

        else if (other.gameObject.CompareTag("WorldLimit"))
        {
            this.gameObject.SetActive(false);
            gameObject.transform.position = m_initialBulletPosition.position;
        }
    }




}
