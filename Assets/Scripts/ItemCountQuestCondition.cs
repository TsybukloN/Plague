namespace PixelCrushers.QuestMachine.Demo
{
    /// <summary>
    /// This example condition for the Demo scene checks the amount of an item in the DemoInventory.
    /// The Pirate's Coin Race quest will become unofferable if the player has already looted all
    /// of the coins from the breakable crates and barrels.
    /// </summary>
    public class ItemCountQuestCondition : QuestCondition
    {
        public Inventory.ItemType itemType;

        public CounterValueConditionMode comparison = CounterValueConditionMode.AtLeast;

        public QuestNumber requiredValue = new QuestNumber();

        public override string GetEditorName()
        {
            return "Inventory has " + comparison + " " + requiredValue.GetValue(quest) + " " + itemType;
        }

        public override void StartChecking(System.Action trueAction)
        {
            base.StartChecking(trueAction);
            if (IsTrue()) SetTrue();
        }

        private bool IsTrue()
        {
            var Inventory = FindObjectOfType<Inventory>();
            if (Inventory != null)
            {
                var itemCount = Inventory.GetItemCount((int)itemType);
                switch (comparison)
                {
                    case CounterValueConditionMode.AtLeast:
                        if (itemCount >= requiredValue.GetValue(quest)) return true;
                        break;
                    case CounterValueConditionMode.AtMost:
                        if (itemCount <= requiredValue.GetValue(quest)) return true;
                        break;
                }
            }
            return false;
        }
    }
}
