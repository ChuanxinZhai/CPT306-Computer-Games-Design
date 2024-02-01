using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This is the map object
    public GameObject map;
    // This is the prefab list
    public GameObject[] prefabList;
    // This is the map size
    public int[] mapSize = new int[3] { 36, 6, 36 };
    // This is the map array
    public int[,,] mapArray;
    // This is the sea width
    public int seaWidth = 3;
    // This is the sea height
    public float seaHeight = 0.5f;
    // This is the sea power
    public int seaPower = 2;
    // This is the wave speed
    public float waveSpeed = 0.5f;
    // This is min land height
    public int minLandHeight = 2;
    // This is the smoothness of the map
    public int smoothness = 20;


    // Return the perlin noise value which is between 2 and prefabList.Length
    // The smoothness is the smoothness of the map
    int GetPerlinNoise(int x, int z){
        return Mathf.RoundToInt(Mathf.PerlinNoise(x / smoothness, z / smoothness) * (prefabList.Length - 2)) + 2;
    }


    void GenerateMapArray(){
        // Temp variables
        int tempHeight;
        // Init the map array
        mapArray = new int[mapSize[0], mapSize[1], mapSize[2]];
        // Generate the map array
        for(int i = 0; i < mapSize[0]; i++){
            for(int k = 0; k < mapSize[2]; k++){
                // If the map block is in the sea, the map height will be 1
                if(i < seaWidth || i >= mapSize[0] - seaWidth || k < seaWidth || k >= mapSize[2] - seaWidth){
                    // Fill the map block with the sea
                    mapArray[i, 0, k] = 0;
                    // Fill the rest map block with the air
                    for(int j = 1; j < mapSize[1]; j++)
                        mapArray[i, j, k] = 1;
                }
                else
                {
                    // Generate the map height
                    tempHeight = Random.Range(minLandHeight, mapSize[1]);
                    // Fill the map block with the ground
                    for(int j = 0; j < tempHeight; j++)
                        mapArray[i, j, k] = GetPerlinNoise(i, k);
                    // Fill the rest map block with the air
                    for(int j = tempHeight; j < mapSize[1]; j++)
                        mapArray[i, j, k] = 1;
                }
            }
        }
    }


    float GetSinValue(float x, float z){
        return seaHeight * Mathf.Sin(x / seaPower) * Mathf.Sin(z / seaPower);
    }


    void InitSeaCube(){
        for(int i = 0; i < mapSize[0]; i++){
            for(int k = 0; k < mapSize[2]; k++){
                // If the map block is the sea
                if(i < seaWidth || i >= mapSize[0] - seaWidth || k < seaWidth || k >= mapSize[2] - seaWidth){
                    // Init the height of the sea cube with the sin value
                    MapGenerator.mapBlockArray[i, 0, k].transform.position = new Vector3(i, GetSinValue(i, k), k);
                    // Set the movement direction of the sea cube
                    if (GetSinValue(i, k) < GetSinValue(i + 1, k + 1))
                        EventAdder.AddEvent(MapGenerator.mapBlockArray[i, 0, k], "SeaMovement", new string[3] {"moveUp", "seaHeight", "waveSpeed"}, new object[3] {1, seaHeight, waveSpeed});
                    else
                        EventAdder.AddEvent(MapGenerator.mapBlockArray[i, 0, k], "SeaMovement", new string[3] {"moveUp", "seaHeight", "waveSpeed"}, new object[3] {-1, seaHeight, waveSpeed});
                }
            }
        }
    }


    void Start()
    {
        GenerateMapArray();
        MapGenerator.GenerateMap(map, mapSize, mapArray, prefabList);
        InitSeaCube();
    }

}
