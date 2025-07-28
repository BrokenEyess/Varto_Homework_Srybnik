using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] protected Transform _target;
    [SerializeField] protected Collider2D _collider;
    [SerializeField] private float _YOffset;

    protected bool _isInitiated;

    public void Init(Transform target)
    {
        _target = target;
        _isInitiated = true;
    }

    private void Update()
    {
        if (_isInitiated)
        {
            OnUpdatePlatform();
        }
    }

    protected virtual void OnUpdatePlatform()
    {
        if (_target.transform.position.y > transform.position.y + _YOffset)
        {
            _collider.enabled = true;
        }
        else _collider.enabled = false;
    }
}
