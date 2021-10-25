using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySOundOnEnable : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event m_Thruster;


    private void OnEnable()
    {
        m_Thruster.Post(gameObject);
    }


    private void OnDisable()
    {
        m_Thruster.Stop(gameObject);
    }
}
