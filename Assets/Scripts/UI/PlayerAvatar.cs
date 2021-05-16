using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class PlayerAvatar : MonoBehaviour
{
    [SerializeField] private Text nickname;
    [SerializeField] private Image picture;
    [SerializeField] private Image cloud;
    [SerializeField] private Text message;

    public void Awake()
    {
        cloud.DOFade(0, 0);
    }

    public string GetNickName()
    {
        return this.nickname.text;
    }

    public void SetNickname(string nickname)
    {
        this.nickname.text = nickname;
    }

    public void SetPicture(Sprite picture)
    {
        this.picture.sprite = picture;
    }

    public void Say(string message)
    {
        Debug.Log(nickname.text + " says " + message);
        this.message.text = message;
        ShowCloud();
    }

    private void ShowCloud()
    {
        StartCoroutine(SomeCoroutine());
    }

    IEnumerator SomeCoroutine()
    {
        Tween myTween = cloud.DOFade(1, 2.5f);
        yield return myTween.WaitForCompletion();
        message.text = "";
        Tween anotherTween = cloud.DOFade(0, 2.5f);
        yield return anotherTween.WaitForCompletion();
        Debug.Log("Tween completed!");
    }
}
