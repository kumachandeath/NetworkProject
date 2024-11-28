using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ChangeTextColor : UIBInder, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText; // ������ �ؽ�Ʈ

    private Color _hoverColor = new Color(178f / 255f, 217f / 255f, 116f / 255f);
    private Color _defaultColor = new Color(94f / 255f, 204f / 255f, 58f / 255f);

    // ���콺�� ��ư�� ���� �� ȣ��Ǵ� �޼���
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = _hoverColor; // ���ϴ� �������� ����
    }

    // ���콺�� ��ư�� ��� �� ȣ��Ǵ� �޼���
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = _defaultColor; // ���� �������� ����
    }
}
