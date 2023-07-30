public class MedCrate: Interactable{
    public override void Interact(){
        UpgradeManager.um.MedUpgradeObtain();
        interactable = false;
        sr.sprite = postInteract;
    }
}