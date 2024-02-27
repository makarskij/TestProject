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
    [SerializeField] private Transform[] resourcesIMG = new Transform[5];

    [SerializeField] private int _currentQuantity, _maxQuantity;
    [SerializeField] private TextMeshProUGUI _storageQuantity;

    [SerializeField] private Image _quantityImage;

    private int _balance;
    [SerializeField] private TextMeshProUGUI _balanceTMP;
    private Transform _balanceTransform;
    private Vector3 initialPosition;
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
        _balanceTransform = _balanceTMP.GetComponent<Transform>();
        initialPosition = _balanceTransform.localPosition;

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
    }

    public void UpgradeQuantity()
    {
        if(_balance >= 100)
        {
            _balance -= 100;
            _maxQuantity += 30;

            _balanceTransform.DOPunchPosition(new Vector3(10f, 0f, 0f), 0.3f, 5, 1f)
            .OnComplete(() =>
             {
                 _balanceTransform.localPosition = initialPosition;
             });

            Sequence scaleSequence = DOTween.Sequence();
            scaleSequence.Append(_quantityTransform.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.2f).SetEase(Ease.InBack));
            scaleSequence.AppendInterval(0.4f);
            scaleSequence.Append(_quantityTransform.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(Ease.OutBack));
            scaleSequence.Play();

            _quantityImage.transform.DOLocalRotate(new Vector3(0f, 0f, -360f), 0.8f, RotateMode.FastBeyond360).SetEase(Ease.InOutBack);
            UpdateText();
        }
        else
        {
            _balanceTransform.DOPunchPosition(new Vector3(20f, 0f, 0f), 0.15f, 5, 10f).SetEase(Ease.OutBounce)
            .SetLoops(3, LoopType.Restart)
            .OnComplete(() =>
             {
                 _balanceTransform.localPosition = initialPosition;
             });
        }
    }
    public IEnumerator AddRandomItem()
    {
        for (int i = 0; i < _maxQuantity; i++)
        {
            int _randomResource = Random.Range(0, resources.Length);
            resources[_randomResource]++;

            resourceTexts[_randomResource].transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 1f, 3, 0.5f).SetEase(Ease.InOutBack);
            resourcesIMG[_randomResource].DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 1f, 3, 0.5f).SetEase(Ease.InOutBack);

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
