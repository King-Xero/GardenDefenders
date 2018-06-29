using System;
using UnityEngine;

public class QuitMenuController : MonoBehaviour
{
    public void ShowQuitMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseQuitMenu()
    {
        gameObject.SetActive(false);
    }
}