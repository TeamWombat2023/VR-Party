using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour {
    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;
    [Range(0f, 1f)] public float bombChance = 0.05f;
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 0.5f;
    public float minForce = 6;
    public float maxForce = 9;
    public float minAngle = -5f;
    public float maxAngle = 5f;
    public float maxLifetime = 5f;

    private Collider _spawnArea;

    private void Awake() {
        _spawnArea = GetComponent<Collider>();
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
            var randomFruit = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            if (Random.value < bombChance) randomFruit = bombPrefab;
            var position = new Vector3 {
                x = Random.Range(_spawnArea.bounds.min.x, _spawnArea.bounds.max.x),
                y = Random.Range(_spawnArea.bounds.min.y, _spawnArea.bounds.max.y),
                z = Random.Range(_spawnArea.bounds.min.z, _spawnArea.bounds.max.z)
            };

            var rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            var fruit = Instantiate(randomFruit, position, rotation);
            Destroy(fruit, maxLifetime);

            var rb = fruit.GetComponent<Rigidbody>();
            rb.AddForce(fruit.transform.up * Random.Range(minForce, maxForce), ForceMode.Impulse);
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}