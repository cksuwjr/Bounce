using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UImanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BtnClick()
    {
        SceneManager.LoadScene("UI2");
    }
    public void BtnClickFirstWorld()
    {
        SceneManager.LoadScene("1Stage");
    }
    public void BtnClickSecondWorld()
    {
        SceneManager.LoadScene("6Stage");
    }
}

