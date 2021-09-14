using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelChangeTrigger : MonoBehaviour
{
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public string placeName = "start";
    public CameraMovement cam;


    private void OnTriggerEnter2D(Collider2D other)
    {

        //if the thing colliding is the player, we need a cam change and we pass the name of the location in as an argument.
        if (other.name == "Player")
        {
            camChange(placeName);
            cam.maxPosition = maxPosition;
            cam.minPosition = minPosition;
        }
    }



    // Start is called before the first frame update
    void Awake()
    {
        camChange(placeName);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void camChange(string locName)
    {  
            switch (locName)
            {
                case "start":
                    maxPosition = new Vector2(54.1f, 109.6f);
                    minPosition = new Vector2(-56.3f, -1.53f);
                    break;

                default:
                    Debug.Log("Tried and failed to load a camera");
                    break;

            } 
    }
}
