using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundbankManager : MonoBehaviour
{ /*
    [SerializeField] static GameObject MusicSoundbank;
    [SerializeField] static GameObject MenuSoundbank;
    [SerializeField] static GameObject GameplaySoundbank;


    private void Start()
    {
        CheckForReferences();
    }

    public static void LoadSoundBank()
    {
        if (!MusicSoundbank.activeInHierarchy)
        {
            MusicSoundbank.SetActive(true);
        }else
        {
            return;
        }

        switch(SceneManager.GetActiveScene().name.ToLower())
        {
            case "mainmenu":
                Debug.Log("The scene 0 is: " + SceneManager.GetActiveScene().name.ToUpper());
                break;
            case "level1":
                Debug.Log("The scene 1 is: " + SceneManager.GetActiveScene().name.ToUpper());
                break;
        }
    }

    public void CheckForReferences()
    {
        if(MusicSoundbank == null)
        {
            MusicSoundbank = GameObject.Find("Music");
        }
    }*/
}
