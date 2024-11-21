using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveDataPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private SoundMovementManager soundMovementManager;

    void Awake()
    {
        LoadPlayerStats();
    }

    void OnApplicationQuit()
    {
        DeletePlayerStats();
    }

    public void SavePlayerStats() {
        PlayerPrefs.SetFloat("PlayerX", player.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.position.z);

        /* PlayerPrefs.SetInt("isStartedGame", tutorialManager.isStartedGame ? 1 : 0);
        PlayerPrefs.SetInt("introduzioneGaia", tutorialManager.introduzioneGaia ? 1 : 0); */
        PlayerPrefs.SetInt("cassaAlzata", tutorialManager.cassaAlzata ? 1 : 0);

        PlayerPrefs.SetInt("firstOnSabbia", soundMovementManager.firstOnSabbia ? 1 : 0);
        PlayerPrefs.SetInt("firstOnErba", soundMovementManager.firstOnErba ? 1 : 0);
    }

    public void LoadPlayerStats() {
        if(PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ")) {
            player.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
        }
        
        if(PlayerPrefs.HasKey("isStartedGame") && PlayerPrefs.HasKey("introduzioneGaia") && PlayerPrefs.HasKey("cassaAlzata")) {
            /* tutorialManager.isStartedGame = PlayerPrefs.GetInt("isStartedGame") == 1;
            tutorialManager.introduzioneGaia = PlayerPrefs.GetInt("introduzioneGaia") == 1; */
            tutorialManager.cassaAlzata = PlayerPrefs.GetInt("cassaAlzata") == 1;
        }

        if(PlayerPrefs.HasKey("firstOnSabbia") && PlayerPrefs.HasKey("firstOnErba")) {
            soundMovementManager.firstOnSabbia = PlayerPrefs.GetInt("firstOnSabbia") == 1;
            soundMovementManager.firstOnErba = PlayerPrefs.GetInt("firstOnErba") == 1;
        }
    }

    public void DeletePlayerStats() {
        PlayerPrefs.DeleteAll();
    }
}
