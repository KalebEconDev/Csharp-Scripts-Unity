using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{


    /* declaring a list now because later i want
     * to add all of the enemies to a list and add some interaction between spawner and enemy.
     * 
     * 
     * I.E. if the spawner is alive the enemies that it spawned get a defense buff so the player rushes to 
     * defeat the spawner first then kill the 
     * weakened enemies.
     * 
     */


    private List<GameObject> enemyList;
    public float spawnRate;
    public Sprite sprite;
    public int mobType; //1 is slime.
    public float spawnArea;
    private bool spawnReady;
    private float spawnLocX;
    private float spawnLocY;
    public GameObject enemyPrefab;
    private Transform transform;
    private GameObject player;
    private bool needCheck;

    // not activated by default
    // once activated spawns enemies for a set amount of time regardless of player position.
    // does not spawn enemies until player activates it


    private bool activated;





    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Sprite>();
        spawnReady = true;
        transform = GetComponent<Transform>();
        activated = false;
        player = GameObject.FindGameObjectWithTag("Player");
        needCheck = true;


    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.transform.position, gameObject.GetComponent<Transform>().position) < 200)
        {
            
        }

        if (!activated && needCheck)
        {
            StartCoroutine(checkActivate());
        }


        if (spawnReady && activated)
        {
            StartCoroutine(SpawnEnemyCo());
            Debug.Log("Player is " + Vector3.Distance(player.transform.position, gameObject.GetComponent<Transform>().position) + " away from spawner.");

        }
    }


    private IEnumerator SpawnEnemyCo()
    {
        spawnReady = false;
        //spawning logic goes below here
        spawnLocX = Random.Range(-spawnArea,spawnArea);
        spawnLocY = Random.Range(-spawnArea, spawnArea);

        switch (mobType)
        {
            //slime is 1
            case 1:
                GameObject enemy = Instantiate(enemyPrefab, new Vector3(transform.position.x + spawnLocX, transform.position.y + spawnLocY,transform.position.z),Quaternion.identity); 
                break;



            default:
                Debug.Log("Tried to spawn a mob, and failed!");
                break;
        }







        //Spawn Rate set by variable spawnRate
        //Higher number = longer cooldown = less spawns
        yield return new WaitForSeconds(1*spawnRate);
        spawnReady = true;

    }


    private IEnumerator checkActivate()
    {
        needCheck = false;
        if (Vector3.Distance(player.transform.position,gameObject.GetComponent<Transform>().position) < 25)
        {

            activated = true;
            yield return new WaitForSeconds(30f);

            //spawns mobs for 30 seconds then deactivates again if the player is far away.
            activated = false;


        }



        yield return new WaitForSeconds(3f);
        needCheck = true;

    }




}
