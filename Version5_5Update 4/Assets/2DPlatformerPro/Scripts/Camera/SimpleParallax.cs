using UnityEngine;
using System.Collections;



	/// <summary>
	/// Very simple parralax scroller.
	/// </summary>
	public class SimpleParallax : MonoBehaviour
	{

		/// <summary>
		/// The main camera that moves with character.
		/// </summary>
		public Camera scrollCamera;

		/// <summary>
		/// How much the background moves with the camera.
		/// </summary>
        /// Visual Display and hoverText
		[Range(0,1)]
		[Tooltip("How much the background moves with the camera, from 0 (not at all) to 1 (track camera 1 for 1).")]
		public float factor = 0.33f;

		/// <summary>
		/// The last camera position.
		/// </summary>
		protected Vector3 lastCameraPosition;

		/// <summary>
		/// Unity Start() hook.
		/// </summary>
		void Start()
		{
			if (scrollCamera == null) scrollCamera = Camera.main;
			lastCameraPosition = scrollCamera.transform.position;
		}

		/// <summary>
		/// Unity LateUpdate() hook.
		/// </summary>
		void LateUpdate()
		{
			Vector3 currentCameraPosition = scrollCamera.transform.position;
			float diffX = lastCameraPosition.x - currentCameraPosition.x;
            float diffY = lastCameraPosition.y - currentCameraPosition.y;
            transform.Translate (-diffX + (diffX * factor), -diffY + (diffY * factor), 0);
            
            lastCameraPosition = currentCameraPosition;
		}
	}

