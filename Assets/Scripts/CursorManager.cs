	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CursorManager : MonoBehaviour
{
	private bool uiOn = true;
	[SerializeField] private GameObject animViewer;
	[SerializeField] private TextManager manager;
    
	void Update()
	{
		if (Input.GetKey(KeyCode.M)){
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			if (Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift))
			{
				manager.textIndex = 34;
				manager.NextText();
			}
		}
		else if (Input.GetMouseButton(1) | Input.GetMouseButton(0))
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Confined;	
		}
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
		if (Input.GetKeyDown(KeyCode.N))
		{
			if (uiOn)
			{
				uiOn = false;
			}
			else 
			{
				uiOn = true;
			}
			animViewer.SetActive(uiOn);
		}
	}
}
