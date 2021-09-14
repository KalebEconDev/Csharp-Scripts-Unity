using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChangeMin;
    public Vector2 cameraChangeMax;
    public Vector3 playerChange;
    private CameraMovement cam;
    public bool needTitleCard;
    public string placeName;
    public GameObject text;
    public Text placeText;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        /* On Collision: if we are colliding with the player then we need a room change.
         * we grab the change in units of the maxmimum bounds of the camera on both axises
         * We also grab the change in character position
         * We need to move the character "forward" relative to them entering the room.
         * This ensures they do not collide with the corresponding trigger back into the room they just left.
         * 
         * Example for use: if we create a room attached to the right of our current room of the same size, and 5 units to the right
         * Then we should pass in +5 for the camChangeMaxX and 0 for the change in Y because the rooms were identical size.
         * 
         * We should also pass +5 or so into the playerMove paramater in the X position to ensure our player dodges the second trigger.
         * Every trigger comes in a pair, unless the player enters a room that they are unable to leave.
         * 
         */
        if (other.CompareTag("Player"))
        {
            cam.minPosition += cameraChangeMin;
            cam.maxPosition += cameraChangeMax;
            other.transform.position += playerChange;


            /*
             * This couroutine displays the name of the zone if it has one when the player triggers the room change
             */

            if (needTitleCard)
            {
                StartCoroutine(placeNameCo());
            }

        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);


    }
}
