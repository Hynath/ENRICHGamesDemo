using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int gridSize = 3;//number of buttons that a single line of (n x n) grid will have 
    public GameObject gridButtonPrefab;//Button prefab that will be created for each index of grid
    public GameObject GridPanel;//Panel that will contain our grid
    public RectTransform Canvas;//MainCanvas
    public Text pairCountText;//Text that shows how many pairs we made 
    public InputField gridSizeInput;//inputBox that updates gridSize
    public List<GridButton> pairList = new List<GridButton>();//List that contains adjacent active buttons
    private int pairCount = 0;//Counts how many pairs we made
    private GridButton[,] gridButtonArray;//2D array that holds everyIndex of grid
    private GameObject gridParentObject;//A parent object that contains our gridButtons
    void Start()
    {
        CreateGridsObject();
        gridSizeInput.text = gridSize.ToString();
    }
    public void ChangeGridSize(string size)
    {
        gridSize = int.Parse(size);
    }
    public void CreateGrid() 
    {
        float buttonDistance = GridPanel.GetComponent<RectTransform>().rect.width / (float)gridSize;//calculate distance bettween 2 adjacent buttons
        gridButtonArray = new GridButton[gridSize, gridSize];//adjusting size of our array
        for (int i = 0; i< gridSize; i++) 
        {
            for (int l = 0; l < gridSize; l++)
            {
                GameObject newGridObject = Instantiate(gridButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);//Create GridObject
                newGridObject.name = "Grid" + i + "-" + l;//Name GridObject
                newGridObject.transform.SetParent(gridParentObject.transform);
                newGridObject.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonDistance, buttonDistance);//adjusting size 
                newGridObject.GetComponent<RectTransform>().transform.position = new Vector2((l * buttonDistance) + (buttonDistance / 2), Canvas.rect.height - ((i * buttonDistance) + (buttonDistance / 2)));//calculating and seting the possition

                newGridObject.GetComponent<GridButton>().activeSprite.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonDistance, buttonDistance);//adjusting size and position of sprite 
                newGridObject.GetComponent<GridButton>().gridPos.x = i;
                newGridObject.GetComponent<GridButton>().gridPos.y = l;
                gridButtonArray[i, l] = newGridObject.GetComponent<GridButton>();//add GridButton to the array
            }
        }
    }
    public void CreateGridsObject()//CreateParent object
    {
        gridParentObject = new GameObject();
        gridParentObject.name = "Grids";
        gridParentObject.transform.SetParent(GridPanel.transform);
        gridParentObject.AddComponent<RectTransform>();
        CreateGrid();
    }
    public void ResetGrids()//Destroy CurrentGrid and Create another one with current gridSize
    {
        Destroy(gridParentObject);
        CreateGridsObject();
        pairCount = 0;
        pairCountText.text = pairCount.ToString();
    }
    public void CheckForPairsMain(int x, int y)//called only when a button is clicked starts calculating how many active buttons are paired with this one 
    {
        pairList.Add(gridButtonArray[x, y]);

        //Check Up
        if (x - 1 >= 0)
        {
            if (gridButtonArray[x - 1, y].currentState == true)
            {
                if (!pairList.Contains(gridButtonArray[x - 1, y]))
                    CheckForPairsBranchs(x - 1, y);
            }
        }
        //Check Down
        if (x + 1 < gridSize)
        {
            if (gridButtonArray[x + 1, y].currentState == true)
            {
                if (!pairList.Contains(gridButtonArray[x + 1, y]))
                    CheckForPairsBranchs(x + 1, y);
            }
        }
        //Check Right
        if (y + 1 < gridSize)
        {
            if (gridButtonArray[x, y + 1].currentState == true)
            {
                if (!pairList.Contains(gridButtonArray[x, y + 1]))
                    CheckForPairsBranchs(x, y + 1);
            }
        }
        //Check Left
        if (y - 1 >= 0)
        {
            if (gridButtonArray[x, y - 1].currentState == true)
            {
                if (!pairList.Contains(gridButtonArray[x, y - 1]))
                    CheckForPairsBranchs(x, y - 1);
            }
        }
        CheckPairList();
    }
    public void CheckForPairsBranchs(int x, int y)
    {
        pairList.Add(gridButtonArray[x, y]);

        //Check Up
        if (x - 1 >= 0)
        {
            if (gridButtonArray[x - 1, y].currentState == true)
            {
                if (!pairList.Contains(gridButtonArray[x - 1, y]))
                    CheckForPairsBranchs(x - 1, y);
            }
        }
        //Check Down
        if (x + 1 < gridSize)
        {
            if (gridButtonArray[x + 1, y].currentState == true)
            {
                if (!pairList.Contains(gridButtonArray[x + 1, y]))
                    CheckForPairsBranchs(x + 1, y);
            }
        }
        //Check Right
        if (y + 1 < gridSize)
        {
            if (gridButtonArray[x, y + 1].currentState == true)
            {
                if (!pairList.Contains(gridButtonArray[x, y + 1]))
                    CheckForPairsBranchs(x, y + 1);
            }
        }
        //Check Left
        if (y - 1 >= 0)
        {
            if (gridButtonArray[x, y - 1].currentState == true)
            {
                if (!pairList.Contains(gridButtonArray[x, y - 1]))
                    CheckForPairsBranchs(x, y - 1);
            }
        }
    }//checks if adjacent buttons are paired  
    public void CheckPairList() //When pair checking ends checks the filled pairLists if the pair is 3 or bigger it deactivates the buttons 
    {
        if (pairList.Count >= 3)
        {
            pairCount++;
            pairCountText.text = pairCount.ToString();
            for (int i = 0; i < pairList.Count; i++)
            {
                pairList[i].activeSprite.enabled = false;
                pairList[i].currentState = false;
            }
        }
    }
}
