using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using DG.Tweening;

public class Storage_upgrade : MonoBehaviour
{
    [SerializeField] private int[] resources = new int[5];
    [SerializeField] private TextMeshProUGUI[] resourceTexts = new TextMeshProUGUI[5];

    [SerializeField] private int _currentQuantity, _maxQuantity;
    [SerializeField] private TextMeshProUGUI _storageQuantity;

    [SerializeField] private Image _quantityImage;

    private int _balance;
    [SerializeField] private TextMeshProUGUI _balanceTMP;
    [SerializeField] private Transform _quantityTransform;

    void Start()
    {
        resources[0] = 54; // _coal
        resources[1] = 56; // _ore
        resources[2] = 40; // _iron
        resources[3] = 0;  // _steel
        resources[4] = 0;  // _copper
       
        _maxQuantity = 500;
        _balance = 800;

        UpdateText();
        StartCoroutine(AddRandomItem());
    }

    public void UpdateText()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            resourceTexts[i].text = resources[i].ToString();
        }

        _balanceTMP.text = _balance.ToString();

        _currentQuantity = resources.Sum();
        _storageQuantity.text = _currentQuantity.ToString() + "/" + _maxQuantity.ToString();

        _quantityImage.fillAmount = (float)_currentQuantity / _maxQuantity;
        //_quantityImage.transform.DORotate(360f, 0.2f).SetEase(Ease.OutExpo)
        //    .OnComplete(() => _quantityImage.transform.DOScale(1f, 0.2f).SetEase(Ease.OutExpo));
    }

    public void UpgradeQuantity()
    {
        if(_balance >= 100)
        {
            _balance -= 100;
            _maxQuantity += 30;

            _quantityTransform.transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutExpo)
            .OnComplete(() => _quantityTransform.transform.DOScale(1f, 0.2f).SetEase(Ease.OutExpo));

            UpdateText();
        }
    }
    public IEnumerator AddRandomItem()
    {
        for(int i = 0; i < _maxQuantity; i++)
        {
            int _randomResource = Random.Range(0, resources.Length);
            resources[_randomResource]++;

            resourceTexts[_randomResource].transform.DOScale(1.4f, 0.3f).SetEase(Ease.OutExpo)
            .OnComplete(() => resourceTexts[_randomResource].transform.DOScale(1f, 0.3f).SetEase(Ease.OutExpo));

            UpdateText();

            float _randomTime = Random.Range(1.2f, 2f);
            yield return new WaitForSeconds(_randomTime);
        }

        yield break;
    }
    void Update()
    {
        
    }
}
