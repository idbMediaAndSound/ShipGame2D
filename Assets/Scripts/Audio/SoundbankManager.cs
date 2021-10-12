using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundbankManager : MonoBehaviour
{
    [SerializeField] static List<GameObject> Soundbanks = new List<GameObject>();

    public Dictionary<GameObject, string> SoundbanksAvailable = new Dictionary<GameObject, string>();


    public static void LoadSoundBank()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        /*switch(SceneManager.GetActiveScene().name)
        {
            case "MainMenu":
                Soundbanks[1].SetActive(true);
                break;
            case "Level1":
                Soundbanks[1].SetActive(false);
                Soundbanks[2].SetActive(true);
                break;
        }*/
    }
}
