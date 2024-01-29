using UnityEngine;

public class AuthorsURL : MonoBehaviour
{
    public void OpenUrl(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
    }
}
