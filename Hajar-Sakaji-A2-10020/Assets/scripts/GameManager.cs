using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject objectiveMessage; // Parent object that holds the message panel and text
    public int totalCans = 5; // The total number of cans that need to be recycled
    private int cansRecycled = 0; // Counter for how many cans have been recycled

    void Start()
    {
        // Display the initial objective message when the game starts
        ShowMessage("Objective: Pick up all the cans and recycle them! (11 cans)");

        // Automatically hide the message after 5 seconds
        Invoke("HideMessage", 5f);
    }

    public void CanRecycled()
    {
        // Increment the count of recycled cans
        cansRecycled++;

        // Check if all cans have been recycled
        if (cansRecycled >= totalCans)
        {
            // Display a success message
            ShowMessage("Great job! You recycled all the cans!");

            // Automatically hide the message after 5 seconds
            Invoke("HideMessage", 5f);
        }
    }

    void ShowMessage(string message)
    {
        // Log the message being shown
        Debug.Log("Showing message: " + message);

        // Activate the message panel and update the text
        objectiveMessage.SetActive(true);
        objectiveMessage.GetComponentInChildren<TextMeshProUGUI>().text = message;
    }

    void HideMessage()
    {
        // Log that the message is being hidden
        Debug.Log("Hiding message and panel");

        // Hide the message panel
        objectiveMessage.SetActive(false);
    }
}
