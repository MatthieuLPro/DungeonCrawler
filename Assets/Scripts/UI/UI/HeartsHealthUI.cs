using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsHealthUI : MonoBehaviour
{
    public static HeartsHealthSystem heartsHealthSystemStatic;

    [SerializeField]
    private Sprite heartSpriteFull = null, heartSpriteMid = null, heartSpriteEmpty = null;

    private List<HeartImage> heartImageList;
    private HeartsHealthSystem heartsHealthSystem;

    private void Awake(){
        heartImageList = new List<HeartImage>();
    }

    private void Start()
    {
        HeartsHealthSystem heartsHealthSystem = new HeartsHealthSystem(3);
        SetHeartsHealthSystem(heartsHealthSystem);
    }

    public void SetHeartsHealthSystem(HeartsHealthSystem heartsHealthSystem)
    {
        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        int heartOffset = 11;
 
        this.heartsHealthSystem = heartsHealthSystem;
        heartsHealthSystemStatic = heartsHealthSystem;
        for(int i = 0; i < heartList.Count; i++)
        {
            HeartImage newHeart = CreateHeart(new Vector2(i * heartOffset, 0));
            newHeart.SetValue(heartList[i].GetValue());
        }

        heartsHealthSystem.OnDamaged += RefreshHearts;
        heartsHealthSystem.OnHealed += RefreshHearts;
    }

    private HeartImage CreateHeart(Vector2 anchoredPosition)
    {
        GameObject heartGameObject = new GameObject("heart", typeof(Image));
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        HeartImage heartImage = new HeartImage(this, heartImageUI);

        HeartPositionAndSize(heartGameObject, anchoredPosition);
        heartImageList.Add(heartImage);
        return (heartImage);
    }

    private void HeartPositionAndSize(GameObject heart, Vector2 anchoredPosition)
    {
        heart.transform.SetParent(transform);
        heart.transform.localPosition = Vector3.zero;
        heart.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heart.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);
    }

    private void RefreshHearts(object sender, System.EventArgs e)
    {
        for(int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HeartsHealthSystem.Heart heart = heartsHealthSystem.GetHeartList()[i];
            heartImage.SetValue(heart.GetValue());
        }
    }

    public class HeartImage
    {
        private int value;
        private Image heartImage;
        private HeartsHealthUI heartsHealthUI;

        public HeartImage(HeartsHealthUI heartsHealthUI, Image heartImage)
        {
            this.heartsHealthUI = heartsHealthUI;
            this.heartImage = heartImage;
        }

        public void SetValue(int value)
        {
            this.value = value;
            switch(value)
            {
                case 0:
                    this.heartImage.sprite = heartsHealthUI.heartSpriteEmpty;
                    break;
                case 1:
                    this.heartImage.sprite = heartsHealthUI.heartSpriteMid;
                    break;
                default:
                    this.heartImage.sprite = heartsHealthUI.heartSpriteFull;
                    break;
            }
        }

        public int GetValue(){
            return value;
        }
    }
}
