using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Police : MonoBehaviour
{
    public List<Transform> _listPositionRoute;
    public float _speed = 1.0f;

    private Transform _transform;
    private Animator _animator;
    private Vector3 _oldPosition;
    private Vector3[] _arrayVec;
    private float LENGTH_ROUTE;
    private Sequence seq;
    public float _lengthRoute
    {
        get
        {
            if (LENGTH_ROUTE == 0)
            {
                float sum = 0;
                for (int i = 0; i < _listPositionRoute.Count - 1; i++)
                {
                    sum += Vector3.Distance(_arrayVec[0], _arrayVec[1]);
                }
                sum += Vector3.Distance(_transform.position, _arrayVec[0]);
                LENGTH_ROUTE = sum;
            }
            return LENGTH_ROUTE;
        }
    }
    void Start()
    {
        seq = DOTween.Sequence();
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        _oldPosition = _transform.position;
        _arrayVec = _listPositionRoute.Select((a) => a.position).ToArray();
        seq.Append(_transform.DOPath(_arrayVec,_lengthRoute/_speed).SetEase(Ease.Linear)).SetLoops(-1, LoopType.Yoyo);
    }
    private void LateUpdate()
    {
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        Vector3 dir = _transform.position - _oldPosition;
        if (dir == Vector3.zero)
        {
            _animator.Play("Idle");
        }
        else
        {
            if (dir.y > dir.x)
            {
                if (dir.y > -dir.x)
                {
                    _animator.Play("WakeUp");
                }
                else
                {
                    _animator.Play("WakeLeft");
                }
            }
            else
            {
                if (dir.y > -dir.x)
                {
                    _animator.Play("WakeRight");
                }
                else
                {
                    _animator.Play("WakeDown");
                }
            }
        }
        _oldPosition = _transform.position;
    }
    public void Wait(float time)
    {
        StartCoroutine(StartWait(time));
    }
    IEnumerator StartWait(float time)
    {
        seq.Pause();
        yield return new WaitForSeconds(time);
        seq.Play();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        var tempStop = other.gameObject.GetComponent<StopPosition>();
        if (tempStop != null)
        {
            Wait(tempStop._stopTime);
        }
    }

}
