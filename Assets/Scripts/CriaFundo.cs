using UnityEngine;
using System.Collections;

public class CriaFundo : MonoBehaviour {

	public GameObject chaoMadeira,
					  fundoMadeira,
					  maincamera,
                      chaoInicial;
    private float alturaUltimoBloco, alturaCamera;
    private float tamanhoFundo;

    private void Start()
    {
        tamanhoFundo = fundoMadeira.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y*1.48f;
        alturaUltimoBloco = chaoInicial.transform.position.y;
    }

	void Update () {
        alturaCamera = maincamera.gameObject.GetComponent<Camera>().orthographicSize + maincamera.transform.position.y;
        GeraFundo();
    }

    private void GeraFundo() {
        int randNumber = Random.Range(0, 10);
        if (alturaCamera >= alturaUltimoBloco) {
            GameObject temp = Instantiate(fundoMadeira, new Vector3(0, alturaUltimoBloco + tamanhoFundo, 0), Quaternion.identity);
            temp.GetComponent<Animator>().SetInteger("porcentagem",randNumber);
            alturaUltimoBloco = alturaUltimoBloco + tamanhoFundo;
        }
    }
}
