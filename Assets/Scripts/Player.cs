using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    #region Variables

    private int m_PlayersLifes = 3;
    [SerializeField] public int Score = 0;

    [SerializeField]
    public int PlayersLife
    {
        get { return m_PlayersLifes; }
        set
        {
            if (PlayersLife <= 0)
            {
                m_PlayersLifes = 0;
            }
            else
            {
                m_PlayersLifes = value;
            }
        }
    }

    [Header("Input Method")] [SerializeField]
    private string m_HorizontalAxis = "Horizontal";

    [SerializeField] private string m_VerticalAxis = "Vertical";
    [SerializeField] private string m_Fire1 = "Fire 1";

    [Header("Variables")] [Range(0, 5)] [SerializeField]
    private float m_Speed = 3.5f;

    [SerializeField] private float m_SpeedMultiplier = 3f;
    [SerializeField] private float x_Limit = 11.36f;
    [SerializeField] private GameObject m_LaserPrefab;
    [SerializeField] private GameObject m_TripleShotPrefab;
    [SerializeField] private GameObject m_ShieldPrefab;
    [SerializeField] private Vector3 m_LaserOffset;
    [SerializeField] private float m_time;
    [SerializeField] private float m_FireRate = 0.5f;
    [SerializeField] private float m_CanFire = -1f;
    private SpawnManager m_spawnManager { get; set; }
    [SerializeField] UI_Manager m_UIManager;
    [SerializeField] DisplayLives m_DisplayLives;
    [SerializeField] private GameObject[] m_EnginesPrefab;
    [SerializeField] private GameObject m_Thruster;

    private bool m_IsTripleShotActive = false;
    private bool m_IsSpeedActive = false;
    private bool m_IsShieldActive = false;

    #endregion Variables

    void Start()
    {
        PlayersLife = 3;
        CheckForUI();
        CheckForSpawnManager();
        SetStartPoint();
    }

    private void CheckForSpawnManager()
    {
        if (m_spawnManager == null)
        {
            m_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        }
    }

    void Update()
    {
        m_time = Time.time;
        CalculateMovement();
        if (Input.GetButton(m_Fire1) && Time.time > m_CanFire)
        {
            ShootLaser();
        }
    }


    #region Methods

    private void SetStartPoint()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    private void CalculateMovement()
    {
        float x_value = Input.GetAxis(m_HorizontalAxis);
        float y_value = Input.GetAxis(m_VerticalAxis);
        Vector3 direction = new Vector3(x_value, y_value, 0);

        transform.Translate(direction * (m_Speed * Time.deltaTime));
        HorizontalWrapping();
        VerticalRestrain();
    }

    private void HorizontalWrapping()
    {
        if (transform.position.x > x_Limit)
        {
            transform.position = new Vector3(-x_Limit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -x_Limit)
        {
            transform.position = new Vector3(x_Limit, transform.position.y, transform.position.z);
        }
    }

    private void VerticalRestrain()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5f, 5f), 0);
    }

    private void ShootLaser()
    {
        m_CanFire = Time.time + m_FireRate;

        if (m_IsTripleShotActive == true)
        {
            GameObject tripleShot =
                Instantiate(m_TripleShotPrefab, transform.position + m_LaserOffset, Quaternion.identity);
            AkSoundEngine.PostEvent("TripleShotFire", gameObject);
        }
        else
        {
            GameObject laser = Instantiate(m_LaserPrefab, transform.position + m_LaserOffset, Quaternion.identity);
            AkSoundEngine.PostEvent("LaserFire", gameObject);
        }
    }

    public void Damage()
    {
        if (!m_IsShieldActive)
        {
            PlayersLife--;

            if (PlayersLife < 1)
            {
                m_spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
                m_UIManager.ShowGameOverScreen();
            }
        }
        else
        {
            m_IsShieldActive = false;
            m_ShieldPrefab.SetActive(false);
        }

        m_UIManager.UpdateLives();
        SetEngineOnFire();
    }

    void SetEngineOnFire()
    {
        switch (PlayersLife)
        {
            case 2:
                m_EnginesPrefab[Random.Range(0,2)].SetActive(true);
                break;
            case 1:
                if (m_EnginesPrefab[0].activeInHierarchy)
                {
                    m_EnginesPrefab[1].SetActive(true);
                }
                else
                {
                    m_EnginesPrefab[0].SetActive(true);
                }
                
                break;
            default:
                return;
        }

        StartCoroutine(ThrusterBlink());
    }

    public void AddScore(int points)
    {
        Score += points;
        m_UIManager.UpdateScore();
    }

    void CheckForUI()
    {
        if (m_UIManager == null)
        {
            m_UIManager = GameObject.Find("UI").GetComponent<UI_Manager>();
        }
    }

    #endregion Methods

    #region Powerups

    public void ActivateTripleShot()
    {
        if (!m_IsTripleShotActive)
            StartCoroutine(PowerUpsCoolDown(CollectablePowerUps.TripleShot));
    }

    public void ActivateSpeed()
    {
        if (!m_IsSpeedActive)
            StartCoroutine(PowerUpsCoolDown(CollectablePowerUps.Speed));
    }

    public void ActivateShield()
    {
        if (!m_IsShieldActive)
            StartCoroutine(PowerUpsCoolDown(CollectablePowerUps.Shield));
    }

    #endregion Powerups

    #region Coroutines

    private IEnumerator PowerUpsCoolDown(CollectablePowerUps collectablePowerUps)
    {
        switch (collectablePowerUps)
        {
            case CollectablePowerUps.TripleShot:
                m_IsTripleShotActive = true;
                yield return new WaitForSeconds(5.0f);
                m_IsTripleShotActive = false;
                break;
            case CollectablePowerUps.Speed:
                m_Speed *= m_SpeedMultiplier;
                m_IsSpeedActive = true;
                AkSoundEngine.SetRTPCValue("MusicSpeed", 1.5f);
                yield return new WaitForSeconds(7.0f);
                m_Speed /= m_SpeedMultiplier;
                m_IsSpeedActive = false;
                AkSoundEngine.SetRTPCValue("MusicSpeed", 1f);
                break;
            case CollectablePowerUps.Shield:
                m_IsShieldActive = true;
                m_ShieldPrefab.SetActive(true);
                yield return new WaitForSeconds(10.0f);
                m_ShieldPrefab.SetActive(false);
                m_IsShieldActive = false;
                break;
        }
    }

    private IEnumerator ThrusterBlink()
    {
        while (PlayersLife <= 2)
        {
            switch (PlayersLife)
            {
                case 2:
                    yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
                    m_Thruster.SetActive(false);
                    yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
                    m_Thruster.SetActive(true);
                    break;
                case 1:
                    yield return new WaitForSeconds(Random.Range(0.1f, 0.6f));
                    m_Thruster.SetActive(false);
                    yield return new WaitForSeconds(Random.Range(0.1f, 0.6f));
                    m_Thruster.SetActive(true);
                    break;
            }
        }
    }

    #endregion coroutines
}