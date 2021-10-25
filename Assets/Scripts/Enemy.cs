using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float m_Speed = 4f;
    [SerializeField] protected float m_YLimit = 6.42f;
    [SerializeField] private Player m_Player;
    [SerializeField] private TypeOfEnemy m_TypeOfEnemy = new TypeOfEnemy();
    [SerializeField] private Animator m_Anim;
    [SerializeField] private AnimationClip m_DestroyEnemy;
    [SerializeField] private Collider2D m_Collider2D;
    [SerializeField] private GameObject m_LaserPrefab;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField] AK.Wwise.Event m_LaserShotSfx;
    [SerializeField] AK.Wwise.Event m_Explossion;
    protected float m_RandomXPos;
    private float m_FireRate = 3f;
    private float m_CanFire = 1f;
    
    void Start()
    {
        m_Player = FindObjectOfType<Player>();

        if (m_Collider2D == null)
        {
            m_Collider2D = GetComponent<Collider2D>();
        }
        StartCoroutine(EnemyShot());

    }

    void Update()
    {
        CalculateMovement();
        Respawn();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * (m_Speed * Time.deltaTime));
    }

    public virtual void Respawn()
    {
        if (transform.position.y < -m_YLimit)
        {
            m_RandomXPos = Random.Range(-9.51f, 9.51f);
            m_Speed = Random.Range(4f, 8f);
            transform.position = new Vector3(m_RandomXPos, m_YLimit, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Laser"))
        {
            if(m_TypeOfEnemy == TypeOfEnemy.regular)
            {
                m_Player.AddScore(10);
            }
            else
            {
                return;
            }
            DestroyEnemy(other);
            m_Explossion.Post(gameObject);
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            if(m_Player != null)
            {
                m_Player.Damage();
            }
            DestroyEnemy(other);
            m_Explossion.Post(gameObject);
        }
        else
        {
            return;
        }
    }

    void DestroyEnemy(Collider2D other)
    {
        m_Anim.SetTrigger("OnEnemyDeath");
        m_Collider2D.enabled = false;
        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
        }
        Destroy(this.gameObject, m_DestroyEnemy.length); 
    }


    private IEnumerator EnemyShot()
    {
        while (true)
        { 
            Instantiate(m_LaserPrefab, transform.position, Quaternion.identity);
            m_LaserShotSfx.Post(gameObject);
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));
        }
        
    }
}
