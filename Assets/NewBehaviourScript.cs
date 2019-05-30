using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour {

    public void ChangeSccene(string SceneTo)
    {
        SceneManager.LoadScene(SceneTo);
    }

}
