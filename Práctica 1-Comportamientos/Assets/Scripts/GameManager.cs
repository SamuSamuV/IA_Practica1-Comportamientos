using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    public void SetActiveLosePanel()
    {
        losePanel.SetActive(true);
    }
}
