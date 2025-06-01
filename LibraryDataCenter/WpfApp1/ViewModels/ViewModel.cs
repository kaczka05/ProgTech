using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using LibraryPresentationLayer;

namespace LibraryPresentationLayer
{
    internal class ViewModel : PropertyChange
    {
        private static Model model = new Model();
        private VMCatalogList _vmCatalogList = new VMCatalogList(model);
        private VMUserList _vmUserList = new VMUserList(model);
        private VMEventList _vmEventList = new VMEventList(model);
        private VMStateList _vmStateList = new VMStateList(model);
        private PropertyChange _selectedVM;
        public PropertyChange SelectedVM
        {
            get => _selectedVM;
            set
            {
                if (SetProperty(ref _selectedVM, value))
                {
                    OnPropertyChanged(nameof(SelectedVM));
                }
            }
        }

        public ICommand UpdateVMCommand { get; }


        public ViewModel()
        {
            UpdateVMCommand = new RelayCommand(ChangeView);

            SelectedVM = _vmCatalogList;
        }

        private void ChangeView(object obj)
        {
            if (obj is string viewName)
            {
                switch (viewName)
                {
                    case "Catalog":
                        SelectedVM = _vmCatalogList;
                        break;
                    case "User":
                        SelectedVM = _vmUserList;
                        break;
                    case "Event":
                        SelectedVM = _vmEventList;
                        break;  
                    case "State":
                        SelectedVM = _vmStateList;
                        break;
                    default:
                        break;
                }
            }
        }


    }
}
