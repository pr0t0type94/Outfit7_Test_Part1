using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons")]
public class Weapon : ScriptableObject
{
    public string m_weaponName;

    [HideInInspector]
    public float m_currentWeaponDamage;
    [HideInInspector]
    public float m_currentWeaponAttackSpeed;
    [HideInInspector]
    public List<Transform> m_shootingPointTransformList;

    [SerializeField]
    private float m_baseWeaponDamage = 2f;
    [SerializeField]
    private float m_baseWeaponAttackSpeed = 0.5f;
    [SerializeField]
    private float m_bulletSpeed = 0.5f;
    private void OnEnable()
    {
        m_currentWeaponDamage = m_baseWeaponDamage;
        m_currentWeaponAttackSpeed = m_baseWeaponAttackSpeed;
    }
    

    public void ShootWeapon(List<Transform> _shootingPointTransformList,Vector3 _bulletDirection, LayerMask _layer, string _material)
    {
        m_shootingPointTransformList = _shootingPointTransformList;

        for (int i = 0; i < _shootingPointTransformList.Count; i++)
        {
            if(_shootingPointTransformList[i].gameObject.activeInHierarchy)
            {
                GameObject l_spawnedBullet = GameManager_Script.m_gameManagerInstance.m_bulletPool.GetNextElement();
                l_spawnedBullet.layer = _layer;
                l_spawnedBullet.transform.position = _shootingPointTransformList[i].transform.position;

                Bullet_Script l_bulletScript = l_spawnedBullet.GetComponent<Bullet_Script>();
                l_bulletScript.m_moveDirection = _bulletDirection;
                l_bulletScript.m_bulletDamage = m_currentWeaponDamage;
                l_bulletScript.m_bulletMoveSpeed = m_bulletSpeed;


                l_bulletScript.m_meshRender.material = _material == "player" ? 
                    l_bulletScript.m_meshRender.material = l_bulletScript.m_playerBulletMaterial : 
                    l_bulletScript.m_meshRender.material = l_bulletScript.m_enemyBulletMaterial;


                l_spawnedBullet.SetActive(true);

            }
        }
    }

}
