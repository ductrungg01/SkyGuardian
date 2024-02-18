using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;
    private MusicPlayer() { }
    public static MusicPlayer Instance
    {
        get { return instance; }
    }
    [SerializeField] private List<AudioClip> backgroundMusics = new List<AudioClip>();
    
    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
            return;
        }

        SetBackgroundMusic();
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void SetBackgroundMusic()
    {
        int numOfBgMusic = backgroundMusics.Count;
        int index = (int)UnityEngine.Random.Range(0, numOfBgMusic);
        GetComponent<AudioSource>().clip = backgroundMusics[index];
    }
}
