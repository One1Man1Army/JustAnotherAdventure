using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JAA.UI
{
	public class HpBar : MonoBehaviour
	{
		[SerializeField] private Image _imageCurrent;

		public void SetValue(float current, float max) =>
			_imageCurrent.fillAmount = current / max;
	}
}
