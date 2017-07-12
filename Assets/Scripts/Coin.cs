using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
    [SerializeField] float speed,time;
    private bool up;

    private void Start(){
        time = time*Random.Range(1f,1.2f);
        StartCoroutine( UpAndDown() );
        StartCoroutine( UpState() );
    }
    public void destroyCoin()
    {
        StartCoroutine( Smaller() );
    }
    private IEnumerator Smaller(){
        while(transform.localScale.x > 0.05f){
            transform.localScale = transform.localScale*0.9f;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }
    private IEnumerator UpAndDown(){
        float direction = 1;

        while(true){
            if(up){direction = 1;}
            else{direction = -1;}
            transform.position = transform.position + Vector3.up*direction*speed*Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
    }
    private IEnumerator UpState(){
        while(true){
            yield return new WaitForSeconds(time);
            up = !up;
        }
    }
}
