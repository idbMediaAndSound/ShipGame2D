using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
   
    [Range(1f, 10f)]
    [SerializeField] private float m_Speed = 5f;

    void Update()
    {
        ProjectileMovement();
        DestroyProjectile();
    }
    private void ProjectileMovement()
    {
        transform.Translate(Vector3.up * m_Speed * Time.deltaTime);
        //m_Rb.AddForce(Vector3.up, ForceMode.Impulse);
    }
    private void DestroyProjectile()
    {

        if (transform.position.y > 7)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
