using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowmo : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0.09f;
        }
    }
}
