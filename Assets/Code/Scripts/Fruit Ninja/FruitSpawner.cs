using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour {
    public GameObject[] fruitPrefabs;
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 0.5f;
    public float minForce = 6;
    public float maxForce = 9;
    public float minAngle = -5f;
    public float maxAngle = 5f;
    public float maxLifetime = 5f;
    public GameObject playerCamera;

    private Vector3 _spawnArea;

    private void Update() {
        _spawnArea = Vector3.Lerp(_spawnArea, playerCamera.transform.position + playerCamera.transform.forward * 1.5f,
            0.05f);
        _spawnArea.y = _spawnArea.y < 0.5f ? 0.5f : _spawnArea.y;
    }

    private void OnEnable() {
        StartCoroutine(SpawnFruit());
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private IEnumerator SpawnFruit() {
        yield return new WaitForSeconds(5f);
        while (enabled) {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            var randomFruit = Random.Range(0, fruitPrefabs.Length);
            var position = new Vector3(_spawnArea.x + Random.Range(-2f, 2f), _spawnArea.y, Random.Range(-0.5f, 0.35f));
            var rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            var fruit = Instantiate(fruitPrefabs[randomFruit], _spawnArea, rotation);
            Destroy(fruit, maxLifetime);

            var rb = fruit.GetComponent<Rigidbody>();
            rb.AddForce(fruit.transform.up * Random.Range(minForce, maxForce), ForceMode.Impulse);
        }
    }
}