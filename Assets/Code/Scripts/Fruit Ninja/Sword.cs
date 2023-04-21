using UnityEngine;

public class Sword : MonoBehaviour {
    private FruitNinjaManager manager;

    private void Awake() {
        manager = FindObjectOfType<FruitNinjaManager>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Fruit")) {
            var hitNormal = collision.contacts[0].normal;
            var angle = Vector3.Angle(hitNormal, Vector3.up);
            if (angle < 45f || angle > 135f) {
                var fruit = collision.gameObject.GetComponent<Fruit>();
                var direction = (collision.transform.position - transform.position).normalized;
                var position = collision.transform.position;
                const float force = 2f;
                fruit.Slice(direction, position, force);
                manager.IncrementScore();
            }
        }
        else if (collision.gameObject.CompareTag("Bomb")) {
            manager.DecreaseScore();
        }
    }
}