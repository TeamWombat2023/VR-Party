using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitScore
{
    public int currentScore;

    //constructor
    public UnitScore()
    {
        currentScore = 0;
    }

    //increase score
    public void IncreaseScore(int score)
    {
        currentScore += score;
    }

    //decrease score
    public void DecreaseScore(int score)
    {
        currentScore -= score;
    }

}

