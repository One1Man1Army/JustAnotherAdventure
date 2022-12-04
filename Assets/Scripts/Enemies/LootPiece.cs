using System;
using JAA.Data;
using TMPro;
using UnityEngine;

namespace JAA.Enemies
{
	public class LootPiece : MonoBehaviour
	{
		[SerializeField] private Transform _skull;
		[SerializeField] private GameObject _pickUpFxPrefab;
		[SerializeField] private TextMeshPro _lootText;
		[SerializeField] private GameObject _pickUpPopUp;
		
		private Loot _loot;
		private bool _picked;
		private WorldData _worldData;

		public void Construct(WorldData worldData)
		{
			_worldData = worldData;
		}

		public void Initialize(Loot loot)
		{
			_loot = loot;
		}

		private void OnTriggerEnter(Collider other) =>
			PickUp();

		private void PickUp()
		{
			if (_picked) 
				return;
			
			_picked = true;

			UpdateWorldData();

			HideSkull();
			PlayPickUpFX();
			ShowText();
			
			Invoke(nameof(DestroySelf), 1.5f);
		}

		private void UpdateWorldData() => 
			_worldData.lootData.Collect(_loot);

		private void HideSkull() => 
			_skull.gameObject.SetActive(false);

		private void DestroySelf() =>
			Destroy(gameObject);

		private void PlayPickUpFX() => 
			Instantiate(_pickUpFxPrefab, transform.position, Quaternion.identity);

		private void ShowText()
		{
			_lootText.text = $"{_loot.value}";
			_pickUpPopUp.gameObject.SetActive(true);
		}
	}
}