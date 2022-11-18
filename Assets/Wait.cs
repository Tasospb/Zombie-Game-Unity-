using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{
    public float wait_time = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait_for_cutscene());
    }

    IEnumerator Wait_for_cutscene()
    {
        yield return new WaitForSeconds(wait_time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
