public class Crate: Interactable{
    public override void Interact(){
        UpgradeManager.um.UtilityUpgradeObtain();
        interactable = false;
        sr.sprite = postInteract;
    }
}