using System;
using JAA.Data;
using TMPro;
using UnityEngine;

namespace JAA.UI
{
	public class LootCounter : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _counter;
		private WorldData _worldData;

		private void Start()
		{
			UpdateCounter();
		}

		public void Construct(WorldData worldData)
		{
			_worldData = worldData;
			_worldData.lootData.changed += UpdateCounter;
		}

		private void UpdateCounter()
		{
			_counter.text = $"{_worldData.lootData.collected}";
		}
	}
}