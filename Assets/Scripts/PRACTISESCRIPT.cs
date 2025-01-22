using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PRACTISESCRIPT : MonoBehaviour
{
    //TODO this Script is only used to test new functions in Scene 1
   
    
    // public Image guide;
    public SpriteRenderer guideSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.instance != null )
        {
            //guide.sprite = DataManager.instance.maleGuide;
            guideSprite.sprite = DataManager.instance.characterGuide;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
