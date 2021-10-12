using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnWwise : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event m_EventToPlay;

    private void OnEnable()
    {
        m_EventToPlay.Post(gameObject);
    }
}
