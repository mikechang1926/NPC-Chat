using UnityEngine;
using TMPro;

public class ChatUIController : MonoBehaviour
{
    public GameObject chatPanel;
    public TMP_Text chatText;

    private string pendingStory = "";

    public void ShowPrompt(string promptText)
    {
        chatPanel.SetActive(true);
        chatText.text = promptText;
    }

    public void SetStory(string story)
    {
        pendingStory = story;
    }

    public void ShowStory()
    {
        if (!string.IsNullOrEmpty(pendingStory))
        {
            chatText.text = pendingStory;
        }
    }

    public void HideChat()
    {
        chatPanel.SetActive(false);
        pendingStory = "";
    }

    public bool IsVisible()
    {
        return chatPanel.activeSelf;
    }
}
