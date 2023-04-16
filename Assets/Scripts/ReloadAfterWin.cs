using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadAfterWin : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("TestMaya");
    }
}
