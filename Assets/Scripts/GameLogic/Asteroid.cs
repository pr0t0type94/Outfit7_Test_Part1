using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float m_moveSpeed = 10f;
    private Vector3 m_moveDirection = Vector3.zero;
    private bool m_stopBehaviour;
    private Transform m_targetPosition;

    private void Awake()
    {
        m_targetPosition = GameObject.FindGameObjectWithTag("EnemyTarget").transform;
    }
    private void Start()
    {
        Vector3 l_vectorDirector = (m_targetPosition.position - transform.position).normalized;
    }
    // Update is called once per frame
    void Update()
    {
        if (!m_stopBehaviour)
            transform.position += m_moveSpeed * m_moveDirection * Time.deltaTime;


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
                if (l_hSystems[i].isActiveAndEnabled)
                {
                    l_hSystems[i].ModifyHealtValue(-20);

                }
            }

            Destroy(this);

        }
    }
}
