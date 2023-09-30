using UnityEngine;
using UnityEngine.UI;

public class PlayTapSound : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(TapSound);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(TapSound);
    }

    private void TapSound()
    {
        DemoAudioManager.instance.PlayClipByIndex(0);
    }
}
