using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public struct Grid
    {
        public int collum, rows;
        public float verticalOffset, horizontalOffset;
    }
    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();
    void Awake()
    {
        grid.collum = 24;
        grid.rows = 13;
        GenerateGrid();
    }
    public void GenerateGrid()
    {
        transform.position = GetComponentInParent<Transform>().position;
        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.collum; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.GetComponent<Transform>().position = new Vector2(x - (grid.collum - grid.horizontalOffset) + GetComponentInParent<Transform>().position.x, y - (grid.rows - grid.verticalOffset) + GetComponentInParent<Transform>().position.y);
                go.name = "X: " + x + ", Y: " + y;
                availablePoints.Add(go.transform.position);
            }
        }

    }

}
