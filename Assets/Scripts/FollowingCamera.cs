using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform _player;
    public float _offsetY=0.0f;
    private Transform _transform;
    void Start()
    {
        _transform = GetComponent<Transform>();        
    }
    void Update()
    {
        _transform.position = Vector3.Lerp(_transform.position, _player.position+new Vector3(0,_offsetY,-10),Time.deltaTime);
    }
}
