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
    private float shipsCurrentRockLevel = 0;
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
            spawnBackgroundFlyingRock();
        }
        shipsCurrentRockLevel += 2.5f;
        //spawnBackgroundFlyingRock();
    }

    /// <summary>
    /// Spawn flying rocks in the background.
    /// </summary>
    private void spawnBackgroundFlyingRock() {
        float backgroundLevel = Random.Range(1f, 10f);
        float spawningY = Random.Range(shipsCurrentRockLevel + 4f, shipsCurrentRockLevel - 0f);
        float spawningSide = getRandomBackgroundRockSide();

        GameObject rock = Instantiate(astroidPrefab, new Vector3(spawningSide, spawningY, backgroundLevel), Quaternion.identity) as GameObject;

        //set scale
        rock.transform.localScale = GetRandomScale();

        //set darkness to simulate distance
        float darknessScale = Random.Range(20f, 200f);
        rock.GetComponent<SpriteRenderer>().color += new Color(darknessScale, darknessScale, darknessScale, 255f);
        Debug.Log(rock.GetComponent<SpriteRenderer>().color);

        //set velocity
        Rigidbody2D rockRigidBody = rock.GetComponent<Rigidbody2D>();
        rockRigidBody.isKinematic = false;  //Cannot be Kinematic 
        rockRigidBody.velocity = GetRandomVelocity();

        //Disable collider
        rock.GetComponent<CircleCollider2D>().enabled = false;
    }

    private Vector3 GetRandomScale()
    {
        float randomScale = Random.Range(0.01f, 1f);
        return new Vector3(randomScale, randomScale);
    }

    private Vector3 GetRandomVelocity()
    {
        float ranx = Random.Range(-5f, 5f);
        float rany = Random.Range(-5f, 5f);
        return new Vector3(ranx, rany);
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
