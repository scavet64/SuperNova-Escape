using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    /********** vv Set in Editor vv **********/

    /// <summary>
    /// Player prefab object
    /// </summary>
    public GameObject PlayerPrefab;

    /// <summary>
    /// Array containing all of then astroid prefabs
    /// </summary>
    public GameObject[] AstroidPrefabs;

    /// <summary>
    /// Background prefab object
    /// </summary>
    public GameObject BackgroundPrefab;

    public GameObject backgroundGameObject;

    /********** ^^ Set in Editor ^^ **********/

    /// <summary>
    /// Parent GameObject to hold the astroids
    /// </summary>
    private GameObject parentObject;

    /// <summary>
    /// Constant for left side x position
    /// </summary>
    private const float LEFTSIDEX = -1.5f;

    /// <summary>
    /// Constant for the right side x position
    /// </summary>
    private const float RIGHTSIDEX = 1.5f;

    /// <summary>
    /// Current player y position
    /// </summary>
    private float currenty =  2.5f;

    /// <summary>
    /// Obstical astroid z position
    /// </summary>
    private const int obsticalZ = 0;

    /// <summary>
    /// The current y position for the current rock level
    /// </summary>
    private float shipsCurrentYRockLevel = 0;

    /// <summary>
    /// Current rock the player is on.
    /// </summary>
    private int shipsCurrentRockLevel;

    /// <summary>
    /// The number of rocks spawned at a single time.
    /// </summary>
    private const int rocksSpawnedAtOnce = 5;

    /// <summary>
    /// List containing all of the spawned rocks
    /// </summary>
    public static List<GameObject> currentSpawnedRocks { get; private set; }

    /// <summary>
    /// This counts the number of times the player has jumped. This is used to indicate when we should spawn another background image
    /// </summary>
    private int timeSinceLastBackgroundSpawn = 249;

    /// <summary>
    /// This number represnets the number of jumps a player must make until a new background image is spawned.
    /// </summary>
    private int spawnBackgroundAfter = 250;

    /// <summary>
    /// The next Y position the new background image will have.
    /// </summary>
    private int nextBackgroundSpawnY = 18;
    
    /// <summary>
    /// Starts this game object
    /// </summary>
	void Start () 
    {
        currentSpawnedRocks = new List<GameObject>();
        spawnRocks(rocksSpawnedAtOnce);
	}

    /// <summary>
    /// Spawns rocks if they are needed
    /// </summary>
    public void spawnRocksIfNeeded() 
    {
        if (shipsCurrentRockLevel + 5 > currentSpawnedRocks.Count) 
        {
            //need more rocks
            Debug.Log(shipsCurrentYRockLevel);
            Debug.Log(currentSpawnedRocks.Count);
            spawnRocks(rocksSpawnedAtOnce);
        }

        if(shipsCurrentRockLevel % 3 == 0) 
        {
            spawnMultipleBackgroundRocks(1);
        }

        shipsCurrentRockLevel++;
        shipsCurrentYRockLevel += 2.5f;
    }

    /// <summary>
    /// Spawns multiple rocks
    /// </summary>
    /// <param name="numberOfRocks">How many rocks to spawn</param>
    private void spawnMultipleBackgroundRocks(int numberOfRocks)
    {
        for(int i = 0; i < numberOfRocks; i++) 
        {
            spawnBackgroundFlyingRock();
        }
    }

    /// <summary>
    /// Spawns a background rock if needed.
    /// </summary>
    public void spawnBackgroundIfNeeded() 
    {
        if(timeSinceLastBackgroundSpawn > spawnBackgroundAfter) 
        {
            spawnBackground();
            timeSinceLastBackgroundSpawn = 0;
        } 
        else 
        {
            timeSinceLastBackgroundSpawn++;
        }

    }

    /// <summary>
    /// Spawn flying rocks in the background.
    /// </summary>
    private void spawnBackgroundFlyingRock() 
    {
        float backgroundLevel = Random.Range(1f, 10f);
        float spawningY = Random.Range(shipsCurrentYRockLevel + 15f, shipsCurrentYRockLevel - 0f);
        float spawningSide = getRandomBackgroundRockSide();
        GameObject randomAstroid = GetRandomAstroid();

        GameObject rock = Instantiate(randomAstroid, new Vector3(spawningSide, spawningY, backgroundLevel), Quaternion.identity) as GameObject;

        //set scale
        rock.transform.localScale = GetRandomScale();

        //set darkness to simulate distance
        float darknessScale = Random.Range(0.4f, 0.8f);
        rock.GetComponent<SpriteRenderer>().color = new Color(darknessScale, darknessScale, darknessScale, 1);
        Debug.Log(rock.GetComponent<SpriteRenderer>().color);

        //set velocity
        Rigidbody2D rockRigidBody = rock.GetComponent<Rigidbody2D>();
        rockRigidBody.isKinematic = false;  //Cannot be Kinematic 
        rockRigidBody.velocity = GetRandomVelocity(spawningSide);

        //Disable collider
        rock.GetComponent<CircleCollider2D>().isTrigger = true;
        rock.gameObject.tag = "Background";
        Debug.Log("Finished Spawning Rock");
    }

    /// <summary>
    /// Returns a random astroid astroid prefab from the list
    /// </summary>
    /// <returns>Random Astroid Prefab</returns>
    private GameObject GetRandomAstroid()
    {
        return AstroidPrefabs[Random.Range(0, AstroidPrefabs.Length)];
    }

    /// <summary>
    /// Returns a vector 3 that should be used to set the scale
    /// </summary>
    /// <returns>vector3 with random X and Y values</returns>
    private Vector3 GetRandomScale()
    {
        float randomScale = Random.Range(0.01f, 1f);
        return new Vector3(randomScale, randomScale);
    }

    /// <summary>
    /// Returns a Vector3 with random X and Y values that should be used for velocity
    /// </summary>
    /// <returns>Vector3 with random X and Y values</returns>
    private Vector3 GetRandomVelocity(float spawingSide)
    {
        float ranx = Random.Range(0.5f, 5f);
        if (spawingSide > 0) {
            ranx *= -1;
        }
        float rany = Random.Range(-5f, 5f);
        return new Vector3(ranx, rany);
    }

    /// <summary>
    /// Spawn the player on a random side
    /// </summary>
    private void spawnPlayer() 
    {
        Instantiate(PlayerPrefab, new Vector3(getRandomFixedSide(), currenty), Quaternion.identity);
        currenty += 2.5f;
        spawnRocks(1);
    }

    /// <summary>
    /// Spawn a new background image and add it to the parent game object.
    /// </summary>
    private void spawnBackground() 
    {
        GameObject newBack = (GameObject) Instantiate(BackgroundPrefab, new Vector3(0, nextBackgroundSpawnY), Quaternion.identity);
        newBack.transform.parent = backgroundGameObject.transform;
        newBack.transform.localPosition = new Vector3(0, nextBackgroundSpawnY);
        newBack.transform.Rotate(new Vector3(0, 0, -90f));
        newBack.transform.localScale = (new Vector3(1, 1, 1));

        nextBackgroundSpawnY += 18;
    }

    /// <summary>
    /// spawn a rock the passed in number of times. This method spawns a random astroid
    /// </summary>
    /// <param name="iterations">Number of rocks to spawn</param>
    private void spawnRocks(int iterations) 
    {

        for (int i = 0; i < iterations; i++) 
        {
            currentSpawnedRocks.Add((GameObject)  Instantiate(GetRandomAstroid(), new Vector3(getRandomFixedSide(), currenty), new Quaternion(Random.Range(0,360),Random.Range(0,360),0,0)));
            currenty += 2.5f;
        }
    }

    /// <summary>
    /// Get random fixed side.
    /// </summary>
    /// <returns>Returns one of the set side float values</returns>
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

    /// <summary>
    /// Returns a random side for the background rocks
    /// </summary>
    /// <returns></returns>
    private float getRandomBackgroundRockSide()
    {
        float randSideX;

        //randomly choose left or right
        if (Random.value > 0.5)
        {
            randSideX = LEFTSIDEX - 5f;
        }
        else
        {
            randSideX = RIGHTSIDEX + 5f;
        }
        return randSideX;
    }
}
