using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> backgroundMusics = new List<AudioClip>();
    
    void Awake() 
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            SetBackgroundMusic();
            DontDestroyOnLoad(gameObject);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void SetBackgroundMusic()
    {
        int numOfBgMusic = backgroundMusics.Count;
        int index = (int)UnityEngine.Random.Range(0, numOfBgMusic);
        GetComponent<AudioSource>().clip = backgroundMusics[index];
    }
}
