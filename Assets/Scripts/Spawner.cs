using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Spawner : MonoBehaviour {

    public GameObject astroidPrefab;
    public GameObject playerPrefab;
    private GameObject parentObject;

    private const float LEFTSIDEX = -1.5f;
    private const float RIGHTSIDEX = 1.5f;
    private float currenty =  2.5f;
    private int obsticalZ = 0;
    private int shipsCurrentRockLevel = 0;
    private int rocksSpawnedAtOnce = 5;

    private List<GameObject> rocks;
    

    /// <summary>
    /// Starts this game object
    /// </summary>
	void Start () {

        rocks = new List<GameObject>();

        //spawnPlayer();
        spawnRocks(rocksSpawnedAtOnce);

	}
	
    /// <summary>
    /// Updates every frame
    /// </summary>
	void Update () {
	
	}

    /// <summary>
    /// Spawns rocks if they are needed
    /// </summary>
    public void spawnRocksIfNeeded() {
        if (shipsCurrentRockLevel + 5 > rocks.Count) {
            //need more rocks
            Debug.Log(shipsCurrentRockLevel);
            Debug.Log(rocks.Count);
            spawnRocks(rocksSpawnedAtOnce);
        }
        shipsCurrentRockLevel++;
        spawnBackgroundFlyingRock();
    }

    /// <summary>
    /// Spawn flying rocks in the background.
    /// </summary>
    private void spawnBackgroundFlyingRock() {
        GameObject rock = Instantiate(astroidPrefab, new Vector3(5, Random.Range(currenty + 2f, currenty - 2f)), Quaternion.identity) as GameObject;

        rock.transform.localScale = new Vector3(0.3f, 0.3f);
        rock.transform.position -= new Vector3(0f, 0f, 5f); //Move rock into background
        Rigidbody2D rockBod = rock.GetComponent<Rigidbody2D>();
        rockBod.isKinematic = false;
        rockBod.velocity = Vector3.left;
    }


    private void spawnPlayer() {
        Instantiate(playerPrefab, new Vector3(getRandomFixedSide(), currenty), Quaternion.identity);
        currenty += 2.5f;
        spawnRocks(1);
    }

    private void spawnRocks(int iterations) {

        for (int i = 0; i < iterations; i++) {

            rocks.Add((GameObject)  Instantiate(astroidPrefab, new Vector3(getRandomFixedSide(), currenty), new Quaternion(Random.Range(0,360),Random.Range(0,360),0,0)));
            currenty += 2.5f;
        }
    }

    private float getRandomFixedSide() {
        float randSideX;

        //randomly choose left or right
        if (Random.value > 0.5) {
            randSideX = LEFTSIDEX;
        } else {
            randSideX = RIGHTSIDEX;
        }

        return randSideX;
    }

    private float getRandomBackgroundRockSide()
    {
        float randSideX;

        //randomly choose left or right
        if (Random.value > 0.5)
        {
            randSideX = LEFTSIDEX - 2f;
        }
        else
        {
            randSideX = RIGHTSIDEX + 2f;
        }

        return randSideX;
    }
}
