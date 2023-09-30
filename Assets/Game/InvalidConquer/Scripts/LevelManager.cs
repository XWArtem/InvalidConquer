using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelStates
{ 
    PlayState,
    Finish
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FinishLevel()
    {
        Time.timeScale = 0f;
        //popup
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        //popup
    }
}
