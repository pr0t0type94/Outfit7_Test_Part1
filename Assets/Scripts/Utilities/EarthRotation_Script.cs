using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation_Script : MonoBehaviour
{
    [SerializeField]
    private float m_rotationSpeed = 5f;

    private Quaternion rotation;
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * m_rotationSpeed);
    }
}
