using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileStrike : MonoBehaviour
{   
    // spawn missile headed to ground, ground target indicator, strike fx, collider with trigger, particle effects
    public GameObject missile;
    public GameObject strikeIndicator;
    public ParticleSystem explosion;
    public ParticleSystem damageType;

    private GameManager gameManager;
    private PlayerController playerController;
    private Vector3 strikePosition;
    private float missileSpeed = 80;
    private float strikeDuration = 10;
    private float counter;
    private int damageIndex;
    private Color damageColor;
    private ParticleSystem weaponType;
 

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player Controller").GetComponent<PlayerController>();
        strikePosition = gameManager.targetPosition.transform.position;
        damageIndex = playerController.damageIndex;
        weaponType = playerController.weaponType[damageIndex];
        damageType = playerController.damageType[damageIndex];
        damageColor = playerController.damageColor[damageIndex].color;
        

        strikeIndicator.GetComponent<Renderer>().material.SetColor("_Color", damageColor);
        GetComponent<SphereCollider>().enabled = false; // will turn on collider when missile strikes
        counter = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        if (missile != null)
        {
            missile.transform.Translate(Vector3.forward * missileSpeed * Time.deltaTime);
            if (missile.transform.position.y < 0)
            {
                Destroy(missile);
                Instantiate(explosion, strikePosition, explosion.transform.rotation);
                Instantiate(weaponType, strikePosition, weaponType.transform.rotation);
                GetComponent<SphereCollider>().enabled = true;
            }
        }

        counter += Time.deltaTime;
        if (counter > strikeDuration)
        {
            Destroy(gameObject); // destroy the whole missile strike after duration
        }
    }
}
