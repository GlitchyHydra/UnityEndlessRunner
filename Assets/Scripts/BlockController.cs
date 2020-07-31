using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [System.Serializable]
    public struct Environment
    {
        public GameObject[] Prefabs;
        public Transform[] SpawnPoints;
    }

    public Environment m_Obstacles;
    public Environment m_Background;

    public GameObject m_CoinObject;

    private const uint m_PlayerLayer = 10;

    private int m_MidIndex;

    private uint m_HighObstaclesCount = 0;

    private void Start()
    {
        foreach (Transform transformObst in m_Obstacles.SpawnPoints)
        {
            SpawnBlockEntities(transformObst);
        }

        m_MidIndex = m_Obstacles.Prefabs.Length / 2 + 1;

        foreach(Transform transform in m_Background.SpawnPoints)
        {
            GameObject gameObject = GetRandom(m_Background.Prefabs,
                0,
                m_Background.Prefabs.Length);
            Instantiate(gameObject, transform);
        }
    }

    private GameObject GetRandom(GameObject[] array, int initial, int length)
    {
        uint index = (uint)UnityEngine.Random.Range(initial, length);
        return array[index];
    }

    private void SpawnBlockEntities(Transform transform)
    {
        float probability = UnityEngine.Random.Range(0f, 100f);

        if (probability >= 0f && probability < 7f)
        {
            Instantiate(m_CoinObject, transform);
        }
        else if (probability >= 7f && probability < 35f)
        {
            Instantiate(GetRandom(m_Obstacles.Prefabs, 0, m_MidIndex), transform);
        }
        else if (probability >= 35f && probability < 55f)
        {
            if (++m_HighObstaclesCount <= 2) 
                Instantiate(GetRandom(m_Obstacles.Prefabs, m_MidIndex, m_Obstacles.Prefabs.Length), transform);
        }
        else
        {
            //Spawn nothing
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == m_PlayerLayer)
        {
            BlockManager.SpawnBlock();
            BlockManager.DestroyBlock();
        }
    }
}
