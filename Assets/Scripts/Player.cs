using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _speed = 1.0f;
    [Header("Sounds")]
    public AudioClip _soundWin;
    public AudioClip _soundLose;
    private Transform _transform;
    private Animator _animator;
    private AudioSource _audioSource;
    private bool _isHide = false;    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        _audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;
    }
    Coroutine CorWalk;
    public void Walk()
    {
        _animator.Play("WakeUp");
        if (CorWalk!=null) StopCoroutine(CorWalk);
        CorWalk = StartCoroutine(StartWalk());
    }
    public void Idle()
    {
        _animator.Play("Idle");
        StopCoroutine(CorWalk);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Walk();
        if (Input.GetKeyUp(KeyCode.Space)) Idle();
    }
    IEnumerator StartWalk()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            _transform.position += Vector3.up * _speed*0.01f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Police")&&(_isHide==false))
        {
            Time.timeScale = 0.0f;
            _audioSource.PlayOneShot(_soundLose);
            GameManager.S._panelLose.SetActive(true);
        }
        if (collision.gameObject.tag == "NextHome")
        {
            Time.timeScale = 0.0f;
            _audioSource.PlayOneShot(_soundWin);
            GameManager.S._panelWin.SetActive(true);
        }
        if (collision.gameObject.tag == "Shelter")
        {
            _isHide = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag=="Shelter")
        _isHide = false;
    }
}
