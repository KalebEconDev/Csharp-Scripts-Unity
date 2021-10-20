using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private List<GameObject> enemyList;
    private List<GameObject> pickUpList;
    private List<GameObject> itemList;
    public GameObject doors;
    private Bounds bounds;

    public List<GameObject> enemyPool;
    private bool needEnemySpawn;
    private bool enemiesLoaded = false;
    private Camera cam;
    public List<Door> doorComponents;
    private bool needCheckDead;


    public int maxEnemySpawn;






    private void Awake()
    {
        //int enemiesSpawned = Random.Range(0, maxEnemySpawn);
        bounds = GetComponent<BoxCollider2D>().bounds;
        needEnemySpawn = true;

        

        //doors = this.gameObject.transform.parent.GetChild

        //only creates enemies if the room calls for it.
        //otherwise the list will remain empty.

        if (maxEnemySpawn > 0) {  } else
        {
            needEnemySpawn = false;
        }
        


    }
    private void Start()
    {

        for (int i = 0; i < doors.transform.childCount; i++)
        {
            GameObject Go = doors.transform.GetChild(i).gameObject;
            doorComponents.Add(Go.GetComponent<Door>());
        }

        cam = Camera.main;
        enemyList = enemyPool;
        needCheckDead = true;
    }


    private void Update()
    {


        if (needCheckDead) 
        {
            StartCoroutine("CheckForDeadEnemiesCo");
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //Updates camera bounds
        //also loads and unloads enemies if needed


        if (other.CompareTag("Player") && enemiesLoaded == false && other.tag != null && maxEnemySpawn > 0)
        {
            
            cam.GetComponent<CameraMovement>().maxPosition.x = bounds.center.x;
            cam.GetComponent<CameraMovement>().maxPosition.y = bounds.center.y;
            cam.GetComponent<CameraMovement>().minPosition.x = bounds.center.x;
            cam.GetComponent<CameraMovement>().minPosition.y = bounds.center.y;
            //Debug.Log("New bounds are: " + bounds);

        }
        else if (other.CompareTag("Player"))
        {
            cam.GetComponent<CameraMovement>().maxPosition.x = bounds.center.x;
            cam.GetComponent<CameraMovement>().maxPosition.y = bounds.center.y;
            cam.GetComponent<CameraMovement>().minPosition.x = bounds.center.x;
            cam.GetComponent<CameraMovement>().minPosition.y = bounds.center.y;

           // Debug.Log("New bounds are: " + bounds);
        }

        if (other.CompareTag("Player") && maxEnemySpawn > 0 && enemiesLoaded == false)
        {
            foreach (Door door in doorComponents)
            {
                print("running door close code");
                door.CloseDoor();
            }

            LoadEnemies();


        }

        


        //if the room is empty, it opens all the doors by default.
        else if (maxEnemySpawn == 0)
        {
            foreach (Door door in doorComponents)
            {
                door.OpenDoor();
            }
        }

    }


    void CreateEnemies(int enemyNumber)
    {
        //create a list of enemies the first time the room gets entered
        if (needEnemySpawn) {

            for (int i = enemyNumber; i > 0; i--)
            {

                //chooses a random enemy from the enemy pool and spawns it 
                int foo = Random.Range(0, enemyPool.Count);
                Vector3 spawnPosition = RandomPointInBounds(bounds);
                GameObject enemy = Instantiate(enemyPool[foo], spawnPosition, Quaternion.identity);

                //adds enemy to enemy list
                enemyList.Add(enemy);
            }

            needEnemySpawn = false;


            //Unloads enemies immediately after creating them and adding them to the list of enemies.
            UnloadEnemies();

        }
        


    }






    void UnloadEnemies()
    {
        //loop through list of enemies and unload them.
        //probably getting called when the player leaves the room
        if (enemyList.Count > 0) 
        {
            foreach (GameObject enemy in enemyList)
            {
                enemy.gameObject.SetActive(false);
                enemy.GetComponent<LogEnemy>().enabled = true;
            }

            enemiesLoaded = false;
        }
    }

    void LoadEnemies()
    {
        if(enemyList.Count > 0 && enemyList != null) 
        {
            foreach (GameObject enemy in enemyList)
            {
                GameObject target = enemy;

                target.gameObject.SetActive(true);
                target.GetComponent<Enemy>().enabled = true;
                print("Spawning: " + target);
            }
            enemiesLoaded = true;
        }
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );

    }


    void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player") && enemiesLoaded == true && other.tag != null && enemyList.Count>0)
        {
            UnloadEnemies();

        }



    }


    private IEnumerator CheckForDeadEnemiesCo()
    {
        needCheckDead = false;
        List<int> tempList = new List<int>();


        if (enemyList.Count > 0 )
        {
            foreach (GameObject enemy in enemyList)
            {
                if (enemy.GetComponent<Enemy>().health <= 0)
                {
                    print("removing: " + enemy.name);
                    tempList.Add(enemyList.IndexOf(enemy));
                    
                }
            }
            foreach(int index in tempList)
            {
                enemyList.RemoveAt(index);
            }

            tempList.Clear();
        }
        //check for dead enemies and remove them from the list of enemies
        if (enemyList.Count <= 0)
        {
            foreach(Door door in doorComponents)
            {
                door.OpenDoor();
            }
        }


        yield return new WaitForSeconds(1f);
        needCheckDead = true;
    }

}
