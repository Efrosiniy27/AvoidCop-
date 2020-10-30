using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Text _textCurrentLevel;
    public GameObject _buttonMusicOn;
    public AudioMixer _mixer;
 
    void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                _buttonMusicOn.SetActive(false);
                _mixer.SetFloat("Vol", -80.0f);
            }
        }
        _textCurrentLevel.text = "Level: " + PlayerPrefs.GetInt("CurrentLevel");
        Time.timeScale = 1.0f;
    }
    public void SoundEnable(bool value)
    {
        if (value)
        {
            _mixer.SetFloat("Vol", 0.0f);
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            _mixer.SetFloat("Vol", -80.0f);
            PlayerPrefs.SetInt("Sound", 0);
        }
            
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene("Level_" + PlayerPrefs.GetInt("CurrentLevel"));
    }
    public void EnterInstagram()
    {
        Application.OpenURL("http://instagram.com/sofil_2727");
    }
    public void ClearStat()
    {
        PlayerPrefs.SetInt("CurrentLevel", 1);
        _textCurrentLevel.text = "Level: " + PlayerPrefs.GetInt("CurrentLevel");
    }
}
