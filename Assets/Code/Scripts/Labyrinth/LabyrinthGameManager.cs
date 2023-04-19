using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthGameManager : MonoBehaviour {
    public static LabyrinthGameManager labyrinthGameManager { get; private set; }

    public UnitHealth _playerHealth = new();
    public UnitScore _playerScore = new();

    private void Awake() {
    }

    // Update is called once per frame
    private void Update() {
    }
}