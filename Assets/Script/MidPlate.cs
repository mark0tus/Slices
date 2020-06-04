using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidPlate : MonoBehaviour
{
    public GameObject[] slices;
    public Transform nextSpawnPos;
    public Color nextSliceColor;

    private Sprite skin;
    private GameObject tempSlice, nextSlice;

    void Start()
    {
        skin = FindObjectOfType<GameManager>().skins[PlayerPrefs.GetInt("Skin", 0)];
        nextSlice = Instantiate(slices[Random.Range(0, slices.Length)], nextSpawnPos.position, Quaternion.identity);       //Spawns a slice
        for (int i = 0; i < nextSlice.transform.childCount; i++)
            nextSlice.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = skin;        //Changes its skin

        Spawn();
    }

    public void Spawn()
    {
        tempSlice = Instantiate(nextSlice, transform);       //Spawns the clone of the 'nextSlice'
        tempSlice.transform.position = transform.position;      //Its position is reseted
        for (int i = 0; i < tempSlice.transform.childCount; i++)
            tempSlice.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = Color.white;      //Changes its color

        Destroy(nextSlice);

        nextSlice = Instantiate(slices[Random.Range(0, slices.Length)], nextSpawnPos.position, Quaternion.identity);       //Spawns a 'nextSlice'
        for (int i = 0; i < nextSlice.transform.childCount; i++)
        {
            nextSlice.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = skin;        //Changes its skin
            nextSlice.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = nextSliceColor;       //Changes its color
        }
        nextSlice.GetComponent<Animation>().Stop();
        nextSlice.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);     //Scales it down
        int randomRot = Random.Range(0, 6);     //Selects a random count of rotations
        nextSlice.transform.Rotate(Vector3.forward, 60f * randomRot);       //Rotates the slice

        CheckSpace();       //Checks if the slice can be placed
    }

    private void CheckSpace()
    {
        bool canFitIn = true;
        int num = (int)tempSlice.transform.eulerAngles.z;
        if (num < 0)
            num = 360 + num;

        foreach (GameObject circle in GameObject.FindGameObjectsWithTag("Circle"))
        {
            int pos = num / 60;

            for (int i = 0; i < tempSlice.transform.childCount; i++)
            {
                if (!circle.GetComponent<Plate>().isEmpty[pos])
                    canFitIn = false;

                if (pos + 1 < 6)
                    pos++;
                else
                    pos = 0;
            }
            if (canFitIn == true)
                return;
            canFitIn = true;
        }

        FindObjectOfType<GameManager>().EndPanelActivation();       //Game is over
    }
}
