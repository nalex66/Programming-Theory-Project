using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject targetIndicator;
    public bool isMissileReady;
    public List<ParticleSystem> weaponType;
    public List<ParticleSystem> damageType;
    public List<Material> damageColor;
    public int damageIndex;

    private Camera cam;
    private Vector3 worldPos;
    private Vector2 mousePos;
    private Vector2 screenOrigin;
    private Vector2 screenLimit;
    private Ray targetRay;
    private Plane targetPlane = new Plane(Vector3.up, 0);
    private float strikeDelay = 3.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        damageIndex = 0;
        isMissileReady = true;
        targetIndicator.GetComponent<Renderer>().material.SetColor("_Color", damageColor[damageIndex].color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 UpdateTarget() // return a vector3 world position for target, based on mouse position
    {
        
        // establish screen bounds with 5% margin from top and side edges, 15% margin at bottom
        screenLimit = new Vector2(Screen.width*0.95f, Screen.height*0.95f);
        screenOrigin = new Vector2(Screen.width*0.05f, Screen.height*0.15f);
        
        // get mouse position on screen
        mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // limit mouse position to horizontal screen bounds
        if (mousePos.x > screenLimit.x)
        {
            mousePos = new Vector2(screenLimit.x, mousePos.y);
        }
        else if (mousePos.x < screenOrigin.x)
        {
            mousePos = new Vector2(screenOrigin.x, mousePos.y);
        }

        // limit mouse position to vertical screen bounds
        if (mousePos.y > screenLimit.y)
        {
            mousePos = new Vector2(mousePos.x, screenLimit.y);
        }
        else if (mousePos.y < screenOrigin.y)
        {
            mousePos = new Vector2(mousePos.x, screenOrigin.y);
        }

        // get world position from mouse position
        targetRay = cam.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 0));
        float point = 0;
        if (targetPlane.Raycast(targetRay, out point)) // check that ray intersects plane
        {
            worldPos = targetRay.GetPoint(point);
        }
        else
        {
            worldPos = Vector3.zero; // default position if intersection fails
        }

        return worldPos;
    }

    public void CycleDamageType()
    {
        // cycle to next damage type
        damageIndex++;
        if (damageIndex >= weaponType.Count)
        {
            damageIndex = 0;
        }
        targetIndicator.GetComponent<Renderer>().material.SetColor("_Color", damageColor[damageIndex].color);
    }

    public void ReadyNextMissle()
    {
        // count down fire delay, change target indicator, isMissileReady = true;
        isMissileReady = false;
        targetIndicator.SetActive(false);
        StartCoroutine(StrikeDelay());
    }

    IEnumerator StrikeDelay()
    {
        yield return new WaitForSeconds(strikeDelay);
        targetIndicator.SetActive(true);
        isMissileReady = true;
    }
}
