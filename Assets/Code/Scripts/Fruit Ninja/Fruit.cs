using System;
using UnityEngine;

public class Fruit : MonoBehaviour {
    public GameObject fruitSlicedPrefab;
    public GameObject fruitWholePrefab;

    private Rigidbody _fruitRigidbody;
    private Collider _fruitCollider;
    private ParticleSystem _juiceEffect;
    private bool _isSliced;
    private float _instantiateTime;

    private void Awake() {
        _fruitRigidbody = GetComponent<Rigidbody>();
        _fruitCollider = GetComponent<Collider>();
        _juiceEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Start() {
        _instantiateTime = Time.time;
    }

    public void Slice(Vector3 direction, Vector3 position, float force) {
        _isSliced = true;
        _fruitCollider.enabled = false;
        fruitWholePrefab.SetActive(false);

        fruitSlicedPrefab.SetActive(true);
        _juiceEffect.Play();

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fruitSlicedPrefab.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        var slices = fruitSlicedPrefab.GetComponentsInChildren<Rigidbody>();

        foreach (var slice in slices) {
            slice.velocity = _fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (!_isSliced && Time.time - _instantiateTime > 0.2f && collision.gameObject.CompareTag("Floor"))
            Destroy(gameObject);
    }
}