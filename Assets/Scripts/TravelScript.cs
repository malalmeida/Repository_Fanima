using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TravelScript : MonoBehaviour
{
    public Animator animator;
    private bool finish;
    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;
    public GameObject cloud4;
    public int cloudsRemoved = 0;


    // Start is called before the first frame update
    void Start()
    {
       cloud1.SetActive(true);
       cloud2.SetActive(true);
       cloud3.SetActive(true);
       cloud4.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(cloudsRemoved == 4)
        {
            animator.SetBool("finish", true);

            SceneManager.LoadScene("Frog");
        }
    }

    public void RemoveCloud1(){
        cloud1.SetActive(false);
        cloudsRemoved ++;
    }

    public void RemoveCloud2(){
        cloud2.SetActive(false);
        cloudsRemoved ++;
    }

    public void RemoveCloud3(){
        cloud3.SetActive(false);
        cloudsRemoved ++;
    }

    public void RemoveCloud4(){
        cloud4.SetActive(false);
        cloudsRemoved ++;
    }
}
