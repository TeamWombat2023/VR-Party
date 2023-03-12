using UnityEngine;

public class Sword : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Fruit")) {
            var hitNormal = collision.contacts[0].normal;
            var angle = Vector3.Angle(hitNormal, Vector3.up);
            Debug.Log("Angle: " + angle);
            if (angle < 45f || angle > 135f) {
                Debug.Log("Hit the side");
                var fruit = collision.gameObject.GetComponent<Fruit>();
                var direction = (collision.transform.position - transform.position).normalized;
                var position = collision.transform.position;
                const float force = 2f;
                fruit.Slice(direction, position, force);
            }
            else {
                Debug.Log("Hit the top");
            }
        }
    }
}