using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    public int move_up = 1;
    public int move_down = -1;
    public int move_left  = -1;
    public int move_right  = 1;


    void Update()
    {
        //Move the player with w, a, s, and d
        if (Input.GetKey("w"))
        {
            transform.position += new Vector3(0, move_up, 0) * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.position += new Vector3(move_left, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            transform.position += new Vector3(0, move_down, 0) * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            transform.position += new Vector3(move_right, 0, 0) * Time.deltaTime;
        }
        
    }
}
