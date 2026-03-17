using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;
using Fragilem17.MirrorsAndPortals;

/// <summary>
/// A simple FPP (First Person Perspective) camera rotation script.
/// Like those found in most FPS (First Person Shooter) games.
/// </summary>
public class FirstPersonCameraRotation : MonoBehaviour {

	public float Sensitivity {
		get { return sensitivity; }
		set { sensitivity = value; }
	}
	[Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
	[Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;
	[Range(0f, 190f)][SerializeField] float xRotationLimit = 88f;

	Vector2 rotation = Vector2.zero;
	const string xAxis = "Mouse X";
	const string yAxis = "Mouse Y";
	
	private bool zoomed = false;
	[SerializeField] float fovNormal = 60.0f;
	[SerializeField] float fovZoomed = 15.0f;
	[SerializeField] float zoomSpeed = 1.5f;
	[SerializeField] CinemachineCamera cam;

	[SerializeField] private int quality = 0;
	[SerializeField] private MirrorRenderer mirrorRenderer;
 
	void Update(){
		if(!Input.GetKey(KeyCode.M))
			{
		rotation.x += Input.GetAxis(xAxis) * sensitivity;
		rotation.y += Input.GetAxis(yAxis) * sensitivity;
		rotation.x = Mathf.Clamp(rotation.x, -xRotationLimit, xRotationLimit);
		rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
		var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
		var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

			transform.localRotation = xQuat * yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
			}
		
		if (Input.GetMouseButton(1)|Input.GetKey(KeyCode.Space)){
			if (zoomed == false){
				zoomed = true;
				DOTween.To(()=> cam.Lens.FieldOfView, x=> cam.Lens.FieldOfView = x, fovZoomed, zoomSpeed).SetEase(Ease.OutSine);
				//cam.DOFieldOfView(fovZoomed, zoomSpeed).SetEase(Ease.OutSine);
			}
		}
		else
		{
			if (zoomed == true){
				zoomed = false;
				DOTween.To(()=> cam.Lens.FieldOfView, x=> cam.Lens.FieldOfView = x, fovNormal, zoomSpeed).SetEase(Ease.OutSine);
				//cam.DOFieldOfView(fovNormal, zoomSpeed);
			}
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
            QualitySettings.SetQualityLevel(quality);
			switch (quality)
			{
                case 0:
                    mirrorRenderer.screenScaleFactor = 1.0f;
                    QualitySettings.SetQualityLevel(1);
                    quality = 1;
                    break;
                case 1:
                    mirrorRenderer.screenScaleFactor = 0.5f;
                    QualitySettings.SetQualityLevel(1);
                    quality = 0;
                    break;
                case 2:
					mirrorRenderer.screenScaleFactor = 1.0f;
					QualitySettings.SetQualityLevel(0);
					quality = 3;
					break;
				case 3:
					mirrorRenderer.screenScaleFactor = 0.5f;
					QualitySettings.SetQualityLevel(0);
					quality = 0;
					break;
			}
        }
	}
}
