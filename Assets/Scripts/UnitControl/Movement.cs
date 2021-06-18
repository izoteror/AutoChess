using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Movement : MonoBehaviour
{
    

   [SerializeField] private GameObject _target;
    private Cell currentCell;
    private Transform _selfTransform;
    private Vector3 _pos;
    private int _distance;
    private GameObject _canvas;

    public GameObject Target { get => _target; set => _target = value; }
    public Cell CurrentCell { get => currentCell; set => currentCell = value; }

    private void Awake()
    {
       // _canvas = gameObject.transform.GetChild(2).gameObject;
      
    }

    public void MoveTo(GameObject target, int distance)
    {
        Target = target;
        _distance = distance;
        CurrentCell = GridField.Instance.CellFromWorldPosition(transform.position);
        _pos = CurrentCell.WorldPosition + CurrentCell.Size / 2;
        StartCoroutine(Walk());
    }

    IEnumerator Walk()
    {
        while (Target)
        {
            yield return new WaitForSeconds(1f);
            Move();
            
        }
    }

    

    public void Move()
    {
        if (Target)
        {
            transform.position = _pos;
            List<Cell> path = GridField.Instance.gameObject.GetComponent<PathFinding>().PathFind(_pos, Target.transform.position);
            if(path == null)
            {
               
                    Debug.Log("Cannot Go to Target!!!");
                GetComponent<Unit>().ToAtack(Target);
                transform.position = _pos;
                return;
            }
            if (path.Count > _distance)
            {
                Cell _prevCell = GridField.Instance.CellFromWorldPosition(_pos);
                _pos = path[0].WorldPosition + CurrentCell.Size / 2;
                Cell _currentCell = GridField.Instance.CellFromWorldPosition(_pos);
                _prevCell.Walkable = true;
                _currentCell.Walkable = false;
                
                if (GetComponent<Unit>().Team == 0)
                {
                    Target = GetComponent<Unit>().GetTarget(MatchControl.Instant.AIUnits).gameObject;
                }
                else
                {
                    Target = GetComponent<Unit>().GetTarget(MatchControl.Instant.Units).gameObject;
                }
                

            }
            else if (path.Count <= _distance && !GetComponent<Unit>().IsAtacked)
                GetComponent<Unit>().ToAtack(Target);
            
        }
        else if (!Target)
        {
            GetComponent<Unit>().Find();
        }
    }
    private void Update()
    {
        Vector3 targetDirection;
        if (Target)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pos, 2 * Time.deltaTime);

            targetDirection = Target.transform.position - transform.position;
            GetComponent<FigurePresenter>().Move(2 * Time.deltaTime);

        }
        else
        {
           
                targetDirection = Vector3.zero;
            GetComponent<FigurePresenter>().Stop();
        }
        
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 2 * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        


    }

}