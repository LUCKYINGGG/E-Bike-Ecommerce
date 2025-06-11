using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesReturnsSystem.ViewModels;
using SalesReturnsSystem.BLL;
using SalesReturnsSystem.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ProjectWebApp.Data;

namespace ProjectWebApp.Components.Pages.SalesReturn
{
    public partial class EBikesSale
    {
        #region Class Fields
        private string _phoneNumber = string.Empty;

        private CustomerSalesSearchView _selectedCustomer = new();
        private List<CustomerSalesSearchView> _selectedCustomers = new();

        private CategoriesView _category = new();
        private List<CategoriesView> _listOfCategories = new();

        private CustomerPartsView _saleDetail = new();
        private List<CustomerPartsView> _saleDetails = new();

        private PartsView _part = new();
        private List<PartsView> _listOfParts = new();
        private List<PartsView> _selectedParts = new();

        private List<PartsView> _parts = new();

        private SalesView _sale = new();

        private MudForm _customerForm = new();

        private bool _noResults;

        #endregion

        #region Validation
        
        private bool _isFormValid;

        private bool _hasDataChanged = false;
        
        private string _feedbackMessage = string.Empty;

        private string? _errorMessage = string.Empty;

        private List<string> _errorDetails = new();

        private bool HasFeedbackMessage => !string.IsNullOrWhiteSpace(_feedbackMessage);

        private bool HasErrorMessage => !string.IsNullOrWhiteSpace(_errorMessage);
        #endregion

        #region Properties

        [Inject]
        protected CustomerServices CustomerServices { get; set; } = default!;
        [Inject]
        protected PartsServices PartsServices { get; set; } = default!;
        [Inject]
        protected CustomerSaleServices CustomerSaleServices { get; set; } = default!;
        [Inject]
        protected SalesServices SalesServices { get; set; } = default!;

        [Inject]
        protected RetrieveCategories RetrieveCategories { get; set; } = default!;

        protected List<CustomerSalesSearchView> Customers { get; set; } = new();

        protected CustomerPartsView Parts { get; set; } = default!;

        [Inject]
        protected IDialogService DialogService { get; set; } = default!;
        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        protected UserManager<ApplicationUser> UserManager { get; set; } = default!;


        #endregion

        #region Authentication and Security
        private string? userId;
        private string? employeeName;

        private List<string> roles = new();


        #endregion

        #region Class Methods

        protected override async Task OnInitializedAsync()
        {
            #region Auth and Security Methods


            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {

                userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var userName = await UserManager.FindByIdAsync(userId);

                employeeName = userName.FirstName + " " + userName.LastName;
                roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            }
            #endregion
        }
        private void Search()
        {
            try
            {
                _noResults = false;

                _errorDetails.Clear();

                _errorMessage = string.Empty;

                _feedbackMessage = string.Empty;

                Customers.Clear();

                if (string.IsNullOrWhiteSpace(_phoneNumber))
                {
                    throw new ArgumentNullException("Phone number is required.");
                }

                Customers = CustomerServices.GetCustomers(_phoneNumber);

                if (Customers.Count > 0)
                {
                    _feedbackMessage = $"{Customers.Count} customers found.";
                }
                else
                {
                    _errorMessage = "No customers found.";
                    _noResults = true;
                }
            }
            catch (ArgumentNullException ex)
            {
                _errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (ArgumentException ex)
            {
                _errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (AggregateException ex)
            {
                if (!string.IsNullOrEmpty(_errorMessage))
                {
                    _errorMessage = $"{_errorMessage} {Environment.NewLine}";
                }

                _errorMessage = $"{_errorMessage} Unable to search for customers.";

                foreach (var error in ex.InnerExceptions)
                {
                    _errorDetails.Add(error.Message);
                }

            }
        }

        private void CustomerClear()
        {
            _feedbackMessage = string.Empty;
            _errorMessage = string.Empty;

            _phoneNumber = string.Empty;
            Customers.Clear();
            _listOfParts.Clear();
            _parts.Clear();

            _feedbackMessage = $"The phone number has been cleared.";
        }

        private void SelectCustomer(int customerId)
        {
            try
            {
                // reset feedback message
                _feedbackMessage = string.Empty;

                _selectedCustomer = CustomerServices.GetCustomer(customerId);
                _saleDetails = CustomerSaleServices.GetCustomerOrders();
                _listOfCategories = RetrieveCategories.GetCategories();

            }
            catch (ArgumentNullException ex)
            {
                _errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (ArgumentException ex)
            {
                _errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (AggregateException ex)
            {
                //  have a collection of errors
                //  each error should be place into a separate line
                if (!string.IsNullOrWhiteSpace(_errorMessage))
                {
                    _errorMessage = $"{_errorMessage}{Environment.NewLine}";
                }

                _errorMessage = $"{_errorMessage}Unable to search for customer";
                foreach (var error in ex.InnerExceptions)
                {
                    _errorDetails.Add(error.Message);
                }
            }
        }

        private void SelectPartCategory(int categoryID)
        {
            _category.CategoryID = categoryID;

            _listOfParts = PartsServices.GetParts(_category.CategoryID);
        }

        private void SelectParts(int partID)
        {
            try
            {
                // reset feedback message
                _feedbackMessage = string.Empty;

                _part.PartID = partID;

                _parts = PartsServices.GetParts(partID);


            }
            catch (ArgumentNullException ex)
            {
                _errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (ArgumentException ex)
            {
                _errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (AggregateException ex)
            {
                //  have a collection of errors
                //  each error should be place into a separate line
                if (!string.IsNullOrWhiteSpace(_errorMessage))
                {
                    _errorMessage = $"{_errorMessage}{Environment.NewLine}";
                }

                _errorMessage = $"{_errorMessage}Unable to search for customer";
                foreach (var error in ex.InnerExceptions)
                {
                    _errorDetails.Add(error.Message);
                }
            }
        }

        private void SelectQuantity()
        {
            try
            {
                
            }
            catch
            {
                
            }
        }

        private void AddPartsToCart()
        {

        }

        private void PartClear()
        {
            _listOfParts.Clear();
            _parts.Clear();
        }

        private void RedeemCoupon()
        {

        }

        private async Task Cancel()
        {
            bool? results = await DialogService.ShowMessageBox("Confirm Cancel", $"Do you wish to close the sales page? All unsaved changes will be lost.", yesText: "Yes", cancelText: "No");

            if (results == true)
            {
                //_customerForm.ResetTouched();
                NavigationManager.NavigateTo($"/Pages/EBikesSale", true);
                //NavigationManager.Refresh();

            }
        }

        #endregion
    }
}
