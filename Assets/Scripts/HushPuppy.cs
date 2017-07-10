using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HushPuppy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
	public void LoadSceneWithBiggerTransition(string name){
		StartCoroutine( BiggerTransition() );
	}
	private IEnumerator BiggerTransition(){
		GameObject.Find("Transition_Mask").GetComponent<TransitionMask>().Bigger_transition();
		yield return new WaitForSeconds(1);
		Debug.Log("OIIIII");
		SceneManager.LoadScene(name);
	}
}
