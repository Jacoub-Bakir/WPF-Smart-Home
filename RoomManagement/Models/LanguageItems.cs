using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Sdl.MultiSelectComboBox.API;
using RoomManagement.Commands;
using RoomManagement.Services;
using Size = System.Drawing.Size;

namespace RoomManagement.Models
{
    public class LanguageItems : INotifyPropertyChanged
    {
        private ObservableCollection<LanguageItem> _items;
        private ObservableCollection<LanguageItem> _selectedItems;
        private bool _toggleCustomStyle;
        private bool _enableAlternateItems;
        private bool _listenToFilterTextChanged;
        private bool _listenToSelectedItemsChanged;
        private bool _enableAutoComplete;
        private bool _enableBatchSelection;
        private bool _enableFiltering;
        private bool _useCustomFilterService;
        private bool _enableSuggestionProvider;
        private bool _isEditable;
        private bool _clearFilterOnDropdownClosing;
        private bool _clearSelectionOnFilterChanged;
        private string _selectionMode;
        private List<LanguageItem> _allItems;

		public LanguageItems()
		{
			ListenToFilterTextChanged = true;
			ListenToSelectedItemsChanged = true;

			SelectionMode = "Multiple";
			IsEditable = true;

			ClearFilterOnDropdownClosing = true;

			EnableFiltering = true;
			UseCustomFilterService = true;

			

			EnableAutoComplete = true;
			EnableBatchSelection = true;

			FilterTextChangedCommand = new RoomManagement.Commands.FilterTextChangedCommand(UpdateEventLog, CanListenToFilterTextChangedExample);
			SelectedItemsChangedCommand = new RoomManagement.Commands.SelectedItemsChangedCommand(UpdateEventLog, UpdateSelectedItems);
			ClearLogCommand = new RoomManagement.Commands.ClearLogCommand(UpdateEventLog);
			ClearItemsCommand = new RoomManagement.Commands.ClearSelectedItemsCommand(UpdateEventLog);
			StyleChangedCommand = new RoomManagement.Commands.StyleChangedCommand(UpdateEventLog);
			SelectRandomItemsCommand = new RoomManagement.Commands.SelectRandomItemsCommand(SelectRandomItems);


			InitializeItemsCollection();
			EnableSuggestionProvider = true;
		}

        public ICommand FilterTextChangedCommand { get; }

        public ICommand SelectedItemsChangedCommand { get; }

        public ICommand StyleChangedCommand { get; }

        public ICommand ClearLogCommand { get; }

        public ICommand ClearItemsCommand { get; }

        public ICommand SelectRandomItemsCommand { get; }

        public IFilterService FilterService { get; set; }


        public ObservableCollection<LanguageItem> Items
        {
            get => _items ?? (_items = new ObservableCollection<LanguageItem>());
            set => _items = value;
        }

        public ObservableCollection<LanguageItem> SelectedItems
        {
            get => _selectedItems ?? (_selectedItems = new ObservableCollection<LanguageItem>());
            set
            {
                _selectedItems = value;

                OnPropertyChanged(nameof(SelectedItems));
            }
        }

        public int SelectedItemsCount { get; set; }

        public ISuggestionProvider CustomSuggestionProvider { get; private set; }

        public string EventLog { get; set; }

        public bool ToggleCustomStyle
        {
            get => _toggleCustomStyle;
            set
            {
                if (_toggleCustomStyle.Equals(value))
                {
                    return;
                }

                _toggleCustomStyle = value;

                UpdateEventLog(nameof(EnableAlternateItems), _toggleCustomStyle.ToString());

                OnPropertyChanged(nameof(ToggleCustomStyle));
            }
        }

        public bool EnableAutoComplete
		{
			get => _enableAutoComplete;
			set
			{
				if (_enableAutoComplete.Equals(value))
				{
					return;
				}

				_enableAutoComplete = value;

				UpdateEventLog(nameof(EnableAutoComplete), _enableAutoComplete.ToString());

				OnPropertyChanged(nameof(EnableAutoComplete));
			}
        }

        public bool EnableBatchSelection
		{
			get => _enableBatchSelection;
			set
			{
				if (_enableBatchSelection.Equals(value))
				{
					return;
				}

				_enableBatchSelection = value;

				UpdateEventLog(nameof(EnableBatchSelection), _enableBatchSelection.ToString());

				OnPropertyChanged(nameof(EnableBatchSelection));
			}
        }

        public bool EnableAlternateItems
		{
			get => _enableAlternateItems;
			set
			{
				if (_enableAlternateItems.Equals(value))
				{
					return;
				}

				_enableAlternateItems = value;

				UpdateEventLog(nameof(EnableAlternateItems), _enableAlternateItems.ToString());

				ApplyEnableAlternateItems();

				OnPropertyChanged(nameof(Items));
			}
        }

        private void ApplyEnableAlternateItems()
		{
			var isEnableAware = Items != null && Items.Count > 0 && Items[0] is IItemEnabledAware;
			if (!isEnableAware)
			{
				return;
			}

			for (var i = 0; i < Items.Count; i++)
			{
				if (Items[i] is IItemEnabledAware enableAwareItem)
				{
					enableAwareItem.IsEnabled = !_enableAlternateItems || i % 2 != 0;
					if (!enableAwareItem.IsEnabled)
					{
						if (SelectedItems.Contains(Items[i]))
						{
							SelectedItems.Remove(Items[i]);
						}
					}
				}
			}
        }

        public void SelectRandomItems(int count)
		{
			if (count <= 0)
			{
				return;
			}

			UpdateEventLog(nameof(SelectRandomItems), count.ToString());

			var min = 0;
			var max = Items.Count - 1;
			var randomNumber = new Random();

			if (count > Items.Count - 1)
			{
				count = Items.Count - 1;
			}

			var randomIndexes = new List<int>();
			while (randomIndexes.Count < count)
			{
				var randomIndex = randomNumber.Next(min, max);
				if (!randomIndexes.Contains(randomIndex))
				{
					randomIndexes.Add(randomIndex);
				}
			}

			foreach (var item in randomIndexes)
			{
				SelectedItems.Add(Items[item]);
			}

			ApplyEnableAlternateItems();

			OnPropertyChanged(nameof(SelectedItems));
        }

        public bool ListenToFilterTextChanged
		{
			get => _listenToFilterTextChanged;
			set
			{
				if (_listenToFilterTextChanged.Equals(value))
				{
					return;
				}

				_listenToFilterTextChanged = value;

				UpdateEventLog(nameof(ListenToFilterTextChanged), _listenToFilterTextChanged.ToString());

				OnPropertyChanged(nameof(ListenToFilterTextChanged));
			}
        }

        public bool ListenToSelectedItemsChanged
		{
			get => _listenToSelectedItemsChanged;
			set
			{
				if (_listenToSelectedItemsChanged.Equals(value))
				{
					return;
				}

				_listenToSelectedItemsChanged = value;

				UpdateEventLog(nameof(ListenToSelectedItemsChanged), _listenToSelectedItemsChanged.ToString());

				OnPropertyChanged(nameof(ListenToSelectedItemsChanged));
			}
        }

		public bool EnableFiltering
		{
			get => _enableFiltering;
			set
			{
				if (_enableFiltering.Equals(value))
				{
					return;
				}

				_enableFiltering = value;

				UpdateEventLog(nameof(EnableFiltering), _enableFiltering.ToString());

				OnPropertyChanged(nameof(EnableFiltering));
				OnPropertyChanged(nameof(UseCustomFilterService));
			}
		}

        public bool EnableSuggestionProvider
        {
            get => _enableSuggestionProvider;
            set
            {
                if (_enableSuggestionProvider.Equals(value))
                {
                    return;
                }

                _enableSuggestionProvider = value;

                UpdateEventLog(nameof(EnableSuggestionProvider), _enableSuggestionProvider.ToString());

                if (_enableSuggestionProvider)
                {
                    CustomSuggestionProvider = new CustomSuggestionProvider(Items, _allItems);
                }
                else
                {
                    CustomSuggestionProvider = null;
                    Items.Clear();
                    _allItems.ForEach(x => Items.Add(x));
                }

                OnPropertyChanged(nameof(EnableSuggestionProvider));
                OnPropertyChanged(nameof(CustomSuggestionProvider));
            }
        }

        public bool IsEditable
		{
			get => _isEditable;
			set
			{
				if (_isEditable.Equals(value))
				{
					return;
				}

				_isEditable = value;

				UpdateEventLog(nameof(IsEditable), _isEditable.ToString());

				OnPropertyChanged(nameof(IsEditable));
			}
        }

        public bool ClearFilterOnDropdownClosing
		{
			get => _clearFilterOnDropdownClosing;
			set
			{
				if (_clearFilterOnDropdownClosing.Equals(value))
				{
					return;
				}

				_clearFilterOnDropdownClosing = value;

				UpdateEventLog(nameof(ClearFilterOnDropdownClosing), _clearFilterOnDropdownClosing.ToString());
				OnPropertyChanged(nameof(ClearFilterOnDropdownClosing));
			}
        }

        public bool ClearSelectionOnFilterChanged
		{
			get => _clearSelectionOnFilterChanged;
			set
			{
				if (_clearSelectionOnFilterChanged == value)
				{
					return;
				}

				_clearSelectionOnFilterChanged = value;
				UpdateEventLog(nameof(ClearSelectionOnFilterChanged), _clearSelectionOnFilterChanged.ToString());
				OnPropertyChanged(nameof(ClearSelectionOnFilterChanged));				
			}
		}

        public List<string> SelectionModes => new List<string> { "Multiple", "Single" };

        public string SelectionMode
		{
			get => _selectionMode;
			set
			{
				if (string.Compare(_selectionMode, value, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return;
				}

				_selectionMode = value;

				UpdateEventLog(nameof(SelectionMode), _selectionMode);

				OnPropertyChanged(nameof(SelectionMode));
			}
        }

        public bool UseCustomFilterService
		{
			get => _useCustomFilterService;
			set
			{
				if (_useCustomFilterService.Equals(value))
				{
					return;
				}

				_useCustomFilterService = value;

				UpdateEventLog(nameof(UseCustomFilterService), _useCustomFilterService.ToString());

				FilterService = _useCustomFilterService ? new CustomFilterService() : null;

				OnPropertyChanged(nameof(EnableFiltering));
				OnPropertyChanged(nameof(UseCustomFilterService));
				OnPropertyChanged(nameof(FilterService));
			}
        }

        private void UpdateSelectedItemsCount(int count)
		{
			SelectedItemsCount = count;

			OnPropertyChanged(nameof(SelectedItemsCount));
        }

        private void UpdateEventLog(string action, string text)
		{
			if (action == "Clear log")
			{
				EventLog = string.Empty;
			}

			if (action == "Clear items")
			{
				SelectedItems.Clear();
				OnPropertyChanged(nameof(SelectedItems));
			}

			if (action == "Style Changed")
			{
				OnPropertyChanged(nameof(SelectedItems));
				OnPropertyChanged(nameof(Items));
			}

			EventLog += action + " => " + text + "\r\n";
			OnPropertyChanged(nameof(EventLog));
        }

        private void UpdateSelectedItems(ICollection selected)
	{			
		foreach (var item in _items)
		{
			var selectedItemIndex = selected.Cast<LanguageItem>().ToList().IndexOf(item);
			item.SelectedOrder = selectedItemIndex > -1 ? selectedItemIndex + 1 : -1;
		}

		UpdateSelectedItemsCount(selected.Count);
        }

        private bool CanListenToFilterTextChangedExample()
		{
			return ListenToFilterTextChanged;
        }

        private bool CanListenToSelectedItemsChangedExample()
		{
			return ListenToSelectedItemsChanged;
        }

		private void InitializeItemsCollection()
		{
			var imageSize24 = new Size(24, 24);
			var items = new List<LanguageItem>();

			List<RemoteInerfaces.Entities.Account> accounts = new List<RemoteInerfaces.Entities.Account>();
			RemoteInerfaces.Services.IAccountService obj = (RemoteInerfaces.Services.IAccountService)Activator.GetObject(typeof(RemoteInerfaces.Services.IAccountService), "tcp://localhost:8080/AccountService");

			accounts = (List<RemoteInerfaces.Entities.Account>) obj.getHouseAccount(1);
            foreach (var account in accounts)
            {
				Console.WriteLine("user " + account.First_Name);
                var imageSize = imageSize24;
                var image = ImageService.GetImage(account.profile_image, imageSize24);

                if (image == null)
                {
                    // use Height to have a consistent item container height, whether or not the image is null
                    //imageSize = new Size(0, imageSize.Height);

                    continue;
                }

                items.Add(new LanguageItem
                {
                    Id = account.Id,
                    Name = account.First_Name,
                    Image = image,
                    ImageSize = imageSize
                });

            }

            _allItems = new List<LanguageItem>(items.OrderBy(a => a.Name));
		}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
