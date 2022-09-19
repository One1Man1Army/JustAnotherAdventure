using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace JAA.Structure.StateMachine.Logic
{
  public class LoadingCurtain : MonoBehaviour
  {
    [SerializeField] private CanvasGroup _curtain;
    
    private readonly WaitForSeconds _seconds = new WaitForSeconds(0.03f);

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    public void Show()
    {
      gameObject.SetActive(true);
      _curtain.alpha = 1;
    }
    
    public void Hide() => StartCoroutine(DoFadeIn());
    
    private IEnumerator DoFadeIn()
    {
      while (_curtain.alpha > 0)
      {
        _curtain.alpha -= 0.03f;
        yield return _seconds;
      }
      
      gameObject.SetActive(false);
    }
  }
}