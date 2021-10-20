using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRoomsForest;
    public GameObject[] topRoomsForest;
    public GameObject[] leftRoomsForest;
    public GameObject[] rightRoomsForest;
    public List<GameObject> rooms;

    public float waitTime;
    private float wait;
    private bool spawnedBoss = false;
    public GameObject boss;

    private void Start()
    {
        wait = waitTime;
    }

    private void Update()
    {
        if(wait <= 0 && spawnedBoss == false)
        {

            /*  
             *  
             *  if the map that gets generated contains 5 rooms or less, the whole array of rooms gets destroyed and remade.
             *  probably not the best thing to do for performance, and technically if you get really unlucky 
             *  you can load for infinite time, because there is no guarantee that a valid room gets generated
             *  However, in practice i've literally never had it take more than 1 second to generate a full dungeon.
             *  
             *  may need to revisit this generation system if rooms have a ton of exclusive rules that prevent valid generation.
             *   
            */

            if (rooms.Count <= 5)
            {
                foreach(GameObject room in rooms)
                {
                    Destroy(room);
                }
            }
            else
            {
                Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                spawnedBoss = true;
            }

            
        }
        else
        {
            wait -= Time.deltaTime;
        }
    }



}
