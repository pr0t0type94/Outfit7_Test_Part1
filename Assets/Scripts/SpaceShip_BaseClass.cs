using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShip_BaseClass : MonoBehaviour , I_Shootable
{
    [SerializeField]
    protected GameObject m_bulletPrefab;
    [SerializeField]
    protected float m_baseMoveSpeed;
    [SerializeField]
    protected float m_baseAttackSpeed;
    [SerializeField]
    protected float m_baseDamageDealt;
    [SerializeField]
    protected float m_maxHealth;
    protected float m_currentHealth;
    [SerializeField]
    protected List<Transform> m_shootingPointTransformList;


    protected Vector3 m_moveDirection;

    public abstract void InstantiateBullet(Vector3 _position);
    protected abstract void Shoot();

    public abstract void LooseHealth(int _damage);

    public abstract void Die();

}
