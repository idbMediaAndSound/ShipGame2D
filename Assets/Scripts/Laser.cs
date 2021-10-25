using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
   
    [Range(1f, 10f)]
    [SerializeField] private float m_Speed = 5f;
    [SerializeField] private TypeOfProjectiles m_TypeOfLaser;
    void Update()
    {
        ProjectileMovement();
        DestroyProjectile();
    }

    private void ProjectileMovement()
    {
        if(m_TypeOfLaser == TypeOfProjectiles.PlayerLaser)
        {
            transform.Translate(Vector3.up * m_Speed * Time.deltaTime);
        }else
        {
            transform.Translate(Vector3.down * m_Speed * Time.deltaTime);
        }

    }
    private void DestroyProjectile()
    {

        if ((transform.position.y > 7 && m_TypeOfLaser == TypeOfProjectiles.PlayerLaser)||
            (transform.position.y < -7 && m_TypeOfLaser == TypeOfProjectiles.EnemyLaser))
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }

        /*if (transform.position.y < -7 && m_TypeOfLaser == TypeOfProjectiles.EnemyLaser)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }*/
    }
}

public enum TypeOfProjectiles
{
    PlayerLaser,
    EnemyLaser
}
