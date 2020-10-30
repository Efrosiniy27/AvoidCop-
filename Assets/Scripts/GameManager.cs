using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager S;
    public GameObject _player;
    public GameObject _nextHome;
    [Header("Interface")]
    public Image _progressBar;
    public GameObject _panelPauseMenu;
    public GameObject _panelWin;
    public GameObject _panelLose;
    public Text _textCurrentLevel;
    public Text _textNextLevel;
    private float _startDist;
    private int _numberLevel;

    static int _countLevel = 8;
    private void Awake()
    {
        S = this;
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
        _startDist = Vector3.Distance(_player.transform.position, _nextHome.transform.position)-1;
        _numberLevel = PlayerPrefs.GetInt("CurrentLevel");
        _textCurrentLevel.text = _numberLevel.ToString();
        _textNextLevel.text = (_numberLevel + 1).ToString();
    }
    private void FixedUpdate()
    {
        float _currentDist= Vector3.Distance(_player.transform.position, _nextHome.transform.position)-1;
        _progressBar.fillAmount = 1-(_currentDist / _startDist);
    }
    public void LoadNextLevel()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") == 8)
        {
            SceneManager.LoadScene("FinishScene");
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", _numberLevel + 1);
            SceneManager.LoadScene("Level_" + PlayerPrefs.GetInt("CurrentLevel"));
        }
       
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("Level_" + _numberLevel);
    }
    public void EnterMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void StopGameTime()
    {
        Time.timeScale = 0.0f;
    }
    public void PlayGameTime()
    {
        Time.timeScale = 1.0f;
    }
}
