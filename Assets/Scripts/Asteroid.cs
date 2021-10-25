using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    
    [SerializeField] private Player m_Player;
    [SerializeField] private GameObject m_ExplosionPrefab;
    [SerializeField] private Collider2D m_Collider2D;
    [SerializeField] private SpriteRenderer m_AsteroidSprite;
    [SerializeField] private AnimationClip m_Anim;
    [SerializeField] private GameObject m_ExplosionContainer;
    [SerializeField] private SpawnManager m_SpawnManager;

    private float m_RotationSpeed = 10;
    
    [SerializeField] private float m_MaxRotationSpeed { get; set; }

void Start()
    {
       CheckReferences();
    }

    // Update is called once per frame
    void Update()
    {
        RotateAsteroid();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            m_Player.AddScore(5);
            InstatiateExplosion();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if(m_Player != null)
            {
                m_Player.Damage();
            }
            InstatiateExplosion();
        }
        
    }

    void RotateAsteroid()
    {
        transform.Rotate(Vector3.forward * (Time.deltaTime * m_RotationSpeed) , Space.Self);
    }

    void InstatiateExplosion()
    {
        m_AsteroidSprite.enabled = false;
        GameObject newAsteroid = Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
        AkSoundEngine.PostEvent("Explossion", gameObject);
        newAsteroid.transform.parent = m_ExplosionContainer.transform;
        m_Collider2D.enabled = false;
        m_SpawnManager.StartSpawning();
        Destroy(this.gameObject, m_Anim.length);
    }

    void CheckReferences()
    {
        if (m_Player == null)
        {
            m_Player = GameObject.Find("Player").GetComponent<Player>();
        }
        if (m_SpawnManager == null)
        {
            m_SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        }
    }
}
