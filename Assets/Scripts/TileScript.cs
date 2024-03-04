using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    //List of objects on this tile. such as player or cube
    //public List<GameObject> occupying;

    [SerializeField] private SpriteRenderer renderer;
    public GameObject occupant; //the object on this tile, such as a player or box
    private bool unoccupied = true; //Used to see if an object just entered the tile

    public bool canMoveTo = true; //MIGHT NOT BE NEEDED
    public bool active = false; //For Switches and toggle walls. Inactive togglewall can be passed through
    public enum TileType 
    { 
        Ground, Wall, Switch, ToggleWall
    }

    public TileType tileType;
    void Start()
    {
        //occupying = new List<GameObject>();
        switch (this.gameObject.layer)
        {
            case 6: //ground
                tileType = TileType.Ground;
                break;
            case 7: //wall
                tileType = TileType.Wall;
                canMoveTo = false;
                break;
            case 8: //switch
                tileType = TileType.Switch;
                break;
            case 9: //togglewall
                tileType = TileType.ToggleWall;
                if (active) canMoveTo = false;
                break;
            default:
                tileType = TileType.Ground;
                break;
        }
            
    }

    public void updateTile() //NEED to clean this up, use switch statements for updating the properties
    {
        //Check if something is on the tile
        if (occupant != null) //Something is on the tile
        {
            canMoveTo = false;
            if (unoccupied == true) //Something just moved into the switch, CHECK WHAT THE OCCUPANT IS
            {
                unoccupied = false;
                if (tileType == TileType.Switch)
                {
                    //Send a signal here
                    renderer.sprite = Resources.Load("Assets/Sprites/switchOn.png") as Sprite;
                }
            }
        }
        else //Nothing is on the switch
        {
            if (isWall() == false) canMoveTo = true;
            if (unoccupied == false)
            {
                unoccupied = true;
                if (tileType == TileType.Switch)
                {
                    //Send a signal here
                    renderer.sprite = Resources.Load("Assets/Sprites/switchOff.png") as Sprite;
                }
            }
        }
    }

    public bool isWall()
    {
        if (tileType == TileType.Wall || (tileType == TileType.ToggleWall && active)) return true;
        else return false;
    }

    
    void Update()
    {
        
    }
}
