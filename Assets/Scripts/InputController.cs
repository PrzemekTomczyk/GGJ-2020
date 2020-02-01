using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Test");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 330; i < 349; i++)
        {
            // TEST TO GET CONTROLLER INFO
            //string keycode = "KeyCode.JoystickButton" + i;
            if (Input.GetKey((KeyCode)i))
            {
                print(i.ToString());
            }

        }
    }

    void onClick(int playerNumber)
    {

    }
}
