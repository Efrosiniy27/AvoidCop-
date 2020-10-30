using UnityEngine;
using System.Collections;

public class MainMusic : MonoBehaviour
{
    public static bool muze;
    private AudioSource _audio;
    void Start()
    {
        
        if (muze == false)
        {
            _audio.Play();
            muze = true;
        }
        else { _audio.Stop(); }
    }
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _audio.volume=0.3f;
        DontDestroyOnLoad(gameObject);
    }
}