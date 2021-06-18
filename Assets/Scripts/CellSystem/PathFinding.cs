using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] private int costForward = 10;
    [SerializeField] private int costDiagonal = 14;
    private GridField grid;

    private void Awake()
    {
        grid = GetComponent<GridField>();
    }
    
    public List<Cell> Path(Cell _startCell, Cell _endCell)
    {
        List<Cell> path = new List<Cell>();
        Cell currentCell = _endCell;
        while(currentCell != _startCell)
        {
            path.Add(currentCell);
            currentCell = currentCell.Parent;
        }
        path.Reverse();
        return path;
    }
    public List<Cell> PathFind(Vector3 _startpos, Vector3 _endpos)
    {
         List<Cell> OpenCells = new List<Cell>();
     HashSet<Cell> ClosedCells = new HashSet<Cell>();
    Cell startCell = grid.CellFromWorldPosition(_startpos);
        Cell endCell = grid.CellFromWorldPosition(_endpos);

        OpenCells.Add(startCell);

        while (OpenCells.Count>0)
        {
            
            Cell currentCell = OpenCells[0];
      
            for(int i=1; i<OpenCells.Count;i++)
            {
   
                if(OpenCells[i].Fcost<currentCell.Fcost || (OpenCells[i].Fcost==currentCell.Fcost && OpenCells[i].Hcost<currentCell.Hcost))
                {
                    currentCell = OpenCells[i];
                }  
            }
            OpenCells.Remove(currentCell);
            ClosedCells.Add(currentCell);
            currentCell.Open = false;

           foreach(Cell endneihgnour in grid.GetNeighbours(endCell))
            {
                if(currentCell==endneihgnour)
                {
                    grid.path = Path(startCell, endneihgnour);
                
                    return grid.path;
                }
            }
              
            

            foreach (Cell neighbour in grid.GetNeighbours(currentCell))
            {
                
                if (!GridField.Instance.IsWalkable(neighbour) || ClosedCells.Contains(neighbour))
                    continue;
                int newGcost = GetDistance(currentCell, neighbour) + currentCell.Gcost;
                if ( newGcost < currentCell.Gcost || !OpenCells.Contains(neighbour))
                {
                    neighbour.Gcost = newGcost;
                    neighbour.Hcost = GetDistance(neighbour,endCell);
                    neighbour.Parent = currentCell;

                    if (!OpenCells.Contains(neighbour))
                    {
                        OpenCells.Add(neighbour);
                        neighbour.Open = true;
                    }
                        
                }

            }
        }
        return null;
    }
    public int GetDistance(Cell _startCell, Cell _endCell)
    {
        int distX = Mathf.Abs(_startCell.X - _endCell.X);
        int distY = Mathf.Abs(_startCell.Y - _endCell.Y);

        if(distX>distY)
        {
            return costDiagonal * distY + costForward * (distX - distY);
        }
        return costDiagonal * distX + costForward * (distY - distX);
    }
}
