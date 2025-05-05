
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Profile : MonoBehaviour
{
    #region Singlton:Profile

    public static Profile Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #endregion

    public class Avatar
    {
        public Sprite Image;
    }

    public List<Avatar> AvatarsList;

    [SerializeField] GameObject AvatarUITemplate;
    [SerializeField] Transform AvatarsScrollView;

    GameObject g;
    int newSelectedIndex, previousSelectedIndex;

    [SerializeField] Color ActiveAvatarColor;
    [SerializeField] Color DefaultAvatarColor;

    [SerializeField] Image CurrentAvatar;

    void Start()
    {

        StartCoroutine(DelayedGetAvatars());
        newSelectedIndex = Save.instance.currentBall;
        newSelectedIndex = previousSelectedIndex = 0;
    }
    /*
    void GetAvailableAvatars()
    {
        for (int i = 0; i < Shop.Instance.ShopItemsList.Count; i++)
        {
            if (Shop.Instance.ShopItemsList[i].IsPurchased)
            {
                //add all purchased avatars to AvatarsList
                AddAvatar(Shop.Instance.ShopItemsList[i].Image);
            }
        }

        SelectAvatar(newSelectedIndex);
    }

    */

    public void GetAvailableAvatars()
    {
        if (AvatarsList == null)
            AvatarsList = new List<Avatar>();

        // Clear scroll view before populating
        foreach (Transform child in AvatarsScrollView)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < Shop.Instance.ShopItemsList.Count; i++)
        {
            var item = Shop.Instance.ShopItemsList[i];
            if (item.IsPurchased)
            {
                Avatar av = new Avatar() { Image = item.Image };
                AvatarsList.Add(av);

                g = Instantiate(AvatarUITemplate, AvatarsScrollView);
                g.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = av.Image;
                g.GetComponent<Button>().AddEventListener(AvatarsList.Count - 1, OnAvatarClick);
            }
        }

        //  Only try to select if there’s at least one avatar
        if (AvatarsList.Count > 0)
        {
            newSelectedIndex = Mathf.Clamp(Save.instance.currentBall, 0, AvatarsList.Count - 1);
            previousSelectedIndex = newSelectedIndex;
            SelectAvatar(newSelectedIndex);
        }
    }


    public void AddAvatar(Sprite img)
    {
        if (AvatarsList == null)
            AvatarsList = new List<Avatar>();

        Avatar av = new Avatar() { Image = img };
        //add av to AvatarsList
        AvatarsList.Add(av);

        //add avatar in the UI scroll view
        g = Instantiate(AvatarUITemplate, AvatarsScrollView);
        g.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = av.Image;

        //add click event
        g.transform.GetComponent<Button>().AddEventListener(AvatarsList.Count - 1, OnAvatarClick);
    }

    void OnAvatarClick(int AvatarIndex)
    {
        SelectAvatar(AvatarIndex);
    }

    void SelectAvatar(int AvatarIndex)
    {
        previousSelectedIndex = newSelectedIndex;
        newSelectedIndex = AvatarIndex;
        AvatarsScrollView.GetChild(previousSelectedIndex).GetComponent<Image>().color = DefaultAvatarColor;
        AvatarsScrollView.GetChild(newSelectedIndex).GetComponent<Image>().color = ActiveAvatarColor;
        //Change Avatar
        CurrentAvatar.sprite = AvatarsList[newSelectedIndex].Image;
        Save.instance.currentBall = newSelectedIndex;
        Save.instance.SaveData();
        Debug.Log(newSelectedIndex);


    }

    System.Collections.IEnumerator DelayedGetAvatars()
    {
        yield return new WaitForSeconds(0.1f); // delay a bit so Shop has finished
        GetAvailableAvatars();
    }
}
