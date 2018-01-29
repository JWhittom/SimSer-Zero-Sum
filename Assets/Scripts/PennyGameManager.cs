using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PennyGameManager : MonoBehaviour
{
    // Play button
    [SerializeField] Button playButton;
    // Graphic representation of the player's penny
    [SerializeField] Image playerPenny;
    // Reference to panel to deactivate when necessary
    [SerializeField] Image activePanel;
    // Starting penny count
    [SerializeField] int startingPennies;
    // Panel for the body of the results matrix
    [SerializeField] RectTransform resultsTable;
    // Text field showing the number of pennies the player has
    [SerializeField] Text playerPennyCountText;
    // Text field showing the number of pennies the opponent has
    [SerializeField] Text rivalPennyCountText;
    // Text field showing the face of the opponent's penny
    [SerializeField] Text rivalPennyFace;
    
    // Reference to active matrix default color
    Color resultDefaultColor = new Color32(0x8c, 0xbc, 0xbe, 0xff);
    // Matrix highlight color - the same light turquoise as the buttons
    Color highlight = new Color32(0xBC, 0xFD, 0xFF, 0xFF);
    // Reference to active matrix element
    GameObject activeResult;
    // Player penny count
    int playerPennyCount;
    // Opponent penny count
    int rivalPennyCount;
    // Player penny face
    string playerPennyString;
    // Rival penny face
    string rivalPennyString;
    // Text field showing the face of the player's penny
    Text playerPennyFace;

    // Use this for initialization
    void Start ()
    {
        // Initialize Random
        Random.InitState((int)Time.time);
        // Retrieve reference to player penny text from player penny image
        playerPennyFace = playerPenny.GetComponentInChildren<Text>();
        // Set starting penny counts
        playerPennyCount = startingPennies;
        rivalPennyCount = startingPennies;
        // Display starting penny counts
        SetPennyCountText();
    }
	
    // Update the penny count text lines
	void SetPennyCountText ()
    {
        playerPennyCountText.text = string.Format("Pennies: {0}", playerPennyCount);
        rivalPennyCountText.text = string.Format("Pennies: {0}", rivalPennyCount);
    }

    // Set opponent's coin value
    void RivalCoinFlip()
    {
        // Randomly select a number between 1 and 100
        int rand = Mathf.RoundToInt(Random.Range(1, 100));
        // If even, pick heads, if odd, tails
        if (rand % 2 == 0)
            rivalPennyString = "Heads";
        else
            rivalPennyString = "Tails";
        rivalPennyFace.text = rivalPennyString;
    }
    
    // Flip the player coin
    public void Flip_Button()
    {
        StartCoroutine(CoinFlip());
    }

    // Play the round
    public void Play_Button()
    {
        // Ensure that the game is still in progress
        if (rivalPennyCount > 0 && playerPennyCount > 0)
        {
            // Reset colors of result matrix
            if (activeResult != null)
                activeResult.GetComponent<Image>().color = resultDefaultColor;
            // Check the opponent's coin
            RivalCoinFlip();
            // Compare values
            // If the coins match, the player takes a coin from the opponent
            if (rivalPennyFace.text == playerPennyFace.text)
            {
                rivalPennyCount--;
                playerPennyCount++;
            }
            // If the coins don't match, the opponent takes a coin from the player
            else
            {
                rivalPennyCount++;
                playerPennyCount--;
            }
            // Highlight the corresponding entry in the results matrix
            activeResult = resultsTable.Find(string.Format("{0}_{1}", playerPennyString, rivalPennyString)).gameObject;
            activeResult.GetComponent<Image>().color = highlight;
            // Update penny count text
            SetPennyCountText();
            // Change play button text if the game has ended
            if (rivalPennyCount <= 0 || playerPennyCount <= 0)
                playButton.GetComponentInChildren<Text>().text = "Continue";
        }
        // Disable panel and move on after game is completed
        else
            activePanel.gameObject.SetActive(false);
    }
    
    // Play the coin flip animation and update text to match
    IEnumerator CoinFlip()
    {
        // Set text for the penny face
        if (playerPennyFace.text != "Tails")
            playerPennyString = "Tails";
        else
            playerPennyString = "Heads";
        // Clear the face text while the coin flips
        playerPennyFace.text = "";
        // Tell the animator to play the coin flip animation
        playerPenny.GetComponent<Animator>().SetBool("shouldFlip", true);
        // Pause a moment to let the coin flip animation play
        yield return new WaitForSeconds(0.5f);
        playerPennyFace.text = playerPennyString;
        playerPenny.GetComponent<Animator>().SetBool("shouldFlip", false);
    }
}
