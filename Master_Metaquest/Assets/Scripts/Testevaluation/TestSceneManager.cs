using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TesSceneManager : MonoBehaviour
{
    [SerializeField] private MethodManager methodManagerRef;
    [SerializeField] private string nextScene;
    [SerializeField] public bool useMethodNear;
    [SerializeField] public bool useMethodFar;

    public void LoadScene()
    {
        methodManagerRef.useMethodNear = useMethodNear;
        methodManagerRef.useMethodFar = useMethodFar;
        SceneManager.LoadScene(nextScene);
    }
}
