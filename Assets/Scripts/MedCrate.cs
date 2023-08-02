public class MedCrate: CostedCrate{
    public override void Interact(){
        if (Player.p.SpendGold(cost)){
            interactable = false;
            firstInteract = false;
            Player.p.RemoveGold(cost);
            base.Interact();
            UpgradeManager.um.MedUpgradeObtain();
            sr.sprite = postInteract;

        }
        
    }
}