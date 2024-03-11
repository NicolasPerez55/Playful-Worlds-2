using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private TextMeshPro information;

    public bool canMove = true;

    public int characterLimit = 3; //How many characters are in this level. 2 means one clone, 3 means two clones.
    public int currentCharacter = 1; //Which character the player currently controls
    public PlayerController player;
    public PlayerController cloneOne;
    public PlayerController cloneTwo;

    public Vector2Int cloneOneStartPos;
    public Vector2Int cloneTwoStartPos;

    //the stored inputs
    public string[] cloneOneSteps;
    public string[] cloneTwoSteps;

    public int lifespanLimit = 10; //When currentLifespan reaches this, switch to next clone
    public int currentLifespan = 0;

    public GameObject[] objects;
    public Dictionary<Vector2Int,TileScript> tiles; //The collection of all the tiles in the level

    //TileScript[,] 

    void Start()
    {
        tiles = new Dictionary<Vector2Int, TileScript>();
        objects = FindObjectsByType<GameObject>(FindObjectsSortMode.None); //CHANGE THIS TO SEARCH BY TILESCRIPT
        foreach (GameObject a in objects)
        {
            if (a.layer >= 6) //Only works if I make no non-tile layers above 6
            {
                tiles.Add(convertPos(a.transform.position), a.GetComponent<TileScript>());
            }
        }

        cloneOneStartPos = new Vector2Int(-1, -2);
        cloneTwoStartPos = new Vector2Int(1, -2);
        cloneOneSteps = new string[lifespanLimit];
        cloneTwoSteps = new string[lifespanLimit];
    }


    void Update()
    {
        information.text = "Lifespan: \n" + (lifespanLimit - currentLifespan) + "\nIteration: \n" + currentCharacter;
    }

    public bool checkPlayerStep(Vector2 currentPosInput, Vector2 nextPosInput, string direction)
    {
        Vector2Int currentPos = convertPos(currentPosInput);
        Vector2Int nextPos = convertPos(nextPosInput);

        //Debug.Log(currentPos);
        Debug.Log(nextPos);
        //Debug.Log(tiles.ContainsKey(new Vector3Int(0, 0, -1));
        if (tiles.ContainsKey(nextPos) && tiles[nextPos].isWall() == false)
        {
            Debug.Log("Next tile is valid");
            if (currentCharacter == 1)
            {
                cloneOneSteps[currentLifespan] = direction;
            }
            else
            {
                cloneTwoSteps[currentLifespan] = direction;
            }
            currentLifespan += 1;
            if (currentLifespan >= lifespanLimit) changeCharacters();
            return true;
        }
        else
        {
            Debug.Log("Next tile is not valid");
            return false;
        }
    }

    public void changeCharacters()
    {
        if (currentCharacter == characterLimit)
        {
            levelOver(); //Placeholder for a level finish check, if not the player can just rewind
        }
        else
        {
            if (currentCharacter == 1)
            {
                cloneOne.gameObject.SetActive(true);
                cloneOne.transform.position = new Vector3(cloneOneStartPos.x, cloneOneStartPos.y,1);
            }
            else if (currentCharacter == 2)
            {
                cloneTwo.gameObject.SetActive(true);
                cloneTwo.transform.position = new Vector3(cloneTwoStartPos.x, cloneTwoStartPos.y, 1);
            }
            currentLifespan = 0;
        }
    }

    void levelOver()
    {
        canMove = false;
    }

    public Vector2Int convertPos(Vector2 pos)
    {
        return new Vector2Int((int)pos.x, (int)pos.y);
    }
}
