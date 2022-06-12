namespace SC4MM.Tests.ModCollections
{

    class ModListTests
    {
        ModList modList;
        MockModListItem listItem;

        [SetUp]
        public void Setup()
        {
            modList = new();
            listItem = new MockModListItem();
        }

        void AddItemToList() => modList.Add(listItem);

        [Test]
        public void AddItemToList_Apply_ItemIsApplied()
        {
            AddItemToList();

            modList.Apply();

            Assert.That(listItem.Applied, Is.True);
        }

        [Test] 
        public void ItemAddedToList_Contains_ReturnsTrue()
        {
            AddItemToList();

            Assert.That(modList.Contains(listItem), Is.True);
        }

        [Test]
        public void ItemNotAddedToList_Contains_ReturnsFalse()
        {
            Assert.That(modList.Contains(listItem), Is.False);
        }

        [Test]
        public void ItemAdded_AddSameItemAgain_ThrowsArgumentException()
        {
            AddItemToList();
            Assume.That(modList.Contains(listItem), Is.True);

            Assert.Throws<ArgumentException>(AddItemToList);
        }

        [Test]
        public void ItemNotInList_RemoveItem_ThrowsArgumentException()
        {
            Assume.That(modList.Contains(listItem), Is.False);

            void act() => modList.Remove(listItem);

            Assert.Throws<ArgumentException>(act);
        }

        [Test]
        public void ItemInList_RemoveItem_NoLongerInList()
        {
            AddItemToList();
            Assume.That(modList.Contains(listItem), Is.True);

            modList.Remove(listItem);

            Assert.That(modList.Contains(listItem), Is.False);
        }

        [Test] public void ModInList_Contains_ReturnsTrue()
        {
            MockMod mod = new MockMod();
            MockModListItem modItem = new();
            modItem.Mod = mod;

            modList.Add(modItem);

            Assert.That(modList.Contains(modItem), Is.True);
        }
    }

    class ModListCompositeTests
    {
        ModList mainlist;
        ModList sublist;
        MockModListItem sublistItem;

        [SetUp]
        public void Setup()
        {
            mainlist = new ModList();
            sublist = new ModList();
            sublistItem = new MockModListItem();
        }

        [Test]
        public void AddSublist_ContainsSublist_ReturnsTrue()
        {
            mainlist.AddSublist(sublist);

            Assert.True(mainlist.Contains(sublist));
        }

        [Test]
        public void AllreadyAddedSublist_AddAgain_ThrowsArgumentException()
        {
            mainlist.AddSublist(sublist);
            Assume.That(mainlist.Contains(sublist), Is.True);

            void action() => mainlist.AddSublist(sublist);

            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void AllreadyAddedSublist_Remove_NoLongerContained()
        {
            mainlist.AddSublist(sublist);
            Assume.That(mainlist.Contains(sublist), Is.True);

            mainlist.RemoveSublist(sublist);

            Assert.That(mainlist.Contains(sublist), Is.False);
        }

        [Test]
        public void AllreadyAddedSublistRemoved_Apply_ContainedModNotApplied()
        {
            mainlist.AddSublist(sublist);
            Assume.That(mainlist.Contains(sublist), Is.True);
            mainlist.RemoveSublist(sublist);
            Assume.That(mainlist.Contains(sublist), Is.False);

            mainlist.Apply();

            Assert.That(sublistItem.Applied, Is.False);
        }

        [Test]
        public void NotContainedSublist_Remove_ThrowsArgumentException()
        {
            Assume.That(mainlist.Contains(sublist), Is.False);

            void act() => mainlist.RemoveSublist(sublist);

            Assert.Throws<ArgumentException>(act);
        }

        [Test]
        public void SublistWithMod_MainlistContainsMod_ReturnsTrue()
        {
            sublist.Add(sublistItem);
            mainlist.AddSublist(sublist);

            Assert.True(mainlist.Contains(sublistItem));
        }

        [Test]
        public void SublistWithMod_ApplyMainlist_SublistModApplied()
        {
            sublist.Add(sublistItem);
            mainlist.AddSublist(sublist);
            Assume.That(sublist.Contains(sublistItem));
            Assume.That(mainlist.Contains(sublist));
            Assume.That(sublistItem.Applied, Is.False);

            mainlist.Apply();

            Assert.That(sublistItem.Applied, Is.True);
        }

        [Test]
        public void SublistWithMod_AddModToMainList_ThrowsArgumentException()
        {
            sublist.Add(sublistItem);
            mainlist.AddSublist(sublist);
            Assume.That(sublist.Contains(sublistItem));
            Assume.That(mainlist.Contains(sublist));

            void act() => mainlist.Add(sublistItem);

            Assert.Throws<ArgumentException>(act);
        }
    }
}
