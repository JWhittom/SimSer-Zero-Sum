using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour 
{
    // Reference to panel to disable
    [SerializeField] Image advancePanel;
	
	// Update is called once per frame
	void Update () 
	{
        // Deactivate panel and all children if space is pressed - one time use
        if (Input.GetKeyDown(KeyCode.Space))
        {
            advancePanel.gameObject.SetActive(false);
        }
	}
}
