using UnityEngine;

public class Sword : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Fruit")) {
            var hitNormal = collision.contacts[0].normal;
            var angle = Vector3.Angle(Vector3.forward, hitNormal);
            if (angle < 45f || angle > 135f)
                Debug.Log("Hit the side");
            else
                Debug.Log("Hit the top");

            var fruit = collision.gameObject.GetComponent<Fruit>();
            var direction = (collision.transform.position - transform.position).normalized;
            var position = collision.transform.position;
            const float force = 2f;
            fruit.Slice(direction, position, force);
        }
    }
}