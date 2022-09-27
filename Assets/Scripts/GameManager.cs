using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject targetPosition;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player Controller").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition.transform.position = playerController.UpdateTarget(); // update target position
        
        // check player input
        // call missile strike if conditions met
        // spawn manager - here or separate?
    }
}
