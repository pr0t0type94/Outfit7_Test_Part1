using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShip_BaseClass : MonoBehaviour
{
    [Header("BaseClass Parameters")]
    public bool m_stopAllBehaviours = false;
    protected Vector3 m_moveDirection;
    
    protected float m_nextTimeToFire;

    [SerializeField]
    protected float m_baseMoveSpeed;
    [SerializeField]
    protected List<Transform> m_shootingPointTransformList;
    [SerializeField]
    protected Weapon m_equippedWeapon;
    

    //public abstract void InstantiateBullet(Vector3 _position);
    public abstract void Move(float _speed);

    public abstract void Shoot();
    
    public abstract void Die();

}
