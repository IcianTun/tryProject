using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public List<GameObject> actionTemplates;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateActions();
        }

    }

    void GenerateActions()
    {

    }
}
