using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    
    #region Singleton
    private static BlockManager instance;

    const float m_SpawnOffset = 10f;
    const float m_StartBlockPosZ = 0f;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private Queue<GameObject> m_BlocksQueue;
    public GameObject m_BlockPrefab;
    public GameObject[] m_ConnectorPrefab;

    public GameObject[] m_StartPrefabs;

    private Vector3 m_LastSpawnPosition = new Vector3(0.0f, 0.0f, m_StartBlockPosZ);

    void Start()
    {
        uint initialBlocksCount = 7;

        m_BlocksQueue = new Queue<GameObject>();
        m_BlocksQueue.Enqueue(m_StartPrefabs[0]);
        m_BlocksQueue.Enqueue(m_StartPrefabs[1]);

        for (uint i = 0; i < initialBlocksCount; i++)
            SpawnBlock();
    }

    public static void SpawnBlock()
    {
        instance.m_LastSpawnPosition.z += m_SpawnOffset;

        float probabillity = Random.Range(1f, 100f);
        if (probabillity >= 1f && probabillity <= 90f)
            instance.SpawnSimpleBlock();
        else instance.SpawnConnector();
    }

    private void SpawnSimpleBlock()
    {
        GameObject gameObject = Instantiate(instance.m_BlockPrefab,
            instance.m_LastSpawnPosition,
            instance.m_BlockPrefab.transform.rotation);
        instance.m_BlocksQueue.Enqueue(gameObject);
    }

    private void SpawnConnector()
    {
        int index = Random.Range(0, m_ConnectorPrefab.Length);
        GameObject gameObject = Instantiate(m_ConnectorPrefab[index],
            m_LastSpawnPosition,
            m_ConnectorPrefab[index].transform.rotation);
        instance.m_BlocksQueue.Enqueue(gameObject);
    }

    public static void DestroyBlock()
    {
        Destroy(instance.m_BlocksQueue.Dequeue());
    }
}
