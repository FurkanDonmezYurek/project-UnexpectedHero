using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroButton : MonoBehaviour
{
    public GameObject[] images;
    public int i;
    public int end;

    private void Update() {
        if(i>=end){
            this.gameObject.SetActive(false);
        }
    }
    public void Click(){
        i++;
        images[i-1].SetActive(false);
        images[i].SetActive(true);
    }
}
