﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Spawner : MonoBehaviour {

    public GameObject astroidPrefab;
    public GameObject playerPrefab;
    private GameObject parentObject;

    private float leftSideX = -1.5f;
    private float rightSideX = 1.5f;
    private float currenty =  2.5f;
    private int obsticalZ = 0;
    private int shipsCurrentRockLevel = 0;
    private int rocksSpawnedAtOnce = 5;

    private List<GameObject> rocks;
    

	// Use this for initialization
	void Start () {

        rocks = new List<GameObject>();

        //spawnPlayer();
        spawnRocks(rocksSpawnedAtOnce);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void spawnRocksIfNeeded() {
        if (shipsCurrentRockLevel + 5 > rocks.Count) {
            //need more rocks
            Debug.Log(shipsCurrentRockLevel);
            Debug.Log(rocks.Count);
            spawnRocks(rocksSpawnedAtOnce);
        }
        shipsCurrentRockLevel++;
    }

    private void spawnPlayer() {
        Instantiate(playerPrefab, new Vector3(getRandomSide(), currenty), Quaternion.identity);
        currenty += 2.5f;
        spawnRocks(1);
    }

    private void spawnRocks(int iterations) {

        for (int i = 0; i < iterations; i++) {

            rocks.Add((GameObject)  Instantiate(astroidPrefab, new Vector3(getRandomSide(), currenty), new Quaternion(Random.Range(0,360),Random.Range(0,360),0,0)));
            currenty += 2.5f;
        }
    }

    private float getRandomSide() {
        float randSideX;

        //randomly choose left or right
        if (Random.value > 0.5) {
            randSideX = leftSideX;
        } else {
            randSideX = rightSideX;
        }

        return randSideX;
    }
}
