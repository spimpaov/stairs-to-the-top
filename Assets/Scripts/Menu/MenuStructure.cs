using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStructure : MonoBehaviour {

	[SerializeField] private List<GameObject> menuList;
	private int actualMenu = 0;
	[SerializeField] private float transitionSpeed;
	[SerializeField] private Background myBG;

	private IEnumerator TransitionLeft(){
		RectTransform menuRT = menuList[actualMenu].GetComponent<RectTransform>();
		Vector2 originalPosition = menuRT.anchoredPosition;
		Vector2 targetPositionLeft = originalPosition - Vector2.right*1000;
		Vector2 targetPositionRight = originalPosition + Vector2.right*1000;
		myBG.Change();
		while(menuRT.anchoredPosition != targetPositionLeft){
			menuRT.anchoredPosition = Vector2.MoveTowards(menuRT.anchoredPosition,targetPositionLeft,transitionSpeed);
			yield return new WaitForEndOfFrame();
		}
		ChangeMenu(1);
		menuRT = menuList[actualMenu].GetComponent<RectTransform>();
		menuRT.anchoredPosition = targetPositionRight;

		while(menuRT.anchoredPosition != originalPosition){
			menuRT.anchoredPosition = Vector2.MoveTowards(menuRT.anchoredPosition,originalPosition,transitionSpeed);
			yield return new WaitForEndOfFrame();
		}
		
	}
	private IEnumerator TransitionRight(){
		myBG.Change();
		RectTransform menuRT = menuList[actualMenu].GetComponent<RectTransform>();
		Vector2 originalPosition = menuRT.anchoredPosition;
		Vector2 targetPositionLeft = originalPosition - Vector2.right*1000;
		Vector2 targetPositionRight = originalPosition + Vector2.right*1000;

		while(menuRT.anchoredPosition != targetPositionRight){
			menuRT.anchoredPosition = Vector2.MoveTowards(menuRT.anchoredPosition,targetPositionRight,transitionSpeed);
			yield return new WaitForEndOfFrame();
		}
		ChangeMenu(-1);
		menuRT = menuList[actualMenu].GetComponent<RectTransform>();
		menuRT.anchoredPosition = targetPositionLeft;

		while(menuRT.anchoredPosition != originalPosition){
			menuRT.anchoredPosition = Vector2.MoveTowards(menuRT.anchoredPosition,originalPosition,transitionSpeed);
			yield return new WaitForEndOfFrame();
		}
		
	}
	private void ChangeMenu(int quant){
		menuList[actualMenu].SetActive(false);

		actualMenu += quant;
		
		if(actualMenu < 0){
			actualMenu = menuList.Count - 1;
		} else if (actualMenu == menuList.Count){
			actualMenu = 0;
		}

		menuList[actualMenu].SetActive(true);
	}
	public void MenuController(string dir){
		switch(dir){
			case "left":
				StartCoroutine(TransitionRight());
				break;
			case "right":
				StartCoroutine(TransitionLeft());
				break;
		}
	}
}
