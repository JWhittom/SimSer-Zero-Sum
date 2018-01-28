using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateManager : MonoBehaviour 
{
    #region SerializeFields - for assignment in the editor
    // Reference to check button to change text on completion
    [SerializeField] Button checkButton;
    // Data matrix the user interacts with
    [SerializeField] RectTransform matrix;
    // Panel to disable after task is complete
    [SerializeField] Image panel;
    // End manager to enable on completion
    [SerializeField] GameObject endManager;
    #endregion

    #region Private fields
    // Array of correct/incorrect outcomes, used to determine highlight color
    // Order: (A, a), (A, b), (B, a), (B, b)
    bool[] outcomes = new bool[4];
    // Highlight color for correct outcomes - light green
    Color correct = new Color32(0x85, 0xfc, 0xa9, 0xff);
    // Highlight color for incorrect outcomes - light red
    Color incorrect = new Color32(0xfc, 0x87, 0x85, 0xff);
    // Reference to active element of outcome matrix
    GameObject activeResult;
    #endregion

    // Determine whether payoffs in an outcome add to 0, return true if so, else false
    bool CheckValues(Text firstPlayer, Text secondPlayer)
    {
        // Convert the values in the text elements to ints
        // The input fields are set to only allow integers
        int firstInt = int.Parse(firstPlayer.text);
        int secondInt = int.Parse(secondPlayer.text);

        // Determine whether the values add up to 0 and return accordingly
        if (firstInt + secondInt == 0)
            return true;
        else
            return false;
    }

    // Determine whether or not all outcomes are correct, return true if all are, else false
    bool Finished()
    {
        // Run through each element in the outcomes array, return false if any are false
        foreach (bool i in outcomes)
            if (!i)
                return false;
        // If it gets here all outcomes are correct, return true
        return true;
    }

    #region Public functions - for buttons
    // Check each result to determine accuracy
    public void CheckButton()
    {
        if (!Finished())
        {
            // Count to track place in the outcome array
            int place = 0;

            // Loop through each result, using characters
            // Characters used instead of integers to avoid setting and resetting more variables
            for (char i = 'A'; i < 'C'; i++)
            {
                for (char j = 'a'; j < 'c'; j++)
                {
                    // Get each outcome element in the matrix, using the name format X_x
                    activeResult = matrix.Find(string.Format("{0}_{1}", i, j)).gameObject;

                    // Compare the values in the outcome element, determine whether they fit a zero-sum specification
                    outcomes[place] = CheckValues(activeResult.transform.Find(string.Format("P1_{0}", i)).Find("Text").GetComponent<Text>(),
                                                  activeResult.transform.Find(string.Format("P2_{0}", j)).Find("Text").GetComponent<Text>());
                    // Highlight the active cell
                    if (outcomes[place])
                        activeResult.GetComponent<Image>().color = correct;
                    else
                        activeResult.GetComponent<Image>().color = incorrect;
                    // Move to the next element of the array
                    place++;
                }
            }
            // Change the check button text if the results are satisfactory
            if (Finished())
                checkButton.GetComponentInChildren<Text>().text = "Continue";
        }
        else
        {
            endManager.GetComponent<EndManager>().active = true;
            panel.gameObject.SetActive(false);
        }
    }
    #endregion
}
