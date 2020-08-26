using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wave
{
    public List<GameObject> m_enemiesList = new List<GameObject>();
    public List<Vector3> m_enemiesSelectedPositions = new List<Vector3>();

    private Vector3 m_referencePosition = Vector3.zero;

    public Wave(Vector3 _referencePosition) 
    { 
        m_referencePosition = _referencePosition;
        SpawnWave();

    }



    public void SpawnWave()
    {
     
            List<Vector3> l_gridOfTransforms;
            GenerateFormation(out l_gridOfTransforms);

            for (int i = 0; i < l_gridOfTransforms.Count; i++)
            {
                int choose = UnityEngine.Random.Range(0, 3);

                if (choose == 0)
                    m_enemiesSelectedPositions.Add(l_gridOfTransforms[i]);

                else
                    continue;
            }

            for (int i = 0; i < m_enemiesSelectedPositions.Count; i++)
            {
                m_enemiesList.Add(GameManager_Script.m_gameManagerInstance.m_basicEnemiesPool.GetNextElement());
                m_enemiesList[i].transform.position = m_enemiesSelectedPositions[i];
                m_enemiesList[i].SetActive(true);

            }
        
    }
    private void GenerateFormation(out List<Vector3> _gridTransforms)
    {
        int l_randomSize = UnityEngine.Random.Range(2, 5);
        _gridTransforms = new List<Vector3>();


        for (int i = 0; i < l_randomSize * l_randomSize; i++)
        {
            _gridTransforms.Add(new Vector3(11 * (i % l_randomSize) + m_referencePosition.x, 11 * (i / l_randomSize) + m_referencePosition.y, 0));
        }
     

    }

    public bool CurrentWaveDestroyed()
    {
        int maxNumberOfEnemies = m_enemiesList.Count;
        int deadEnemies = m_enemiesList.FindAll(x => !x.activeInHierarchy).ToList().Count;

        return (maxNumberOfEnemies - deadEnemies) == 0;
    }
 
}
