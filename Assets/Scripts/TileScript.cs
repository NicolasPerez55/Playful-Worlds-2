using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    //List of objects on this tile. such as player or cube
    //public List<GameObject> occupying;

    [SerializeField] private SpriteRenderer renderer;
    public GameObject occupant; //the object on this tile, such as a player or box
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
                break;
            case 8: //switch
                tileType = TileType.Switch;
                break;
            case 9: //togglewall
                tileType = TileType.ToggleWall;
                break;
            default:
                tileType = TileType.Ground;
                break;
        }
            
    }

    public void updateTile()
    {
        //deactivate the switch
        if (tileType == TileType.Switch && occupant == null)
        {

        }
        else if (tileType == TileType.Switch)
        {

        }
    }

    
    void Update()
    {
        
    }
}
