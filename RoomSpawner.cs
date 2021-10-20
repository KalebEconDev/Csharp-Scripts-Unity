using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDir;
    private RoomTemplates templates;
    private int rand;
    // 2 --> need bottom door
    // 1 --> need top door
    // 4 --> need left door
    // 3 --> need right door

    public bool spawned = false;


    private void Start()
    {
        Destroy(gameObject, 3f);

        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        Invoke("Spawn",.05f);
    }

    private void Spawn()
    {

        if(spawned == false) 
        {

            if (openingDir == 2)
            {
                //need a bottom door room
                rand = Random.Range(0, templates.bottomRoomsForest.Length);
                Instantiate(templates.bottomRoomsForest[rand], transform.position, Quaternion.identity);


            }
            else if (openingDir == 1)
            {
                //need a top door
                rand = Random.Range(0, templates.topRoomsForest.Length);
                Instantiate(templates.topRoomsForest[rand], transform.position, Quaternion.identity);

            }
            else if (openingDir == 4)
            {
                //need a left door

                rand = Random.Range(0, templates.leftRoomsForest.Length);
                Instantiate(templates.leftRoomsForest[rand], transform.position, Quaternion.identity);
            }
            else if (openingDir == 3)
            {
                //need a right door

                rand = Random.Range(0, templates.rightRoomsForest.Length);
                Instantiate(templates.rightRoomsForest[rand], transform.position, Quaternion.identity);


            }

            spawned = true;
        }


        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spawn Point") && other.GetComponent<RoomSpawner>().spawned == true)
        {
            Destroy(gameObject);
        }
    }

}
