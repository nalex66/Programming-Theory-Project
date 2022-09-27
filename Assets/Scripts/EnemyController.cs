using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 3;
    public float health = 10;
    private float angleOffset;
    private Vector3 direction;
    
    private GameManager gameManager;
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // turn towards player position
        direction = (gameManager.EndZone - transform.position).normalized;
        angleOffset = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        transform.Rotate(Vector3.up * angleOffset);

    }

    // Update is called once per frame
    void Update()
    {
        // walk forward, activate animation for walking??
        if (health > 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (health < 0)
        {
            //fall over and die, start coroutine to destroy a few seconds later
        }

        if (transform.position.z < gameManager.EndZone.z)
        {
            gameManager.GameOver();
        }
    }
}
