using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reloader : MonoBehaviour
{
    public float Delay;
    public string TargetScene;

    IEnumerator Start ()
    {
        yield return new WaitForSeconds(Delay);

        while (true)
        {
            if (Input.anyKey)
                SceneManager.LoadScene(TargetScene);
            
            yield return null;
        }
    }
}
