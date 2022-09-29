using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public List<GameObject> enemies;
    public GameObject missileStrike;
    public GameObject targetPosition;

    public Vector3 endZone { get; private set; }    // ENCAPSULATION - variable is accessed from other classes but not modified

    private bool isGameActive;
    private PlayerController playerController;
    private float spawnRate = 1f;
    private float xRange = 48;
    private float zSpawnPos = 40;
    


    // Start is called before the first frame update
    void Start()
    {
        endZone = new Vector3(0, 0, -25);
        targetPosition = GameObject.Find("Player Controller");
        playerController = GameObject.Find("Player Controller").GetComponent<PlayerController>();
        isGameActive = true;
        gameOverScreen.gameObject.SetActive(false);
        targetPosition.gameObject.SetActive(true);
        InvokeRepeating("SpawnEnemy", 0, spawnRate);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            // update target position based on mouse position
            targetPosition.transform.position = playerController.UpdateTarget(); 

            // check mouse button input
            if (Input.GetMouseButtonDown(0) && playerController.isMissileReady)
            {
                // fire missile
                Instantiate(missileStrike, targetPosition.transform.position, targetPosition.transform.rotation);
                playerController.ReadyNextMissle();
            }

            if (Input.GetMouseButtonDown(1))
            {
                // change weapon type on right-click
                playerController.CycleDamageType();     // ABSTRACTION - calculations are removed to a separate method
            }
            
        }
               
    }

    public void GameOver() // game over will be called from emeny controller if they reach the end zone
    {
        gameOverScreen.gameObject.SetActive(true);
        targetPosition.gameObject.SetActive(false);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        // just restart directly rather than going back to menu - change if difficulty setting added to main menu
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), 0.2f, zSpawnPos);
    }

    void SpawnEnemy()
    {
        if (isGameActive)
        {
            int index = Random.Range(0, enemies.Count);
            Instantiate(enemies[index], RandomSpawnPos(), enemies[index].transform.rotation);
        }

    }    

}
