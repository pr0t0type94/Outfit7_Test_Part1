using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Script : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float m_cameraSpeed = 1f;
    [SerializeField]
    private Transform m_cameraTarget = null;

    private Vector3 m_movePosition;
    private Vector3 m_initialPosition;
    private void Awake()
    {
        m_initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        m_movePosition.x = Mathf.Lerp(transform.position.x, m_cameraTarget.position.x, m_cameraSpeed * Time.deltaTime);
        m_movePosition.y = Mathf.Lerp(transform.position.y, m_cameraTarget.position.y, m_cameraSpeed * Time.deltaTime);
        m_movePosition.z = m_initialPosition.z;

        transform.position = m_movePosition;



    }
}
