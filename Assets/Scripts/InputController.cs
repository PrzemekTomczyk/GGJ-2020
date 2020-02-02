using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_player1;
    public GameObject m_player2;
    public GameObject m_player3;
    public GameObject m_player4;



    void Start()
    {
        print("Test");
    }

    // Update is called once per frame
    void Update()
    {
      //  for (int i = 330; i < 349; i++)
        {
            // TEST TO GET CONTROLLER INFO
            //string keycode = "KeyCode.JoystickButton" + i;
            //if (Input.GetKey((KeyCode)i))
            {
                //KeyCode.JoystickButton0 // if anny press a

                if(Input.GetKey((KeyCode.Joystick1Button0)))
                {
                    print("join 1");
                    m_player1.GetComponent<Image>().color = new Color(0, 0, 0,255);
                }
                if (Input.GetKey((KeyCode.Joystick2Button0)))
                {
                    print("join 2");
                    m_player2.GetComponent<Image>().color = new Color(0, 0, 0, 255);

                }
                if (Input.GetKey((KeyCode.Joystick3Button0)))
                {
                    print("join 3");
                    m_player3.GetComponent<Image>().color = new Color(0, 0, 0, 255);

                }
                if (Input.GetKey((KeyCode.Joystick4Button0)))
                {

                    print("join 4");
                    m_player4.GetComponent<Image>().color = new Color(0, 0, 0, 255);

                }


                if (Input.GetKey((KeyCode.Joystick1Button1)))
                {
                    print("join 1");
                    m_player1.GetComponent<Image>().color = new Color(255, 255,255, 255);
                }
                if (Input.GetKey((KeyCode.Joystick2Button1)))
                {
                    print("join 2");
                    m_player2.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                }
                if (Input.GetKey((KeyCode.Joystick3Button1)))
                {
                    print("join 3");
                    m_player3.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                }
                if (Input.GetKey((KeyCode.Joystick4Button1)))
                {

                    print("join 4");
                    m_player4.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                }


                if (Input.GetKey((KeyCode.Joystick1Button7)))
                {

                    print("start game");
                    SceneManager.LoadScene("Przemek_Sandbox", LoadSceneMode.Single);

                }


            }

        }
    }

    void onClick(int playerNumber)
    {

    }
}
