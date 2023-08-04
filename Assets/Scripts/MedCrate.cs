public class MedCrate : CostedCrate{
    public override void OnBuy(){
        UpgradeManager.um.MedUpgradeObtain();
        sr.sprite = postInteract;
    }
}