using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectsPool_Script
{

    public List<GameObject> m_Elements;
    int m_CurrentElementId = 0;
    
     
    public GameobjectsPool_Script(int _elementsCount, GameObject _prefab, Transform _parent = null)
    {
        m_Elements = new List<GameObject>(_elementsCount);

        for (int i = 0; i < _elementsCount; i++)
        {
            GameObject l_obj = GameObject.Instantiate(_prefab, _parent);
            l_obj.SetActive(false);
            m_Elements.Add(l_obj);
        }

    }
    public GameObject GetNextElement()
    {
        m_CurrentElementId += 1;

        if (m_CurrentElementId == m_Elements.Count)
        {
            m_CurrentElementId = 0;
        }

        return m_Elements[m_CurrentElementId];

    }
}
