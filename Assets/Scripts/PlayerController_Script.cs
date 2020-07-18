using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Script : SpaceShip_BaseClass
{
    private Vector3 m_initialPlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        m_initialPlayerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
            //m_moveDirection.x = Input.GetAxis("Horizontal") * Time.deltaTime;
            //m_moveDirection.y = m_currentPosition.y;
            //m_moveDirection.z = Input.GetAxis("Vertical") * Time.deltaTime;
            //m_moveDirection.Normalize();
            //transform.position += m_moveDirection * m_baseMoveSpeed * Time.deltaTime;


#else

        if (Input.touchCount > 0)
        {

            Vector3 l_playerDirection;
            Touch l_touch = Input.GetTouch(0);
            Vector3 l_touchPosition = Camera.main.ScreenToWorldPoint(l_touch.position);

            l_playerDirection.x = (l_touchPosition.x >= Screen.width / 2) ? 1 : -1;
            l_playerDirection.y = (l_touchPosition.x >= Screen.height / 2) ? 1 : -1;

            Vector3 l_vectorDirector = l_touchPosition - transform.position; //distance vector

            l_vectorDirector.y = m_initialPlayerPosition.y;
            l_vectorDirector.Normalize();

            transform.position += l_vectorDirector * m_baseMoveSpeed * Time.deltaTime;

        }
    }
#endif
    private void Move()
    {
        
    }

    protected override void Shoot()
    {

    }
    public override void LooseHealth(int _damage)
    {
        m_currentHealth -= _damage;
    }
    public override void Die()
    {
        //TODO:
        //end game menu -> exit or continue (Watch ad)
        //reset player score and position
    }

    public void resetPosition()
    {

    }

    public override void InstantiateBullet(Vector3 _position)
    {

    }
}
