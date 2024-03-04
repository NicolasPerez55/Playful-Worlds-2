using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Transform destin; //Next destination for the character to move to
    public Tilemap tilemap;
    [SerializeField] private SceneController scene;

    public Vector3 nextDestin;
    public Vector3Int gridPosition;
    public Vector3Int nextGridPosition;

    public float speed = 5f;
    public float speedModifier = 1.1f;

    public int stepCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        destin.parent = null;
        nextDestin = destin.position;
        gridPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        nextGridPosition = gridPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destin.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, destin.position) <= 0.1f)
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                nextDestin += new Vector3(1f, 0, 0);
                nextGridPosition += new Vector3Int(1, 0, 0);
                checkObstacle();
            }
            else if (Input.GetAxisRaw("Horizontal") == -1f)
            {
                nextDestin += new Vector3(-1f, 0, 0);
                nextGridPosition += new Vector3Int(-1, 0, 0);
                checkObstacle();
            }
            else if (Input.GetAxisRaw("Vertical") == 1)
            {
                nextDestin += new Vector3(0, 1f, 0);
                nextGridPosition += new Vector3Int(0, 1, 0);
                checkObstacle();
            }
            else if (Input.GetAxisRaw("Vertical") == -1f)
            {
                nextDestin += new Vector3(0, -1f, 0);
                nextGridPosition += new Vector3Int(0, -1, 0);
                checkObstacle();
            }

        }
    }

    //Used in movement to see what is on the next tile
    void checkObstacle()
    {
        Debug.Log("Checking obstacle");
        if (scene.checkPlayerStep(transform.position, nextDestin))
        {
            Debug.Log("Check found no obstacle");
            destin.position = nextGridPosition;
        }
        else
        {
            nextDestin = destin.position;
            gridPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
            nextGridPosition = gridPosition;
        }
        //destin.position = nextGridPosition;
        //if (tilemap.GetTile(nextDestin) == )
    }
}
