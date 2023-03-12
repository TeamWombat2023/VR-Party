using UnityEngine;

public class Sword : MonoBehaviour {
    private ScoreManager _scoreManager;

    private void Awake() {
        _scoreManager = FindObjectOfType<ScoreManager>();
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
                _scoreManager.IncrementScore();
            }
        }
        else if (collision.gameObject.CompareTag("Bomb")) {
            _scoreManager.GameOver();
        }
    }
}