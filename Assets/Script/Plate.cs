using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public GameObject[] neighbours;

    private Transform mainCircleTransform;
    private Vector3 startPosition;
    private int fullSlicesCount;

    [HideInInspector]
    public bool isActive = true;

    [HideInInspector]
    public bool[] isEmpty;

    void Start()
    {
        startPosition = transform.position;
        isEmpty = new bool[6];
        for (int i = 0; i < 6; i++)     //Each slice is empty
            isEmpty[i] = true;

        mainCircleTransform = GameObject.FindGameObjectWithTag("MainCircle").GetComponent<Transform>();
    }

    private void OnMouseDown()
    {
        {
            Transform tempSlice = mainCircleTransform.GetChild(1);      //Gets the active Slice
            int num = (int)tempSlice.eulerAngles.z;
            if (num < 0)
                num = 360 + num;

            int pos = num / 60; 

            for (int i = 0; i < tempSlice.childCount; i++)
            {
                if (!isEmpty[pos])      //If the position is not empty, then the check is over
                    return;

                if (pos + 1 < 6)
                    pos++;
                else
                    pos = 0;
            }

            pos = num / 60;

            for (int i = 0; i < tempSlice.childCount; i++)
            {
                isEmpty[pos] = false;

                if (pos + 1 < 6)
                    pos++;
                else
                    pos = 0;
            }
            fullSlicesCount += tempSlice.childCount;

            tempSlice.transform.SetParent(Camera.main.transform);
            tempSlice.gameObject.GetComponent<Slice>().Move(startPosition, transform);       //Starts to move the Slice towards the empty position

            isActive = false;
        }
    }

    public void ResetSlices(bool canScore = true)
    {

        for (int i = 4; i < transform.childCount; i++)
        {
            //transform.GetChild(i).gameObject.GetComponent<Slice>().SpawnParticle(canScore);     //Spawns 'DestroyParticle'
            Destroy(transform.GetChild(i).gameObject, 0.1f);        //Destroys the Slice
        }

        for (int i = 0; i < 6; i++)     //Each slice is empty
            isEmpty[i] = true;
        fullSlicesCount = 0;        //There are no full slices

        
    }

    private void ResetNeighbours()
    {
        neighbours[0].GetComponent<Plate>().ResetSlices();     //Resets first neighbour
        neighbours[1].GetComponent<Plate>().ResetSlices();     //Resets second neighbour
        FindObjectOfType<GameManager>().StartButton();
    }

    public void CheckFullSlices()
    {
        if (fullSlicesCount == 6)
        {
            ResetSlices();
            ResetNeighbours();
        }
    }
}