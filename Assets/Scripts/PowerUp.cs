using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : MonoBehaviour
{
    [SerializeField] protected float m_Speed = 3f;
    [SerializeField] protected float m_YLimit = 6.42f;
    [SerializeField] private Player m_Player;
    
    [SerializeField] private CollectablePowerUps m_TypeOfPoswerup = new CollectablePowerUps();
    protected float m_RandomXPos;
    //bool isActive = false;


    void Start()
    {
        m_Player = FindObjectOfType<Player>();    
    }
    void Update()
    {   
        transform.Translate(Vector3.down * m_Speed * Time.deltaTime);
        DestroyPowerUp();
    }
    public virtual void DestroyPowerUp()
    {
        if (transform.position.y < -m_YLimit)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (m_Player != null)
            {
                switch(m_TypeOfPoswerup)
                {
                    case CollectablePowerUps.TripleShot:
                        m_Player.ActivateTripleShot();
                        break;

                    case CollectablePowerUps.Speed:
                        m_Player.ActivateSpeed();
                        break;
                    case CollectablePowerUps.Shield:
                        m_Player.ActivateShield();
                        break;
                }
                
            }

            Destroy(this.gameObject);
            
        }
    }
}
