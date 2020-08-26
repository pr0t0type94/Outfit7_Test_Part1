using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private List<BoxCollider> m_worldBounds = new List<BoxCollider>();

    private Vector3 m_startingWavePosition;
    private Wave m_nextWaveToSpawn = null;
    private int m_wavesCounter = 0;

    private GameObject m_asteroidPrefab;

    public event Action RewardPlayer;
    void Start()
    {
        SpawnNewWave();
    }

    // Update is called once per frame
    void Update()
    {

        if (m_nextWaveToSpawn != null)
        {
           if (m_nextWaveToSpawn.CurrentWaveDestroyed())
            {
                if (m_wavesCounter % 3 == 0)
                {
                    RewardPlayer();
                }
                SpawnNewWave();
            }
        }


    }
  
    public void SpawnNewWave()
    {
       
        int l_random = UnityEngine.Random.Range(0, 4);
        m_startingWavePosition = RandomPointInCollider(m_worldBounds[l_random]);
        m_nextWaveToSpawn = new Wave(m_startingWavePosition);
        m_wavesCounter++;

    }
    public Vector3 RandomPointInCollider(Collider _collider)
    {
        return new Vector3(_collider.transform.position.x, _collider.transform.position.y, 0);
     
    }
}
