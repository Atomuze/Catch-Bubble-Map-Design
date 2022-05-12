using System.Collections;
using UnityEngine;

public class musicHandler : MonoBehaviour
{
    AudioSource AudioSource;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        StartCoroutine(LoadSongCoroutine());
    }

    [System.Obsolete]
    IEnumerator LoadSongCoroutine()
    {

        AudioSource = GetComponent<AudioSource>();
        WWW www = new WWW(Application.dataPath + "/data/music.mp3");
        yield return www;

        AudioClip audioClip = www.GetAudioClip();
        AudioSource.clip = audioClip;


    }
}
