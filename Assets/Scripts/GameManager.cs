using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool m_IsGameOver;
    [SerializeField] GameObject m_SpawnManager;

    private void Update()
    {
        if(m_IsGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }

   public void GameOver()
    {
        m_IsGameOver = true;
        m_SpawnManager.SetActive(false);
    }

}
