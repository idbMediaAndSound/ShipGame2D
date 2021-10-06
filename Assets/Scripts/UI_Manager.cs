using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{ 
    //[SerializeField] Text m_Lives;
    [SerializeField] Text m_Score;
    [SerializeField] Player m_Player;
    [SerializeField] DisplayLives m_DisplayLives;
    [SerializeField] GameObject m_GameOverScreen;
    [SerializeField] GameObject m_GameOverText;
    [SerializeField] GameManager m_GameManager;


    void Start()
    {
        CheckForReferences();
        HideGameOverScreen();
        UpdateScore();
    }

    public void UpdateScore()
    {
        m_Score.text = $"Score: {m_Player.Score}";
    }

    public void UpdateLives()
    {
        m_DisplayLives.UpdateLives(m_Player.PlayersLife);
    }

    void HideGameOverScreen()
    {
        if (m_GameOverScreen.activeInHierarchy)
        {
            m_GameOverScreen.SetActive(false);
        }
        else
        {
            return;
        }
    }
    public void ShowGameOverScreen()
    {
        m_GameOverScreen.SetActive(true);
        m_GameManager.GameOver();
        if (m_GameOverText.activeInHierarchy)
        {
            StartCoroutine(GameOverFlicker());
        }
    }
    void CheckForReferences()
    {
        if (m_DisplayLives == null)
        {
            m_DisplayLives = GameObject.Find("Lives_Display_Image").GetComponent<DisplayLives>();
        }
        if (m_Score == null)
        {
            m_Score = GameObject.Find("Score_Text").GetComponent<Text>();
        }
        if (m_Player == null)
        {
            m_Player = GameObject.Find("Player").GetComponent<Player>();
        }
        if (m_GameOverScreen == null)
        {
            m_GameOverScreen = GameObject.Find("GameOverPanel");
        }
        if (m_GameManager == null)
        {
            m_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); ;
        }
    }



    IEnumerator GameOverFlicker()
    {
        m_GameOverText.SetActive(true);
        yield return new WaitForSeconds(Random.Range(0.2f, 0.7f));
        m_GameOverText.SetActive(false);
        yield return new WaitForSeconds(Random.Range(0.2f, 0.7f));
        StartCoroutine(GameOverFlicker());
    }

}
