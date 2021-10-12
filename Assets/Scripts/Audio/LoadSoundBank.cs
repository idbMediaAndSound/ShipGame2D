using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSoundBank : MonoBehaviour
{
    [SerializeField] AK.Wwise.Bank m_Bank;

    private void OnEnable()
    {
        m_Bank.Load();
    }

    private void OnDisable()
    {
        m_Bank.Unload();
    }
}
