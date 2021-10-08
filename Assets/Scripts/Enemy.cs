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
    protected float m_RandomXPos;
    [SerializeField] private AnimationClip m_DestroyEnemy;
    [SerializeField] private Collider2D m_Collider2D;
    void Start()
    {
        m_Player = FindObjectOfType<Player>();
        if (m_Collider2D == null)
        {
            m_Collider2D = GetComponent<Collider2D>();
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * (m_Speed * Time.deltaTime));
        Respawn();
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
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            if(m_Player != null)
            {
                m_Player.Damage();
            }
            DestroyEnemy(other);
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
}
