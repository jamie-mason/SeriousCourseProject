using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    
    public void ChangeScene(string sceneName){
        SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
    }
    public void ChangeScene(int sceneID){
        SceneManager.LoadScene(sceneID,LoadSceneMode.Single);
    }
    public void ReloadCurrentScene(){
        string thisScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScene,LoadSceneMode.Single);
    }
    
    

}
