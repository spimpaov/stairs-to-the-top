using UnityEngine;
using System.Collections;

public class Agua : MonoBehaviour {

    public Player player;
    public float maxSpeed;
    public float minSpeed_Start;
    public float minSpeed_End;
    public float MaximumDistance;
    public float MinimumDistance;
    public float waterSpeed;
    public float timeNextMinSpeed;

    private float timeLeftMinSpeed;
	
    void Start() {
        timeLeftMinSpeed = timeNextMinSpeed;
    }

	void Update () {
		Sobe();
	}

	void Sobe(){

        timeLeftMinSpeed -= Time.deltaTime;
        if (timeLeftMinSpeed < 0)
        {
            minSpeed_Start += 0.1f;
            if (minSpeed_Start > minSpeed_End) { minSpeed_Start = minSpeed_End; }
            timeLeftMinSpeed = timeNextMinSpeed;
        }


        var dist = Vector3.Distance(this.transform.position, player.transform.position);

        if (dist > MaximumDistance)
        {
            waterSpeed = maxSpeed;
        }
        else if (dist < MinimumDistance)
        {
            waterSpeed = minSpeed_Start;
        }
        else
        {
            var distRatio = (dist - MinimumDistance) / (MaximumDistance - MinimumDistance);
            var diffSpeed = maxSpeed - minSpeed_Start;
            waterSpeed = (distRatio * diffSpeed) + minSpeed_Start;
        }
        this.transform.position += new Vector3(0, waterSpeed * Time.deltaTime, 0);
    }
}
