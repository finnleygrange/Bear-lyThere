using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLooseScript : MonoBehaviour
{
    public GameObject enemyBase;
    public GameObject playerBase;

    private float enemyBaseHealth;
    private float playerBaseHealth;

    public GameObject UI;
    public GameObject winScreen;
    public GameObject looseScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyBaseHealth = enemyBase.GetComponent<BaseHealthScript>().health;
        playerBaseHealth = playerBase.GetComponent<BaseHealthScript>().health;

        if (enemyBaseHealth <= 0)
        {
            UI.SetActive(false);
            winScreen.SetActive(true);
            Invoke("loadMenu", 5);
        }

        if (playerBaseHealth <= 0)
        {
            UI.SetActive(false);
            looseScreen.SetActive(true);
            Invoke("loadMenu", 5);
        }
    }
    void loadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
