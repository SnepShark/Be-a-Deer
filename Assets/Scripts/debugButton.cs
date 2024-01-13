using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class debugButton : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] TFManager manager;
	[SerializeField] int animIndex;
	
	public void OnPointerDown(PointerEventData data)
	{
		manager.playBoneAnim(animIndex);
	}
}
