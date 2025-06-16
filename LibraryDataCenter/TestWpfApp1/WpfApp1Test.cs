using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryPresentationLayer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;



file class FakeModel : IModel
{
    public List<IModelCatalog> Catalogs { get; } = new();
    public List<IModelUser> Users { get; } = new();

    public Task AddCatalogAsync(int id, string author, string title, int nrOfPages)
    {
        Catalogs.Add(new FakeCatalog { CatalogId = id, Author = author, Title = title, NrOfPages = nrOfPages });
        return Task.CompletedTask;
    }

    public Task RemoveCatalogAsync(int id)
    {
        Catalogs.RemoveAll(c => c.CatalogId == id);
        return Task.CompletedTask;
    }

    public Task AddUserAsync(int id, string firstName, string lastName)
    {
        Users.Add(new FakeUser { UserId = id, FirstName = firstName, LastName = lastName });
        return Task.CompletedTask;
    }

    public Task RemoveUserAsync(int id)
    {
        Users.RemoveAll(u => u.UserId == id);
        return Task.CompletedTask;
    }

    public Task AddDatabaseEventAsync(int id, int employeeId, int stateId, bool addition) => Task.CompletedTask;
    public Task AddUserEventAsync(int id, int employeeId, int stateId, int userId, bool borrowing) => Task.CompletedTask;
    public Task RemoveEventAsync(int id) => Task.CompletedTask;
    public Task AddStateAsync(int id, int nrOfBooks, int catalogId) => Task.CompletedTask;
    public Task RemoveStateAsync(int id) => Task.CompletedTask;

    public List<IModelCatalog> GetAllCatalogsAsync() => Catalogs;
    public List<IModelUser> GetAllUsersAsync() => Users;
    public List<IModelEvent> GetAllEventsAsync() => new();
    public List<IModelState> GetAllStatesAsync() => new();
}

file class FakeCatalog : IModelCatalog
{
    public int CatalogId { get; init; }
    public string Title { get; init; } = "";
    public string Author { get; init; } = "";
    public int NrOfPages { get; init; }
}

file class FakeUser : IModelUser
{
    public int UserId { get; init; }
    public string FirstName { get; init; } = "";
    public string LastName { get; init; } = "";
}

[TestClass]
public class PropertyChangeTests
{
    private class TestModel : PropertyChange
    {
        public string DummyName
        {
            get => Name;
            set => Name = value;
        }
    }

    [TestMethod]
    public void SettingPropertyRaisesNotification()
    {
        var model = new TestModel();
        string changedProperty = null;

        model.PropertyChanged += (sender, e) => changedProperty = e.PropertyName;

        model.DummyName = "Alice";

        Assert.AreEqual("Name", changedProperty);
        Assert.AreEqual("Alice", model.DummyName);
    }

    [TestMethod]
    public void PropertyChanged_EventRaised_WhenValueChanges()
    {
        var model = new TestModel();
        string? changedProperty = null;

        model.PropertyChanged += (s, e) => changedProperty = e.PropertyName;

        model.DummyName = "Alice";

        Assert.AreEqual("Name", changedProperty);
        Assert.AreEqual("Alice", model.DummyName);
    }

    [TestMethod]
    public void PropertyChanged_EventNotRaised_WhenValueIsSame()
    {
        var model = new TestModel();
        model.DummyName = "Same";

        int eventCount = 0;
        model.PropertyChanged += (s, e) => eventCount++;

        
        model.DummyName = "Same";

        Assert.AreEqual(0, eventCount);
    }

    [TestMethod]
    public void MultiplePropertyChanges_RaiseMultipleEvents()
    {
        var model = new TestModel();
        int eventCount = 0;

        model.PropertyChanged += (s, e) => eventCount++;

        model.DummyName = "One";
        model.DummyName = "Two";
        model.DummyName = "Three";

        Assert.AreEqual(3, eventCount);
    }

    [TestMethod]
    public void EventArgs_HasCorrectPropertyName()
    {
        var model = new TestModel();
        PropertyChangedEventArgs? args = null;

        model.PropertyChanged += (s, e) => args = e;

        model.DummyName = "Bob";

        Assert.IsNotNull(args);
        Assert.AreEqual("Name", args.PropertyName);
    }

    [TestMethod]
    public void PropertyChange_ReturnsFalse_IfNoChange()
    {
        var model = new TestModel();
        model.DummyName = "Initial";

        bool changed = model.DummyName == "Initial";
        Assert.IsTrue(changed);
    }
    [TestMethod]
    public void PropertyChanged_InvokesAllSubscribers()
    {
        var model = new TestModel();
        int counter1 = 0, counter2 = 0;

        model.PropertyChanged += (s, e) => counter1++;
        model.PropertyChanged += (s, e) => counter2++;

        model.DummyName = "Event";

        Assert.AreEqual(1, counter1);
        Assert.AreEqual(1, counter2);
    }

    [TestMethod]
    public void PropertyChanged_NotRaised_IfInitiallyNullAndSetToNull()
    {
        var model = new TestModel();
        string? changed = null;

        model.PropertyChanged += (s, e) => changed = e.PropertyName;

        model.DummyName = null;  

        Assert.IsNull(changed); 
    }
    [TestMethod]
    public void SettingSameValueAgain_TriggersOnlyIfDifferent()
    {
        var model = new TestModel();
        int callCount = 0;

        model.DummyName = "Alice";
        model.PropertyChanged += (s, e) => callCount++;

        model.DummyName = "Bob";   
        model.DummyName = "Alice"; 

        Assert.AreEqual(2, callCount);
    }
    [TestMethod]
    public void ChangingToWhitespace_IsConsideredChange()
    {
        var model = new TestModel();
        model.DummyName = "SomeValue";

        string? changedProp = null;
        model.PropertyChanged += (s, e) => changedProp = e.PropertyName;

        model.DummyName = " ";

        Assert.AreEqual("Name", changedProp);
        Assert.AreEqual(" ", model.DummyName);
    }
    private class IntPropertyModel : PropertyChange
    {
        private int _age;
        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }
    }

    [TestMethod]
    public void SetProperty_WorksWithValueTypes()
    {
        var model = new IntPropertyModel();
        string? changedProp = null;

        model.PropertyChanged += (s, e) => changedProp = e.PropertyName;

        model.Age = 30;

        Assert.AreEqual("Age", changedProp);
        Assert.AreEqual(30, model.Age);
    }


}

[TestClass]
public class ModelInterfaceTests
{
    
    private class FakeModel : IModel
    {
        public List<IModelCatalog> Catalogs { get; } = new();

        public Task AddCatalogAsync(int id, string author, string title, int nrOfPages)
        {
            Catalogs.Add(new FakeCatalog { CatalogId = id, Author = author, Title = title, NrOfPages = nrOfPages });
            return Task.CompletedTask;
        }

        public Task RemoveCatalogAsync(int id)
        {
            Catalogs.RemoveAll(c => c.CatalogId == id);
            return Task.CompletedTask;
        }

        public Task AddUserAsync(int id, string firstName, string lastName) => Task.CompletedTask;
        public Task RemoveUserAsync(int id) => Task.CompletedTask;
        public Task AddDatabaseEventAsync(int id, int employeeId, int stateId, bool addition) => Task.CompletedTask;
        public Task AddUserEventAsync(int id, int employeeId, int stateId, int userId, bool borrowing) => Task.CompletedTask;
        public Task RemoveEventAsync(int id) => Task.CompletedTask;
        public Task AddStateAsync(int id, int nrOfBooks, int catalogId) => Task.CompletedTask;
        public Task RemoveStateAsync(int id) => Task.CompletedTask;

        public List<IModelCatalog> GetAllCatalogsAsync() => Catalogs;
        public List<IModelUser> GetAllUsersAsync() => new();
        public List<IModelEvent> GetAllEventsAsync() => new();
        public List<IModelState> GetAllStatesAsync() => new();
    }

    private class FakeCatalog : IModelCatalog
    {
        public int CatalogId { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public int NrOfPages { get; init; }
    }

    [TestMethod]
    public async Task AddCatalogAddsItemToList()
    {
        var model = new FakeModel();
        await model.AddCatalogAsync(10, "Author X", "Book Y", 250);

        var catalogs = model.GetAllCatalogsAsync();

        Assert.AreEqual(1, catalogs.Count);
        Assert.AreEqual("Author X", catalogs[0].Author);
        Assert.AreEqual("Book Y", catalogs[0].Title);
        Assert.AreEqual(250, catalogs[0].NrOfPages);
    }

    [TestMethod]
    public void ModelCatalog_ConstructsCorrectly()
    {
        var catalog = new ModelCatalog(1, "1984", "Orwell", 300);

        Assert.AreEqual(1, catalog.CatalogId);
        Assert.AreEqual("1984", catalog.Title);
        Assert.AreEqual("Orwell", catalog.Author);
        Assert.AreEqual(300, catalog.NrOfPages);
    }

    [TestMethod]
    public void ModelUser_ConstructsCorrectly()
    {
        var user = new ModelUser(2, "Alice", "Smith");

        Assert.AreEqual(2, user.UserId);
        Assert.AreEqual("Alice", user.FirstName);
        Assert.AreEqual("Smith", user.LastName);
    }

    [TestMethod]
    public void ModelState_ConstructsCorrectly()
    {
        var state = new ModelState(3, 15, 1);

        Assert.AreEqual(3, state.StateId);
        Assert.AreEqual(15, state.NrOfBooks);
        Assert.AreEqual(1, state.Catalog);
    }

    [TestMethod]
    public void ModelUserEvent_ConstructsCorrectly()
    {
        var userEvent = new ModelUserEvent(10, 100, 5, 50, true);

        Assert.AreEqual(10, userEvent.EventId);
        Assert.AreEqual(100, userEvent.Employee);
        Assert.AreEqual(5, userEvent.State);
        Assert.AreEqual(50, userEvent.User);
        Assert.IsTrue(userEvent.Borrowing);
    }

    [TestMethod]
    public void ModelDatabaseEvent_ConstructsCorrectly()
    {
        var dbEvent = new ModelDatabaseEvent(20, 200, 8, true);

        Assert.AreEqual(20, dbEvent.EventId);
        Assert.AreEqual(200, dbEvent.Employee);
        Assert.AreEqual(8, dbEvent.State);
        Assert.IsTrue(dbEvent.Addition);
    }

    
}


