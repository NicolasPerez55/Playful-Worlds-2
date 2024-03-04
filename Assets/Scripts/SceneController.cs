using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public int currentCharacter = 1; //Which character the player currently controls

    public GameObject[] objects;
    public Dictionary<Vector3Int,TileScript> tiles; //The collection of all the tiles in the level

    //TileScript[,] 

    void Start()
    {
        tiles = new Dictionary<Vector3Int, TileScript>();
        objects = FindObjectsByType<GameObject>(FindObjectsSortMode.None); //CHANGE THIS TO SEARCH BY TILESCRIPT
        foreach (GameObject a in objects)
        {
            if (a.layer >= 6) //Only works if I make no non-tile layers above 6
            {
                tiles.Add(convertPos(a.transform.position), a.GetComponent<TileScript>());
            }
        }
    }


    void Update()
    {
        
    }

    public bool checkPlayerStep(Vector3 currentPosInput, Vector3 nextPosInput)
    {
        Vector3Int currentPos = convertPos(currentPosInput);
        Vector3Int nextPos = convertPos(nextPosInput);

        //Debug.Log(currentPos);
        Debug.Log(nextPos);
        //Debug.Log(tiles.ContainsKey(new Vector3Int(0, 0, -1));
        if (tiles.ContainsKey(nextPos) && tiles[nextPos].isWall() == false)
        {
            Debug.Log("Next tile is valid");
            return true;
        }
        else
        {
            Debug.Log("Next tile is not valid");
            return false;
        }
    }

    public Vector3Int convertPos(Vector3 pos)
    {
        return new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
    }
}
