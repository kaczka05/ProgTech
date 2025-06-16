using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Windows.Input;

using System.Collections.ObjectModel;

using LibraryLogicLayer;

using LibraryDataLayer;

using LibraryModel;




namespace LibraryViewModel

{
    public class VMCatalogList : PropertyChange

    {

        private int CatalogId;

        private string Title;

        private string Author;

        private int NrOfPages;

        private VMCatalog _catalog;

        private IModelCatalog _modelCatalog;

        private IModel model;



        public ICommand AddCatalogCommand { get; }

        public ICommand RemoveCatalogCommand { get; }

        public ICommand UpdateCatalogCommand { get; }

        public ICommand EditCatalogCommand { get; }



        private ObservableCollection<VMCatalog> _catalogs;

        public VMCatalogList()

        {

            this.model = new Model();

            _catalogs = new ObservableCollection<VMCatalog>();

            AddCatalogCommand = new RelayCommand(() => { Add(); }, () => true);

            RemoveCatalogCommand = new RelayCommand(() => { Delete(); }, () => true);

            UpdateCatalogCommand = new RelayCommand(() => { GetCatalogs(); }, () => true);

            EditCatalogCommand = new RelayCommand(() => { Edit(); }, () => true);

        }

        public VMCatalogList(IModel model)

        {

            this.model = model;

            _catalogs = new ObservableCollection<VMCatalog>();

            AddCatalogCommand = new RelayCommand(() => { Add(); }, () => true);

            RemoveCatalogCommand = new RelayCommand(() => { Delete(); }, () => true);

            UpdateCatalogCommand = new RelayCommand(() => { GetCatalogs(); }, () => true);

            EditCatalogCommand = new RelayCommand(() => { Edit(); }, () => true);

        }



        public VMCatalog SelectedCatalog

        {

            get

            {

                if (_catalog == null)

                {

                    _catalog = new VMCatalog();

                    OnPropertyChanged(nameof(SelectedCatalog));

                }

                return _catalog;

            }

            set

            {

                if (_catalog != value && value != null)

                {

                    _catalog = value;



                    CatalogId = _catalog.CatalogId;

                    Title = _catalog.Title;

                    Author = _catalog.Author;

                    NrOfPages = _catalog.NrOfPages;





                    OnPropertyChanged(nameof(SelectedCatalog));

                    OnPropertyChanged(nameof(catalogId));

                    OnPropertyChanged(nameof(title));

                    OnPropertyChanged(nameof(author));

                    OnPropertyChanged(nameof(nrOfPages));

                }

            }

        }

        public ObservableCollection<VMCatalog> Catalogs

        {

            get => _catalogs;

            set

            {

                _catalogs = value;

                OnPropertyChanged(nameof(Catalogs));

            }

        }

        public IModelCatalog ModelCatalog

        {

            get => _modelCatalog;

            set

            {

                _modelCatalog = value;

                OnPropertyChanged(nameof(ModelCatalog));

                _catalog = new VMCatalog(CatalogId, Title, Author, NrOfPages);

            }

        }

        public int catalogId

        {

            get => CatalogId;

            set

            {

                CatalogId = value;

                OnPropertyChanged(nameof(catalogId));

            }

        }

        public string title

        {

            get => Title;

            set

            {

                Title = value;

                OnPropertyChanged(nameof(title));

            }

        }

        public string author

        {

            get => Author;

            set

            {

                Author = value;

                OnPropertyChanged(nameof(author));

            }

        }

        public int nrOfPages

        {

            get => NrOfPages;

            set

            {

                NrOfPages = value;

                OnPropertyChanged(nameof(nrOfPages));

            }

        }

        private VMCatalog? CatalogToPresentationLayer(IModelCatalog catalog)

        {

            if (catalog == null)

                return null;

            return new VMCatalog(catalog.CatalogId, catalog.Title, catalog.Author, catalog.NrOfPages);

        }

        public void GetCatalogs()

        {

            var catalogs = model.GetAllCatalogsAsync();

            Catalogs.Clear();

            foreach (var catalog in catalogs)

            {

                Catalogs.Add(CatalogToPresentationLayer(catalog));

            }

            OnPropertyChanged(nameof(Catalogs));

        }

        private async Task Add()

        {

            await model.AddCatalogAsync(CatalogId, Title, Author, NrOfPages);

        }

        private async Task Delete()

        {

            await model.RemoveCatalogAsync(_catalog.CatalogId);

        }

        private async Task Edit()

        {

            await model.RemoveCatalogAsync(_catalog.CatalogId);

            await Task.Delay(200);

            await model.AddCatalogAsync(CatalogId, Title, Author, NrOfPages);

        }



     

    }

    public class VMUserList : PropertyChange

    {

        private int UserId;

        private string FirstName;

        private string LastName;

        private VMUser _user;

        private IModelUser _modelUser;

        private IModel model;

        public ICommand AddUserCommand { get; }

        public ICommand RemoveUserCommand { get; }

        public ICommand UpdateUserCommand { get; }

        public ICommand EditUserCommand { get; }

        private ObservableCollection<VMUser> _users;

        public VMUserList()

        {

            this.model = new Model();

            _users = new ObservableCollection<VMUser>();

            AddUserCommand = new RelayCommand(() => { Add(); },  () => true);

            RemoveUserCommand = new RelayCommand(() => { Delete(); }, () => true);

            UpdateUserCommand = new RelayCommand(() => { GetUsers(); }, () => true);

            EditUserCommand = new RelayCommand(() => { Edit(); }, () => true);

        }

        public VMUserList(IModel model)

        {

            this.model = model;

            _users = new ObservableCollection<VMUser>();

            AddUserCommand = new RelayCommand(() => { Add(); }, () => true);

            RemoveUserCommand = new RelayCommand(() => { Delete(); }, () => true);

            UpdateUserCommand = new RelayCommand(() => { GetUsers(); }, () => true);

            EditUserCommand = new RelayCommand(() => { Edit(); }, () => true);

        }

        

        public ObservableCollection<VMUser> Users

        {

            get => _users;

            set

            {

                _users = value;

                OnPropertyChanged(nameof(Users));

            }

        }

        public VMUser SelectedUser

        {

            get

            {

                if (_user == null)

                {

                    _user = new VMUser();

                    OnPropertyChanged(nameof(SelectedUser));

                }

                return _user;

            }

            set

            {

                if (_user != value && value != null)

                {

                    _user = value;

                    UserId = _user.UserId;

                    FirstName = _user.FirstName;

                    LastName = _user.LastName;



                    OnPropertyChanged(nameof(userId));

                    OnPropertyChanged(nameof(firstName));

                    OnPropertyChanged(nameof(lastName));



                    OnPropertyChanged(nameof(SelectedUser));

                }

            }

        }

        public IModelUser ModelUser

        {

            get => _modelUser;

            set

            {

                _modelUser = value;

                OnPropertyChanged(nameof(ModelUser));

                _user = new VMUser(UserId, FirstName, LastName);

            }

        }

        public int userId

        {

            get => UserId;

            set

            {

                UserId = value;

                OnPropertyChanged(nameof(userId));

            }

        }

        public string firstName

        {

            get => FirstName;

            set

            {

                FirstName = value;

                OnPropertyChanged(nameof(firstName));

            }

        }

        public string lastName

        {

            get => LastName;

            set

            {

                LastName = value;

                OnPropertyChanged(nameof(lastName));

            }

        }

        private VMUser? UserToPresentationLayer(IModelUser user)

        {

            if (user == null)

                return null;

            return new VMUser(user.UserId, user.FirstName, user.LastName);

        }

        public void GetUsers()

        {

            var users = model.GetAllUsersAsync();

            Users.Clear();

            foreach (var user in users)

            {

                Users.Add(UserToPresentationLayer(user));

            }

            OnPropertyChanged(nameof(Users));

        }

        private async Task Add()

        {

            await model.AddUserAsync(UserId, FirstName, LastName);

        }

        private async Task Delete()

        {

            await model.RemoveUserAsync(_user.UserId);

        }

        private async Task Edit()

        {

            await model.RemoveUserAsync(_user.UserId);

            await Task.Delay(200);

            await model.AddUserAsync(UserId, FirstName, LastName);

        }

    }

    public class VMEventList : PropertyChange

    {

        private int EventId;

        private int EmployeeId;

        private int StateId;

        private bool Addition;

        private int UserId;

        private bool Borrowing;

        private VMEvent _event;

        private IModelEvent _modelEvent;

        private IModel model;

        public ICommand AddEventCommand { get; }

        public ICommand RemoveEventCommand { get; }

        public ICommand UpdateEventCommand { get; }



        public ICommand EditEventCommand { get; }

        private ObservableCollection<VMEvent> _events;

        public VMEventList()    

        {

            this.model = new Model();

            _events = new ObservableCollection<VMEvent>();

            AddEventCommand = new RelayCommand(() => { Add(); }, () => true);

            RemoveEventCommand = new RelayCommand(() => { Delete(); }, () => true);

            UpdateEventCommand = new RelayCommand(() => { GetEvents(); }, () => true);

            EditEventCommand = new RelayCommand(() => { Edit(); }, () => true);

        }

        public VMEventList(IModel model)

        {

            this.model = model;

            _events = new ObservableCollection<VMEvent>();

            AddEventCommand = new RelayCommand(() => { Add(); }, () => true);

            RemoveEventCommand = new RelayCommand(() => { Delete(); }, () => true);

            UpdateEventCommand = new RelayCommand(() => { GetEvents(); }, () => true);

            EditEventCommand = new RelayCommand(() => { Edit(); }, () => true);

        }

        public VMEvent SelectedEvent

        {

            get

            {

                if (_event == null)

                {

                    _event = new VMEvent();

                    OnPropertyChanged(nameof(SelectedEvent));

                }

                return _event;

            }

            set

            {

                if (_event != value && value!=null)

                {

                    _event = value;

                    EventId = _event.EventId;

                    EmployeeId = _event.EmployeeId;

                    StateId = _event.StateId;

                    Addition = _event.Addition;

                    UserId = _event.UserId;

                    Borrowing = _event.Borrowing;

                    OnPropertyChanged(nameof(eventId));

                    OnPropertyChanged(nameof(employee));

                    OnPropertyChanged(nameof(state));

                    OnPropertyChanged(nameof(addition));

                    OnPropertyChanged(nameof(user));

                    OnPropertyChanged(nameof(borrowing));





                    OnPropertyChanged(nameof(SelectedEvent));

                }

            }

        }

        public ObservableCollection<VMEvent> Events

        {

            get => _events;

            set

            {

                _events = value;

                OnPropertyChanged(nameof(Events));

            }

        }

        public IModelEvent ModelEvent

        {

            get => _modelEvent;

            set

            {

                _modelEvent = value;

                OnPropertyChanged(nameof(ModelEvent));

            }

        }

        public int eventId

        {

            get => EventId;

            set

            {

                EventId = value;

                OnPropertyChanged(nameof(eventId));

            }

        }

        public int employee

        {

            get => EmployeeId;

            set

            {

                EmployeeId = value;

                OnPropertyChanged(nameof(employee));

            }

        }

        public int state

        {

            get => StateId;

            set

            {

                StateId = value;

                OnPropertyChanged(nameof(state));

            }

        }

        public bool addition

        {

            get => Addition;

            set

            {

                Addition = value;

                OnPropertyChanged(nameof(addition));

            }

        }

        public int user

        {

            get => UserId;

            set

            {

                UserId = value;

                OnPropertyChanged(nameof(user));

            }

        }

        public bool borrowing

        {

            get => Borrowing;

            set

            {

                Borrowing = value;

                OnPropertyChanged(nameof(borrowing));

            }

        }

        

        private VMEvent? EventToPresentationLayer(IModelEvent eventObj)

        {

            if (eventObj == null)

                return null;

            return new VMEvent(eventObj.EventId, eventObj.Employee, eventObj.State, eventObj.User, eventObj.Borrowing, eventObj.Addition);

        }

        public void GetEvents()

        {

            var events = model.GetAllEventsAsync();

            Events.Clear();

            foreach (var eventObj in events)

            {

                Events.Add(EventToPresentationLayer(eventObj));

            }

            OnPropertyChanged(nameof(Events));

        }

       

        

        private async Task Add()

        {

            if (UserId != 0)

            {

                await model.AddUserEventAsync(EventId, EmployeeId, StateId, UserId, Borrowing);

            }

            else

            {

                await model.AddDatabaseEventAsync(EventId, EmployeeId, StateId, Addition);

            }

        }



        private async Task Delete()

        {

            await model.RemoveEventAsync(_event.EventId);

        }

        private async Task Edit()

        {

            await model.RemoveEventAsync(_event.EventId);

            await Task.Delay(200);

            if (UserId != 0)

            {

                await model.AddUserEventAsync(EventId, EmployeeId, StateId, UserId, Borrowing);

            }

            else

            {

                await model.AddDatabaseEventAsync(EventId, EmployeeId, StateId, Addition);

            }

        }

    }

    public class VMStateList : PropertyChange

    {

        private int StateId;

        private int NrOfBooks;

        private int CatalogId;

        private VMState _state;

        private IModelState _modelState;

        private IModel model;

        public ICommand AddStateCommand { get; }

        public ICommand RemoveStateCommand { get; }

        public ICommand UpdateStateCommand { get; }

        public ICommand EditStateCommand { get; }

        private ObservableCollection<VMState> _states;

        public VMStateList()

        {

            this.model = new Model();

            _states = new ObservableCollection<VMState>();

            AddStateCommand = new RelayCommand(() => { Add(); }, () => true);

            RemoveStateCommand = new RelayCommand(() => { Delete(); }, () => true);

            UpdateStateCommand = new RelayCommand(() => { GetStates(); }, () => true);

            EditStateCommand = new RelayCommand(() => { Edit(); }, () => true);

        }

        public VMStateList(IModel model)

        {

            this.model = model;

            _states = new ObservableCollection<VMState>();

            AddStateCommand = new RelayCommand(() => { Add(); }, () => true);

            RemoveStateCommand = new RelayCommand(() => { Delete(); }, () => true);

            UpdateStateCommand = new RelayCommand(() => { GetStates(); }, () => true);

            EditStateCommand = new RelayCommand(() => { Edit(); }, () => true);

        }

        public VMState SelectedState

        {

            get

            {

                if (_state == null)

                {

                    _state = new VMState();

                    OnPropertyChanged(nameof(SelectedState));

                }

                return _state;

            }

            set

            {

                if (_state != value && value != null)

                {

                    _state = value;

                    StateId = _state.StateId;

                    NrOfBooks = _state.NrOfBooks;

                    CatalogId = _state.CatalogId;



                    OnPropertyChanged(nameof(stateId));

                    OnPropertyChanged(nameof(nrOfBooks));

                    OnPropertyChanged(nameof(catalogId));

                    OnPropertyChanged(nameof(SelectedState));

                }

            }

        }



        public ObservableCollection<VMState> States

        {

            get => _states;

            set

            {

                _states = value;

                OnPropertyChanged(nameof(States));

            }

        }

        public IModelState ModelState

        {

            get => _modelState;

            set

            {

                _modelState = value;

                OnPropertyChanged(nameof(ModelState));

            }

        }



        public int stateId

        {

            get => StateId;

            set

            {

                StateId = value;

                OnPropertyChanged(nameof(stateId));

            }

        }

        public int nrOfBooks

        {

            get => NrOfBooks;

            set

            {

                NrOfBooks = value;

                OnPropertyChanged(nameof(nrOfBooks));

            }

        }

        public int catalogId

        {

            get => CatalogId;

            set

            {

                CatalogId = value;

                OnPropertyChanged(nameof(catalogId));

            }

        }

       

        private VMState? CatalogToPresentationLayer(IModelState state)

        {

            if (state == null)

                return null;

            return new VMState(state.StateId,state.NrOfBooks, (state.Catalog));

        }

        public void GetStates()

        {

            var states = model.GetAllStatesAsync();

            States.Clear();

            foreach (var state in states)

            {

                States.Add(CatalogToPresentationLayer(state));

            }

            OnPropertyChanged(nameof(States));

        }

        private async Task Add()

        {

            await model.AddStateAsync(StateId, NrOfBooks, CatalogId);

        }

        private async Task Delete()

        {

            await model.RemoveStateAsync(_state.StateId);

        }

        private async Task Edit()

        {

            await model.RemoveStateAsync(_state.StateId);

            await Task.Delay(200);

            await model.AddStateAsync(StateId, NrOfBooks, CatalogId);

        }



    }

    }