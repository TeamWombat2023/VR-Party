using UnityEngine;


public class PlayerBehaviour : MonoBehaviour {
    private void Start() {
    }

    private void Update() {
    }

    private void PlayerTakeDmg(int damage) {
        LabyrinthGameManager.labyrinthGameManager._playerHealth.TakeDamage(damage);
        Debug.Log(LabyrinthGameManager.labyrinthGameManager._playerHealth.currentHealth);
    }

    private void PlayerHeal(int healing) {
        LabyrinthGameManager.labyrinthGameManager._playerHealth.Heal(healing);
        Debug.Log(LabyrinthGameManager.labyrinthGameManager._playerHealth.currentHealth);
    }

    private void IncreaseScore(int score) {
        LabyrinthGameManager.labyrinthGameManager._playerScore.IncreaseScore(score);
        Debug.Log(LabyrinthGameManager.labyrinthGameManager._playerScore.currentScore);
    }

    private void DecreaseScore(int score) {
        LabyrinthGameManager.labyrinthGameManager._playerScore.DecreaseScore(score);
        Debug.Log(LabyrinthGameManager.labyrinthGameManager._playerScore.currentScore);
    }

    //actions on collision
    private void OnTriggerEnter(Collider other) {
        //if heal object
        if (other.gameObject.CompareTag("HealPickup"))
            PlayerHeal(10);

        //if damage object
        else if (other.gameObject.CompareTag("DamagePickup"))
            PlayerTakeDmg(10);

        //if score object
        else if (other.gameObject.CompareTag("ScorePickup"))
            IncreaseScore(10);

        //if trap object
        else if (other.gameObject.CompareTag("TrapPickup")) DecreaseScore(10);

        //destroy after pickup
        other.gameObject.SetActive(false);
    }
}