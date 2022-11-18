using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchScript : MonoBehaviour
{
    CharacterController characterCollider;
    float originalHeight;
    public float reducedHeight;

    // Start is called before the first frame update
    void Start()
    {
        characterCollider = gameObject.GetComponent<CharacterController>();
        originalHeight = characterCollider.height;
    }

    // Update is called once per frame
    void Update()
    {
        //Crouch
        if (Input.GetKeyDown(KeyCode.C))
            Crouch();
        else if (Input.GetKeyUp(KeyCode.C))
            GoUp();
    }

    // Method to reduce height

    void Crouch()
    {
        characterCollider.height = reducedHeight;
    }

    // Method to reset height

    void GoUp()
    {
        characterCollider.height = originalHeight;
    }
}
