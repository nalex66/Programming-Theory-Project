using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera cam;
    private Vector3 worldPos;
    private Vector2 mousePos;
    private Vector2 screenOrigin;
    private Vector2 screenLimit;
    private Ray targetRay;
    private Plane targetPlane = new Plane(Vector3.up, 0);

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
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

    public void ChangeDamageType()
    {
        // cycle to next damage type -- separate cooldowns on types or single firing delay?
    }

    public void CheckFiringReadiness()
    {
        // need coroutine to count down fire delay
    }
}
