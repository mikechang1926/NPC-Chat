using UnityEngine;

public class NPCProximityDetector : MonoBehaviour
{
    public string playerTag = "Player";

    private RandomWander wanderScript;
    private Backstory backstory;
    private ChatUIController chatUI;
    private bool playerNearby = false;

    void Start()
    {
        wanderScript = GetComponent<RandomWander>();
        backstory = GetComponent<Backstory>();
        chatUI = FindObjectOfType<ChatUIController>();

        if (chatUI == null)
        {
            Debug.LogError("ChatUIController not found in the scene.");
        }
    }

    void Update()
    {
        if (playerNearby && chatUI != null && chatUI.IsVisible() && Input.GetKeyDown(KeyCode.E))
        {
            chatUI.ShowStory();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerNearby = true;

            if (wanderScript != null)
                wanderScript.SetIdle(true);

            if (chatUI != null)
            {
                string story = backstory != null ? backstory.GetStory() : "This warrior’s tale is a mystery.";
                chatUI.SetStory(story);
                chatUI.ShowPrompt("Press E to say hi...");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerNearby = false;

            if (wanderScript != null)
                wanderScript.SetIdle(false);

            if (chatUI != null)
                chatUI.HideChat();
        }
    }
}
