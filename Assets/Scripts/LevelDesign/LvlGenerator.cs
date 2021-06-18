using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlGenerator : MonoBehaviour
{
    
    public GameObject LvlButtonPrefab;
    public GameObject LvlPanel;
    public Vector2 StartPos;
    public int LvlCount;
    public GameObject Back;
    public ScrollRect Scroll;
    int direction = 1;
    Vector2 currentPos;
    // Start is called before the first frame update
    void Awake()
    {
        LvlActivate.LvlButtons.Clear();

        Generator();

        Scroll.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
        


    }
     
    public void Generator()
        {
   
        for (int i = 0; i < LvlCount/5; i++)
        {
           GenerateBack(i);
           
        }
       
       
        
    }


    
    public void GenerateBack(int numberofback)
    {
        GameObject back = Instantiate(Back, this.transform, false) as GameObject;
        back.GetComponent<RectTransform>().parent = LvlPanel.GetComponent<RectTransform>();
        back.GetComponent<Back>().Number = numberofback;
        back.GetComponent<Back>().GenerateBack();
        LvlActivate.LvlButtons.AddRange(back.GetComponent<Back>().GetListLevel());

        LvlActivate.ActivateLvlMethod(SceneDataTranslator.LvlNumber);
        /*  for (int i = 0; i < 5; i++)
          {
              Vector3 pos;
              if (currentPos.x == -300)
              {
                  direction = 1;

              }
              else if (currentPos.x == 300)
              {
                  direction = -1;
              }


              pos = currentPos + new Vector2(direction * 150, 75);
              currentPos = pos;
              GenerateLvl(currentPos, back);

          } */

    }

    public void GenerateLvl(Vector3 pos,GameObject back)
    {
        GameObject lvlbutton = Instantiate(LvlButtonPrefab, this.transform, false) as GameObject;
        Vector2 worldPos = lvlbutton.transform.position;
        lvlbutton.transform.parent = back.transform;

        lvlbutton.transform.localPosition = pos;
        
        
        //lvlbutton.transform.position = worldPos;
       // LvlButtons.Add(lvlbutton);
    }
    
}
