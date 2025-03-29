using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 rotationPoint;
    public static int height = 25;
    public static int width = 10;

    public static int oheight = 0;
    public static int owidth = 0;

    static float fallNormal = 0.025f;
    static float fallTime = 0.025f;

    private static Transform[,] grid = new Transform[width, height];
    private float fallFast = 0.005f;
    private float preTime;


    void Start()
    {
    }

    void Update()
    {
        if (Time.time - preTime > fallTime)
        {
            transform.position += new Vector3(0, -0.1f, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -0.1f, 0);
                AddToGrid();
                CheckForLines();

                this.enabled = false;
                FindFirstObjectByType<Spawn>().NewTermino();
            }
            preTime = Time.time;
        }

        if (Input.GetKey(KeyCode.S))
        {
            fallTime = fallFast;
        }
        else
        {
            fallTime = fallNormal;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
        }

        SetKey("A");
        SetKey("D");

        if (Input.GetKeyDown(KeyCode.E))
        {
            FindFirstObjectByType<Spawn>().HoldPiece(gameObject);
        }

    }

    public void SetCurrentPiece(GameObject piece)
    {
        transform.position = piece.transform.position;
        transform.rotation = piece.transform.rotation;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        FindFirstObjectByType<Spawn>().StopSpawn();
    }

    public void SetNewFallTime()
    {
        fallTime -= 0.001f;
        fallNormal -= 0.001f;
        Debug.Log("no działa" + fallTime);
    }

    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    void DeleteLine(int i)
    {
        for (int j = owidth; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    bool HasLine(int i)
    {
        for (int j = owidth; j < width; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
            }
        }
        return true;
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = owidth; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
        FindFirstObjectByType<Score>().IncreaseScore();
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int X = Mathf.RoundToInt(children.transform.position.x);
            int Y = Mathf.RoundToInt(children.transform.position.y);

            grid[X, Y] = children;

            if (Y >= height - 5)
            {
                GameOver();
            }
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int X = Mathf.RoundToInt(children.transform.position.x);
            int Y = Mathf.RoundToInt(children.transform.position.y);

            if (X < owidth || X >= width || Y < oheight || Y >= height)
            {
                return false;
            }
            if (grid[X, Y] != null)
            {
                return false;
            }
        }
        return true;
    }

    public void SetKey(string key)
    {
        if (System.Enum.TryParse(key, true, out KeyCode keyCode))
        {
            if (Input.GetKeyDown(keyCode))
            {
                MovePiece(keyCode);
            }
        }
    }

    public void MovePiece(KeyCode key)
    {
        Vector3 moveDirection = Vector3.zero;

        if (key == KeyCode.A) moveDirection = new Vector3(-1, 0, 0);
        else if (key == KeyCode.D) moveDirection = new Vector3(1, 0, 0);

        if (moveDirection != Vector3.zero)
        {
            transform.position += moveDirection;
            if (!ValidMove())
            {
                transform.position -= moveDirection;
            }
        }
    }
}