    using UnityEngine;using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class UpgradeIcon: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
        public Image Icon;
        public Upgrade u;
        
    public void Start(){
            Icon.sprite = u.Icon;
        }
        
        public void OnPointerEnter(PointerEventData eventData){
            UpgradeToolTip.tt.Show = true;
            UpgradeToolTip.tt.UpdateTT(u);
        }

        public void OnPointerExit(PointerEventData eventData){
            UpgradeToolTip.tt.Show = false;
        }
    }
