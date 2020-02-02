using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    //publi



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
                //KeyCode.JoystickButton0 // if anny press a

                if(Input.GetKey((KeyCode.Joystick1Button0)))
                {
                    print("join 1");
                }
                if (Input.GetKey((KeyCode.Joystick2Button0)))
                {
                    print("join 2");
                }
                if (Input.GetKey((KeyCode.Joystick3Button0)))
                {
                    print("join 3");
                }
                if (Input.GetKey((KeyCode.Joystick4Button0)))
                {
                    print("join 4");
                }
                //  print(i.ToString());
            }

        }
    }

    void onClick(int playerNumber)
    {

    }
}
