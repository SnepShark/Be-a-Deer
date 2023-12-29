﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TFManager : MonoBehaviour
{
	private Renderer bodyRenderer;
	private Renderer faceRenderer;
	[SerializeField]
 private GameObject bodyObj;
 [SerializeField]
 private GameObject faceObj;
 [SerializeField] Animator anim;
    void Start()
    {
	    bodyRenderer = bodyObj.GetComponent<Renderer>();
	    faceRenderer = faceObj.GetComponent<Renderer>();
    }

	public void changeBodyState(float stage)
	{
		bodyRenderer.sharedMaterial.SetFloat("_Index", stage);
	}
	public void changeFaceState(float stage)
	{
		faceRenderer.sharedMaterial.SetFloat("_Index", stage);
	}
	
	public void transition(float time, float target)
	{
		bodyRenderer.sharedMaterial.DOFloat(target, "_Percent", time);
		faceRenderer.sharedMaterial.DOFloat(target, "_Percent", time);
	}
	
	public void setPercent(float target)
	{
		bodyRenderer.sharedMaterial.SetFloat("_Percent", target);
		faceRenderer.sharedMaterial.SetFloat("_Percent", target);
	}
	
	public void playBoneAnim(int which)
	{
		Debug.Log(which);
		switch (which)
		{
			//ears
		case 0:
			anim.Play("earsgrow");
			break;
			
			//antlers
		case 1:
			anim.Play("antlergrowth");
			break;
			
			//antlers
		case 2:
			anim.Play("muzzlegrow");
			break;
		case 3:
			anim.Play("muzzleoff");
			anim.Play("earsoff");
			anim.Play("antlersoff");
			break;
		}
	}
	
	public void resetAndGo(float stage, float initial, float target, float time)
	{
		setPercent(initial);
		changeBodyState(stage);
		changeFaceState(stage);
		transition(time, target);
	}
}
