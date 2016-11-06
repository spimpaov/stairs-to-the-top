using UnityEngine;
using System.Collections;

public class CriaFundo : MonoBehaviour {

	public GameObject fundoMadeira,
					  fundoPontilhado,
					  maincamera;

	private int i = 0,//Contador para instanciaFundo (Madeira)
				j = 0;//Contador para instanciaPontilhado (Pontilhado)

	void Update () {
		instanciaFundoMadeira ();
		instanciaFundoPontilhado ();
	}
		
	private void instanciaFundoMadeira(){
		if (maincamera.transform.position.y + 8 >= 10*i) {
			Instantiate (fundoMadeira, new Vector3 (0, 10f*i + 8f, 0), Quaternion.identity);
			i++;
		}
	}

	private void instanciaFundoPontilhado(){
		if (maincamera.transform.position.y + 8 >= 10*j) {
			Instantiate (fundoPontilhado, new Vector3 (0, 20f*j - 3f, 0), Quaternion.identity);
			j++;
		}
	}
}
