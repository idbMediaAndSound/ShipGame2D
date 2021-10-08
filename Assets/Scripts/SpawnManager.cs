using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject m_EnemyPrefab;
    [SerializeField] private GameObject m_EnemyContainer;
    [SerializeField] private List <GameObject> m_PowerUpPrefabs;
    private float m_WaitTime { get; set; }
    private bool m_StopSpawning = false;
    void Start()
    {
        m_WaitTime = Random.Range(8f, 20f);
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    
    void SpawnItem(GameObject goPrefab)
    {
        float xPos = Random.Range(-9.47f, 9.47f);
        Vector3 InstancePos = new Vector3(xPos, 9f, 0f);
        GameObject newItem = Instantiate(goPrefab, InstancePos, Quaternion.identity);
        if(goPrefab == m_EnemyPrefab)
        {
            newItem.transform.parent = m_EnemyContainer.transform;
        }
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);
        while (m_StopSpawning == false)
        {
            SpawnItem(m_EnemyPrefab);
            float randomWait = Random.Range(1f, 5f);
            yield return new WaitForSeconds(randomWait);
        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3);
        while (m_StopSpawning == false)
        {
            SpawnItem(m_PowerUpPrefabs[Random.Range(0, m_PowerUpPrefabs.Count)]);
            float randomWait = Random.Range(8f, 12f);
            yield return new WaitForSeconds(randomWait);
        }
    }
    
    public void OnPlayerDeath()
    {
        m_StopSpawning = true;
    }

    
}
