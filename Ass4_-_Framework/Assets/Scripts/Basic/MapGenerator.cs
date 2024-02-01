// Author: Ziming Li
// Date: 2023-03-04
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // This is the map array
    public static int[,,] mapArray;
    // This is the map block array
    public static GameObject[,,] mapBlockArray;


    /// <summary>
    /// Generate a map with the map array, and save the map array and map block array.
    /// All the map blocks will be the children of the map object.
    /// The map array can be called by MapGenerator.mapArray.
    /// The map block array can be called by MapGenerator.mapBlockArray.
    /// </summary>
    public static void GenerateMap(GameObject map, int[] mapSize, int[,,] _mapArray, GameObject[] prefabList){
        GameObject tempObject;
        // Save the map array
        mapArray = _mapArray;
        // Init the map position
        map.transform.position = new Vector3(0, 0, 0);
        // Init the map block array
        mapBlockArray = new GameObject[mapSize[0], mapSize[1], mapSize[2]];
        // Generate the map
        for(int i = 0; i < mapSize[0]; i++){
            for(int j = 0; j < mapSize[1]; j++){
                for(int k = 0; k < mapSize[2]; k++){
                    tempObject = Instantiate(prefabList[_mapArray[i, j, k]], new Vector3(i, j, k), Quaternion.identity);
                    tempObject.transform.parent = map.transform;
                    tempObject.name = "MapBlock_" + i + "_" + j + "_" + k;
                    mapBlockArray[i, j, k] = tempObject;
                }
            }
        }
    }
}
