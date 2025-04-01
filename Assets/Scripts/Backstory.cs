using UnityEngine;

public class Backstory : MonoBehaviour
{
    [TextArea(3, 10)]
    public string backstory;

    public string GetStory()
    {
        return string.IsNullOrEmpty(backstory) ? "This warrior’s story is yet untold." : backstory;
    }
}
