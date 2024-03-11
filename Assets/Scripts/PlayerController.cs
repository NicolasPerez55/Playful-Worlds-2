using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public bool isPlayer;

    public Transform destin; //Next destination for the character to move to
    //public Tilemap tilemap;
    [SerializeField] private SceneController scene;

    public Vector2 nextDestin;
    public Vector2Int gridPosition;
    public Vector2Int nextGridPosition;

    public float speed = 5f;
    public float speedModifier = 1.1f;

    public int stepCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        destin.parent = null;
        nextDestin = destin.position;
        gridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        nextGridPosition = gridPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destin.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, destin.position) <= 0.1f)
        {
            if (isPlayer && scene.canMove)
            {
                if (Input.GetAxisRaw("Horizontal") == 1) //CHANGE THIS TO JUST "GET KEY DOWN"
                {
                    moveCharacter("right");
                }
                else if (Input.GetAxisRaw("Horizontal") == -1f)
                {
                    moveCharacter("left");
                }
                else if (Input.GetAxisRaw("Vertical") == 1)
                {
                    moveCharacter("up");
                }
                else if (Input.GetAxisRaw("Vertical") == -1f)
                {
                    moveCharacter("down");
                }
            }
        }
    }

    public void moveCharacter(string direction)
    {
        switch (direction)
        {
            case "left":
                nextDestin += new Vector2(-1f, 0);
                nextGridPosition += new Vector2Int(-1, 0);
                checkObstacle("left");
                break;
            case "right":
                nextDestin += new Vector2(1f, 0);
                nextGridPosition += new Vector2Int(1, 0);
                checkObstacle("right");
                break;
            case "up":
                nextDestin += new Vector2(0, 1f);
                nextGridPosition += new Vector2Int(0, 1);
                checkObstacle("up");
                break;
            case "down":
                nextDestin += new Vector2(0, -1f);
                nextGridPosition += new Vector2Int(0, -1);
                checkObstacle("down");
                break;
            default:
                break;
        }
    }

    //Used in movement to see what is on the next tile
    void checkObstacle(string direction)
    {
        Debug.Log("Checking obstacle");
        if (scene.checkPlayerStep(transform.position, nextDestin, direction))
        {
            Debug.Log("Check found no obstacle");
            destin.position = new Vector3(nextGridPosition.x, nextGridPosition.y, destin.position.z);
        }
        else
        {
            nextDestin = destin.position;
            gridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            nextGridPosition = gridPosition;
        }
        //destin.position = nextGridPosition;
        //if (tilemap.GetTile(nextDestin) == )
    }

    Vector2 posTo2dPos(Vector3 position)
    {
        return new Vector2(position.x, position.y);
    }
}
