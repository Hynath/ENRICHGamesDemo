using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridButton : MonoBehaviour
{
    public struct gridPosition
    {
        public int x;
        public int y;
    }
    public gridPosition gridPos;//Holds grid coordinates
    public bool currentState = false;//Is button active or not 
    public Image activeSprite;//sprite that shown when button is active
    public void ButtonPressed() 
    {
        if (!currentState) 
        {
            activeSprite.enabled = true;
            currentState = !currentState;
            FindObjectOfType<GameManager>().pairList.Clear();//clear previous pair list
            FindObjectOfType<GameManager>().CheckForPairsMain(gridPos.x, gridPos.y);//start Checking pairs
        }
    }
}
