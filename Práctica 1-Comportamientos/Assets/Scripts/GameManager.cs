using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject losePanel;

    public bool endGame = false;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void SetActiveLosePanel()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
