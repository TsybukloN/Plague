// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using System;

namespace PixelCrushers.QuestMachine.Demo
{
    /// <summary>
    /// Simple inventory
    /// </summary>
    public class Inventory : Saver, IMessageHandler
    {
        public enum ItemType { Herb1 = 0, Herb2 = 1, Herb3 = 2 }

        public static int Herb1Slot { get { return (int)ItemType.Herb1; } }
        public static int Herb2Slot { get { return (int)ItemType.Herb2; } }
        public static int Herb3Slot { get { return (int)ItemType.Herb3; } }

        [Serializable]
        public class Slot
        {
            public UnityEngine.UI.Button itemButton;
            public UnityEngine.UI.Text countText;
            public UnityEngine.UI.Text useText;

            public int count;
            public int maxCount = 1;
            public bool usable;
        }

        public Slot[] slots;

        public int usingIndex { get; private set; }

        public override void Awake()
        {
            base.Awake();
            usingIndex = -1;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            ListenForMessages();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            MessageSystem.RemoveListener(this);
        }

        public void AddItem(int index)
        {
            ModifyItemCount(index, 1);
        }

        public void RemoveItem(int index)
        {
            ModifyItemCount(index, -1);
        }

        public void ModifyItemCount(int index, int delta)
        {
            SetItemCount(index, GetItemCount(index) + delta);
        }

        public void SetItemCount(int index, int count)
        {
            var slot = GetSlot(index);
            if (slot == null) return;
            slot.count = Mathf.Clamp(count, 0, slot.maxCount);
            slot.itemButton.GetComponent<UnityEngine.UI.Image>().enabled = slot.count > 0;
            slot.itemButton.interactable = slot.usable && slot.count > 0;
            slot.countText.enabled = slot.count > 0;
            slot.countText.text = slot.count.ToString();
        }

        public int GetItemCount(int index)
        {
            var slot = GetSlot(index);
            return (slot != null) ? slot.count : 0;
        }

        public void UseItem(int index)
        {
            var slot = GetSlot(index);
            if (slot == null) return;
            var turnOn = usingIndex != index;
            slot.useText.enabled = turnOn;
            usingIndex = turnOn ? index : -1;
        }

        private Slot GetSlot(int index)
        {
            return (0 <= index && index < slots.Length) ? slots[index] : null;
        }

        private void ListenForMessages()
        {
            // Listen for messages for getting and dropping items:
            MessageSystem.AddListener(this, "Get", "Herb1");
            MessageSystem.AddListener(this, "Get", "Herb2");
            MessageSystem.AddListener(this, "Get", "Herb3");
            MessageSystem.AddListener(this, "Drop", "Herb1");
            MessageSystem.AddListener(this, "Drop", "Herb2");
            MessageSystem.AddListener(this, "Drop", "Herb3");
        }

        public void OnMessage(MessageArgs messageArgs)
        {
            // Modify the item count:
            var count = (messageArgs.firstValue != null) && (messageArgs.firstValue.GetType() == typeof(int)) ? (int)messageArgs.firstValue : 1;
            if (messageArgs.message == "Drop") count = -count;
            var slotIndex = GetSlotIndex(messageArgs.parameter);
            ModifyItemCount(slotIndex, count);
        }

        private int GetSlotIndex(string slotType)
        {
            switch (slotType)
            {
                case "Herb1":
                    return Herb1Slot;
                case "Herb2":
                    return Herb2Slot;
                case "Herb3":
                    return Herb3Slot;
                default:
                    return -1;
            }
        }

        [Serializable]
        public class SaveData
        {
            public int herb1;
            public int herb2;
            public int herb3;
        }

        // Called by the Save System when saving a game. Returns a string 
        // containing data we want to include in saved games.
        public override string RecordData()
        {
            var data = new SaveData();
            data.herb1 = GetItemCount(Herb1Slot);
            data.herb2 = GetItemCount(Herb2Slot);
            data.herb3 = GetItemCount(Herb3Slot);
            return SaveSystem.Serialize(data);
        }

        // Called by the Save System when loading a game. Applies the
        // saved game data to the current state of this script.
        public override void ApplyData(string data)
        {
            if (string.IsNullOrEmpty(data)) return;
            var saveData = SaveSystem.Deserialize<SaveData>(data);
            if (saveData == null) return;
            SetItemCount(Herb1Slot, saveData.herb1);
            SetItemCount(Herb2Slot, saveData.herb2);
            SetItemCount(Herb3Slot, saveData.herb3);
        }
    }
}
