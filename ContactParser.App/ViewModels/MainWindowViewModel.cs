using ContactParser.App.Helpers;
using ContactParser.App.Models;
using ContactParser.App.Services;

namespace ContactParser.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Properties
        /// <summary>
        /// Holds the value for the input field
        /// </summary>
        private string _inputField;

        /// <summary>
        /// Property of <see cref="_inputField"/>
        /// </summary>
        public string InputField
        {
            get { return _inputField; }
            set
            {
                _inputField = value;
                OnPropertyChanged(nameof(InputField));
            }
        }

        /// <summary>
        /// Holds the value for the retrieved gender
        /// </summary>
        private string _gender;

        /// <summary>
        /// Property of <see cref="_gender"/>
        /// </summary>
        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        /// <summary>
        /// Holds the value for the input field
        /// </summary>
        private string _salutation;

        /// <summary>
        /// Property of <see cref="_salutation"/>
        /// </summary>
        public string Salutation
        {
            get { return _salutation; }
            set
            {
                _salutation = value;
                OnPropertyChanged(nameof(Salutation));
            }
        }

        /// <summary>
        /// Holds the value of the title
        /// </summary>
        private string _title;

        /// <summary>
        /// Property of <see cref="_title"/>
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        /// <summary>
        /// Holds the value of the first name
        /// </summary>
        private string _firstName;

        /// <summary>
        /// Property of <see cref="_firstName"/>
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        /// <summary>
        /// Holds the value of the last name
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Property of <see cref="_lastName"/>
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        /// <summary>
        /// Holds the value for the new title
        /// </summary>
        private string _newTitle;

        /// <summary>
        /// Property of <see cref="_newTitle"/>
        /// </summary>
        public string NewTitle
        {
            get { return _newTitle; }
            set
            {
                _newTitle = value;
                OnPropertyChanged(nameof(NewTitle));
            }
        }

        /// <summary>
        /// Holds the value for the concatenated name
        /// </summary>
        private string _concatenatedName;

        /// <summary>
        /// Property of <see cref="_concatenatedName"/>
        /// </summary>
        public string ConcatenatedName
        {
            get { return _concatenatedName; }
            set
            {
                _concatenatedName = value;
                OnPropertyChanged(nameof(ConcatenatedName));
            }
        }

        /// <summary>
        /// Holds the button handlers for the add new title button
        /// </summary>
        public RelayCommand AddTitleCommand { get; set; }

        /// <summary>
        /// Holds the button handlers for the parse button
        /// </summary>
        public RelayCommand ParseCommand { get; set; }

        /// <summary>
        /// Holds the button handlers for the reset button
        /// </summary>
        public RelayCommand ResetCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor that initializes the command properties for the buttons
        /// </summary>
        public MainWindowViewModel()
        {
            AddTitleCommand = new RelayCommand(ExecuteAddTitle, CanExecuteAddTitle);
            ResetCommand = new RelayCommand(ExecuteReset, CanExecuteReset);
            ParseCommand = new RelayCommand(ExecuteParse, CanExecuteParse);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Enables the add new title button when the new title input is not empty
        /// </summary>
        /// <param name="parameter">Parameter to submit some data</param>
        /// <returns>True when the new title is not empty, otherwise false</returns>
        public bool CanExecuteAddTitle(object parameter) => !string.IsNullOrEmpty(NewTitle);

        /// <summary>
        /// Button handler that adds a new title
        /// </summary>
        /// <param name="parameter">Parameter to submit some data</param>
        public void ExecuteAddTitle(object parameter)
        {

        }

        /// <summary>
        /// Enables the reset button
        /// </summary>
        /// <param name="parameter">Parameter to submit some data</param>
        /// <returns>true</returns>
        public bool CanExecuteReset(object parameter) => true;

        /// <summary>
        /// Resets all text fields
        /// </summary>
        /// <param name="parameter">Parameter to submit some data</param>
        public void ExecuteReset(object parameter)
        {
            InputField = string.Empty;
            Gender = string.Empty;
            Salutation = string.Empty;
            Title = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            NewTitle = string.Empty;
            ConcatenatedName = string.Empty;
        }

        /// <summary>
        /// Enables the parse button when the input field is filled out
        /// </summary>
        /// <param name="parameter">Parameter to submit some data</param>
        /// <returns>True when the input field is not empty, otherwise false</returns>
        public bool CanExecuteParse(object parameter) => !string.IsNullOrEmpty(InputField);

        /// <summary>
        /// Button handler that parses the text in the input field
        /// </summary>
        /// <param name="parameter">Parameter to submit some data</param>
        public void ExecuteParse(object parameter)
        {
            Name parsedName = NameParser.ParseName(InputField);

            Gender = parsedName.Gender;
            FirstName = parsedName.FirstName;
            LastName = parsedName.LastName;
            Title = parsedName.Title;
            Salutation = parsedName.Salutation;
            ConcatenatedName = parsedName.Greeting;
        }
        #endregion
    }
}
