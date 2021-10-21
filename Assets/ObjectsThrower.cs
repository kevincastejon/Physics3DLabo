using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsThrower : MonoBehaviour
{
    [SerializeField] private Transform _transformA;
    [SerializeField] private Transform _transformB;
    [SerializeField] private float _speed = 3f;

    private bool _isTriggerA;
    private bool _hasRigidbodyA;
    private bool _isKinematicA;
    private bool _isTriggerB;
    private bool _hasRigidbodyB;
    private bool _isKinematicB;

    private Rigidbody _rigidbodyA;
    private Rigidbody _rigidbodyB;
    private Collider _colliderA;
    private Collider _colliderB;

    private bool _isStarted;

    private void Awake()
    {
        _colliderA = _transformA.GetComponent<Collider>();
        _colliderB = _transformB.GetComponent<Collider>();
    }

    public void ThrowObjects(bool isTriggerA, bool hasRigidbodyA, bool isKinematicA, bool isTriggerB, bool hasRigidbodyB, bool isKinematicB)
    {
        _isStarted = true;

        _isTriggerA = isTriggerA;
        _hasRigidbodyA = hasRigidbodyA;
        _isKinematicA = isKinematicA;
        _isTriggerB = isTriggerB;
        _hasRigidbodyB = hasRigidbodyB;
        _isKinematicB = isKinematicB;

        _colliderA.isTrigger = _isTriggerA;
        _colliderB.isTrigger = _isTriggerB;

        if (_hasRigidbodyA)
        {
            _rigidbodyA = _transformA.gameObject.AddComponent<Rigidbody>();
            _rigidbodyA.isKinematic = _isKinematicA;
            _rigidbodyA.useGravity = false;
            _rigidbodyA.angularDrag = 0f;
            _rigidbodyA.interpolation = RigidbodyInterpolation.Interpolate;
        }
        if (_hasRigidbodyB)
        {
            _rigidbodyB = _transformB.gameObject.AddComponent<Rigidbody>();
            _rigidbodyB.isKinematic = _isKinematicB;
            _rigidbodyB.useGravity = false;
            _rigidbodyB.angularDrag = 0f;
            _rigidbodyB.interpolation = RigidbodyInterpolation.Interpolate;
        }
        PhysicsThrow();
    }

    private void PhysicsThrow()
    {
        if (_hasRigidbodyA && !_isKinematicA)
        {
            _rigidbodyA.AddForce(Vector3.right * _speed, ForceMode.Impulse);
        }
        if (_hasRigidbodyB && !_isKinematicB)
        {
            _rigidbodyB.AddForce(Vector3.left * _speed, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (!_isStarted)
        {
            return;
        }

        if (!_hasRigidbodyA)
        {
            _transformA.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        if (!_hasRigidbodyB)
        {
            _transformB.Translate(Vector3.left * _speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (!_isStarted)
        {
            return;
        }

        if (_hasRigidbodyA && _isKinematicA)
        {
            _rigidbodyA.MovePosition(_rigidbodyA.position + Vector3.right * _speed * Time.fixedDeltaTime);
        }
        if (_hasRigidbodyB && _isKinematicB)
        {
            _rigidbodyB.MovePosition(_rigidbodyB.position + Vector3.left * _speed * Time.fixedDeltaTime);
        }
    }

    public void ResetObjects()
    {
        _isStarted = false;
        DestroyImmediate(_rigidbodyA);
        DestroyImmediate(_rigidbodyB);
        _transformA.position = new Vector3(-4f, 0f, 0f);
        _transformA.rotation = Quaternion.identity;
        _transformB.position = new Vector3(4f, 0f, 0f);
        _transformB.rotation = Quaternion.identity;
    }
}
