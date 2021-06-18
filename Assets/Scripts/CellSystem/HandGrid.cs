using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrid : MonoBehaviour
{
    [SerializeField] public Cell[,] Grid;
    [SerializeField] private LayerMask obstrugle;
    private int width;
    private int height;

    public List<Cell> path;


    public static HandGrid Instance;

    public int Width { get => width; }
    public int Height { get => height; }

    public HandGrid(int _width, int _height)
    {
        width = _height;
        height = _height;
    }

    public bool IsWalkable(Cell _cell)
    {
        return !(Physics.CheckSphere(_cell.WorldPosition+_cell.Size/2, (_cell.Size.z) / 4, obstrugle));
    }

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        width = (int)transform.localScale.x;
        height = (int)transform.localScale.z;
        Grid = new Cell[Width, Height];
        Vector3 cellSize = new Vector3(transform.localScale.x / Width, transform.localScale.y/Width,transform.localScale.z/Height);
        Vector3 leftBottomPos = transform.position - Vector3.right * transform.localScale.x / 2 - Vector3.forward * transform.localScale.z / 2;

        for (int x = 0; x < Grid.GetLength(0); x++)
        {
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                Vector3 cellPosition = leftBottomPos + Vector3.right * x * cellSize.x + Vector3.forward * y * cellSize.z;
                bool walkable = true;
                Grid[x, y] = new Cell(walkable, cellPosition, cellSize, x, y);              
            }
        }
    }


  
    public Cell CellFromWorldPosition(Vector3 _worldPosition)
    {

        float percentX = (_worldPosition.x + transform.localScale.x / 2) / transform.localScale.x;
        float percentY = (_worldPosition.z + transform.localScale.z / 2) / transform.localScale.z;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = (int)((Width) * percentX);
        int y = (int)((Height) * percentY);

        return Grid[x, y];
    }
  
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, 1, Height));
        if (Grid != null)
        {

            foreach (Cell cell in Grid)
            {
                if (cell.Walkable)
                    Gizmos.color = Color.white;
                else if (!cell.Walkable)
                    Gizmos.color = Color.black;

                Gizmos.DrawCube(cell.WorldPosition + cell.Size / 2, cell.Size);

            }
        }
    }
}
