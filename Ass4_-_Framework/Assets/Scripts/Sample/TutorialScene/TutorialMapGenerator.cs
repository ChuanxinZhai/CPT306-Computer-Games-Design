using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMapGenerator : MonoBehaviour
{
    // This is the map object
    public GameObject map;
    // This is the prefab list
    public GameObject[] prefabList;
    // This is the map size
    public int[] mapSize = new int[3] { 50, 3, 50 };
    // This is the map array
    public int[,,] mapArray;
    // This is the player prefab
    public GameObject playerPrefab;
    // This is the player init position
    public Vector3 playerInitPosition = new Vector3(25, 6, 25);


    // Generate the map array
    void GenerateMapArray(){
        // Init the map array
        mapArray = new int[mapSize[0], mapSize[1], mapSize[2]];
        // Generate the map array
        for(int i = 0; i < mapSize[0]; i++){
            for(int k = 0; k < mapSize[2]; k++){
                if (i <= 10 && k <= 10) {
                    mapArray[i, 0, k] = 0;
                    mapArray[i, 1, k] = 0;
                    mapArray[i, 2, k] = 0;
                }
                else if (i + k <= 30) {
                    mapArray[i, 0, k] = 0;
                    mapArray[i, 1, k] = 0;
                    mapArray[i, 2, k] = 1;
                }
                else{
                    mapArray[i, 0, k] = 0;
                    mapArray[i, 1, k] = 1;
                    mapArray[i, 2, k] = 1;
                }
            }
        }
    }

    // Generate the player
    void GeneratePlayer(){
        GameObject tempObject;
        tempObject = Instantiate(playerPrefab, playerInitPosition, Quaternion.identity);
        tempObject.name = "Player";
        tempObject.transform.tag = "Player";
        tempObject.AddComponent<PlayerMovement>();
        tempObject.AddComponent<Rigidbody>();
        PlayerMovement.maxJumpTimes = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateMapArray();
        MapGenerator.GenerateMap(map, mapSize, mapArray, prefabList);
        GeneratePlayer();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
