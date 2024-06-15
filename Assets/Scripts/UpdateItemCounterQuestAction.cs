namespace PixelCrushers.QuestMachine.Demo
{
    /// <summary>
    /// This example action for the Demo scene updates a quest counter with the 
    /// current amount of items in the player's inventory. When the Harvest Carrots
    /// quest starts, it uses this action to update the counter.
    /// </summary>
    public class UpdateItemCounterQuestAction : QuestAction
    {
        public Inventory.ItemType itemType;

        public override string GetEditorName()
        {
            return "Update " + itemType.ToString().ToLower() + "s counter with current value in Inventory";
        }

        public override void Execute()
        {
            base.Execute();
            QuestCounter counter = null;
            switch (itemType)
            {
                case Inventory.ItemType.Herb1:
                    counter = quest.GetCounter("Herb1");
                    if (counter != null) counter.currentValue = FindObjectOfType<DemoInventory>().GetItemCount((int)itemType);
                    break;
                case Inventory.ItemType.Herb2:
                    counter = quest.GetCounter("Herb2");
                    if (counter != null) counter.currentValue = FindObjectOfType<DemoInventory>().GetItemCount((int)itemType);
                    break;
                case Inventory.ItemType.Herb3:
                    counter = quest.GetCounter("Herb3");
                    if (counter != null) counter.currentValue = FindObjectOfType<DemoInventory>().GetItemCount((int)itemType);
                    break;
            }
        }
    }
}
