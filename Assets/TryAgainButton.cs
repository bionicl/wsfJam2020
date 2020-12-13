using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TryAgainButton : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TryAgain);
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
