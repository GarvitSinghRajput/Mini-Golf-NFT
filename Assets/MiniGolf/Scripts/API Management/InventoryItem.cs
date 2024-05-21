using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using MoralisUnity;
using MoralisUnity.Platform.Objects;
using TMPro;

public class Golf_Clubs : MoralisObject
{
    public string Name { get; set; }
    public int Power { get; set; }
    public int Curve { get; set; }
    public int TopSpin { get; set; }
    public int BackSpin { get; set; }
    public int BallGuide { get; set; }
    public int Impact { get; set; }
    public int ClubType { get; set; }
    public string ImageUrl { get; set; }
    public string ClubPrice { get; set; }
    public Golf_Clubs() : base("Golf_Clubs") {}
}

public class InventoryItem : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image myIcon;
    [SerializeField] private TextMeshProUGUI myName;
    [SerializeField] private Button myButton;

    private Golf_Clubs _itemData;
    private UnityWebRequest _currentWebRequest;

    private void Awake()
    {
        //We will activate them when the texture is retrieved
        myIcon.gameObject.SetActive(false);
        myButton.interactable = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _currentWebRequest?.Dispose();
    }

    public void Init(Golf_Clubs newData)
    {
        _itemData = newData;
        myName.text = _itemData.Name;
        StartCoroutine(GetTexture(_itemData.ImageUrl));
    }

    public void Init(string tokenId, NftMetadata nftMetadata)
    {
        _itemData = new Golf_Clubs
        {
            objectId = tokenId,
            Name = nftMetadata.Name,
            Power = nftMetadata.Power,
            Curve = nftMetadata.Curve,
            TopSpin = nftMetadata.TopSpin,
            BackSpin = nftMetadata.BackSpin,
            BallGuide = nftMetadata.BallGuide,
            Impact = nftMetadata.Impact,
            ClubType = nftMetadata.ClubType,
            ImageUrl = nftMetadata.ImageUrl,
        };

        StartCoroutine(GetTexture(_itemData.ImageUrl));
    }

    public string GetId()
    {
        return _itemData.objectId;
    }

    public Golf_Clubs GetData()
    {
        return _itemData;
    }

    public Sprite GetSprite()
    {
        return myIcon.sprite;
    }

    public string GetName()
    {
        return myName.text;
    }

    public void SetData(Golf_Clubs newData)
    {
        if (_itemData.ImageUrl != newData.ImageUrl)
        {
            //We need to get the new texture
            GetTexture(newData.ImageUrl);
        }

        _itemData = newData;
    }

    private IEnumerator GetTexture(string imageUrl)
    {
        using UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imageUrl);
        _currentWebRequest = uwr;

        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(uwr.error);
            uwr.Dispose();
        }
        else
        {
            var tex = DownloadHandlerTexture.GetContent(uwr);
            myIcon.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

            //Now we are able to click the button and we will pass the loaded sprite :)
            myIcon.gameObject.SetActive(true);
            myButton.interactable = true;

            uwr.Dispose();
        }
    }
}

