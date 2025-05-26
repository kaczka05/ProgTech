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

        public ICommand UpdateViemCommand { get; }


        public ViewModel()
        {
            UpdateViemCommand = new RelayCommand(ChangeView);

            SelectedVM = new VMCatalogList();
        }

        private void ChangeView(object obj)
        {
            if (obj is string viewName)
            {
                switch (viewName)
                {
                    case "Catalog":
                        SelectedVM = new VMCatalogList();
                        break;
                    case "User":
                        SelectedVM = new VMUserList();
                        break;
                    case "Event":
                        SelectedVM = new VMEventList();
                        break;
                    case "State":
                        SelectedVM = new VMStateList();
                        break;
                    default:
                        break;
                }
            }
        }


    }
}
