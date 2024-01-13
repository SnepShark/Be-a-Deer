using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debugmenu : MonoBehaviour
{
	[SerializeField] private Slider stageSlider;
	[SerializeField] private Slider percentSlider;
	[SerializeField] private TFManager manager;
    void Start()
    {
	    stageSlider.onValueChanged.AddListener((v) => {
		    manager.changeBodyState(v);
		    manager.changeFaceState(v);
	    });
	    percentSlider.onValueChanged.AddListener((v) => {
		    manager.setPercent(v);
	    });
    }
    
}
