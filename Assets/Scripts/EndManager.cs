using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour 
{
    // Bool to ensure that Space doesn't quit the game from the beginning
    public bool active = false;

	// Update is called once per frame
	void Update () 
	{
        // Exit on Space pressed - starts inactive so a space press won't quit automatically
        if (Input.GetKeyDown(KeyCode.Space) && active)
            Application.Quit();
	}
}
