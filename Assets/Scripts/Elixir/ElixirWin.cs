using UnityEngine;
using UnityEngine.SceneManagement;

public class ElixirWin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync("WinScene");
        }
    }
}
