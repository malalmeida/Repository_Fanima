using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HomeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Frog");
    }

}
