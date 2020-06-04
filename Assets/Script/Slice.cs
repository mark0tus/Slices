using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{
    private bool canMove = false;
    private Vector3 targetPos;
    private Transform parentTransform;

    void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.18f);        //Makes the Slice move towards the empty position
            if (Vector3.Distance(transform.position, targetPos) < 0.05f)        //If the distance between the target and the Slice is less than X, then stops movement
                StopMovement();
        }
    }


    public void Move(Vector3 target, Transform transf)
    {
        parentTransform = transf;
        targetPos = target;
        canMove = true;
    }

    private void StopMovement()
    {
        canMove = false;
        transform.position = targetPos;
        transform.SetParent(parentTransform);
        FindObjectOfType<Score>().IncrementScore(transform.childCount);      //Increments score
        parentTransform.gameObject.GetComponent<Plate>().isActive = true;
        parentTransform.gameObject.GetComponent<Plate>().CheckFullSlices();
        transform.localScale = new Vector3(1f, 1f, 1f);
        FindObjectOfType<MidPlate>().Spawn();     //Spawns a new slice
    }
}