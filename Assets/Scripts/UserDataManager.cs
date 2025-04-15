using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages basic user-related operations such as logging in with a username,
/// storing user data via PlayerPrefs, and displaying relevant UI panels.
/// </summary>
/// <Author>Play2Make</Author>
public class UserDataManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField nameInput;             // Input field where the user types their name
    public GameObject keyboardPanel;             // Virtual keyboard panel for name input
    public GameObject sceneSelectionPanel;       // Panel for selecting scenes after login
    public TMP_Text playerName;                  // Text field displaying the current player name

    private const string USER_NAME_KEY = "UserName";     // Key to store the username in PlayerPrefs
    private const string USER_LOGGED_KEY = "UserLogged"; // Key to track if the user has already logged in

    void Start()
    {
        PlayerPrefs.DeleteAll(); // Clear all saved data (likely for testing or development purposes)

        // Set initial player name text based on saved username
        playerName.text = "Jogador Atual: " + PlayerPrefs.GetString(USER_NAME_KEY);

        // Check login status and open the appropriate UI
        CheckLoginStatus();
    }


    /// Verifies if the user has previously logged in and updates the UI accordingly.
    private void CheckLoginStatus()
    {
        if (PlayerPrefs.GetInt(USER_LOGGED_KEY, 0) == 1)
        {
            // User already logged in - show scene selection panel and hide keyboard
            sceneSelectionPanel.SetActive(true);
            keyboardPanel.SetActive(false);
            playerName.text = "Jogador Atual: " + PlayerPrefs.GetString(USER_NAME_KEY);
            Debug.Log("User already logged in. Name: " + PlayerPrefs.GetString(USER_NAME_KEY));
        }
        else
        {
            // User not logged in - show keyboard panel for name input
            keyboardPanel.SetActive(true);
            sceneSelectionPanel.SetActive(false);
            Debug.Log("User not logged in. Showing keyboard...");
        }
    }

    /// Called when the user submits their name. Validates input and saves data.
    public void SaveUserName()
    {
        string userName = nameInput.text.Trim();

        if (string.IsNullOrEmpty(userName))
        {
            Debug.LogWarning("User name cannot be empty!");
            ShowNameWarning();
            return;
        }

        // Save the name and mark user as logged in
        PlayerPrefs.SetString(USER_NAME_KEY, userName);
        PlayerPrefs.SetInt(USER_LOGGED_KEY, 1);
        PlayerPrefs.Save();

        Debug.Log("User name saved: " + userName);

        // Update UI to show scene selection panel
        keyboardPanel.SetActive(false);
        sceneSelectionPanel.SetActive(true);
        playerName.text = "Jogador Atual: " + PlayerPrefs.GetString(USER_NAME_KEY);
    }

    /// Displays a warning message when the input name is invalid.
    private void ShowNameWarning()
    {
        // This is where you can display a UI warning panel or popup
        Debug.LogWarning("Please enter a valid name.");
    }
}
