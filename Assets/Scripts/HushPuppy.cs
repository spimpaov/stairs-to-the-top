using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HushPuppy : MonoBehaviour {

    public void LoadScene(string sceneName){
        GameObject pm = GameObject.FindGameObjectWithTag("PauseManager");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (pm != null) pm.GetComponent<PauseManager>().pressionado = false;
        if (player != null) player.GetComponent<Player>().paused = false;
        SceneManager.LoadScene(sceneName);
    }
	public void LoadSceneWithBiggerTransition(string name){
		StartCoroutine( BiggerTransition() );
	}
	private IEnumerator BiggerTransition(){
		GameObject.Find("Transition_Mask").GetComponent<TransitionMask>().Bigger_transition();
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(name);
	}
}
