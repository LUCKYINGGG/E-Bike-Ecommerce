using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesReturnsSystem.Entities;
using ServicingSystem.BLL;
using ServicingSystem.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ProjectWebApp.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProjectWebApp.Components.Pages.Servicing
{
    public partial class Servicing
    {


        #region Fields
        private string _lastName = string.Empty;
        private int _selectedCustomer = 0;

        private CustomerVehicleView _customerVehicle = new();
        private List<CustomerVehicleView> _customerVehicles = new();

        private StandardJobsView _standardJob = new();
        private List<StandardJobsView> _standardJobs = new();

        private List<StandardJobsView> _serviceJobsGrid = new();

        private CategoryView _category = new();
        private List<CategoryView> _listOfCategories = new();

        private PartView _part = new();
        private List<PartView> _listOfParts = new();
        private List<PartView> _selectedParts = new();

        private CouponView _coupon = new();
        private CouponView _couponView;

        enum CouponStatus
        {
            Valid,
            Invalid,
            Default
        }
        private CouponStatus _status = CouponStatus.Default;


        private MudForm _customerForm = new();


        private bool _noRecords;


        #endregion


        #region Validation, Feedback & Error Messages 
        // flag to if the form is valid.
        private bool _isFormValid;
        //  flag if data has changed
        private bool _hasDataChanged = false;


        // The feedback message
        private string _feedbackMessage = string.Empty;

        // The error message
        private string? _errorMessage = string.Empty;

        // error details
        private List<string> _errorDetails = new();

        // has feedback
        private bool HasFeedbackMessage => !string.IsNullOrWhiteSpace(_feedbackMessage);

        // has error
        private bool HasErrorMessage => !string.IsNullOrWhiteSpace(_errorMessage);

        #endregion

        #region Properties

        //protected List<StandardJobsView> StandardJobs { get; set; } 


        [Inject]
        protected CustomerService CustomerService { get; set; } = default!;

        protected List<CustomerSearchView> Customers { get; set; } = new();

        [Inject]
        protected CustomerVehicleService CustomerVehicleService { get; set; } = default!;

        [Inject]
        protected StandardJobsService StandardJobsService { get; set; } = default!;

        [Inject]
        protected PartService PartService { get; set; } = default!;
        //protected List<PartView> PartViews { get; set; }
        //protected PartView Part { get; set; } = new();

        [Inject]
        protected CategoryService CategoryService { get; set; } = default!;

        [Inject]
        protected CouponService CouponService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        protected IDialogService DialogService { get; set; } = default!;

        [Inject] protected JobService JobService { get; set; } = default!;

        //[Inject] 
        //protected UserManager<ApplicationUser> UserManager { get; set; } = default!;

        #endregion

        #region Authentication and Security
        private string? userId;
        private string? employeeName;
     
        private List<string> roles = new();


        #endregion


        #region Method

        protected override async Task OnInitializedAsync()
        {
            #region Auth and Security Methods
          

            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                
                userId = user.FindFirst("EmployeeID")?.Value ?? "";

                //var userName = await UserManager.FindByIdAsync(userId);
                //employeeName = user.Claims.FirstOrDefault(x => x.Type == "FullName")?.Value;
                employeeName = user.FindFirst("FullName")?.Value ?? "Unknown User";

                //employeeName = userName.FirstName + " " + userName.LastName;
                roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            }
            else
            {
                userId = "Unknown";
                employeeName = "Unknown";
                roles = ["Unknown"];
            }

            #endregion

        }

        // Customer Search
        private void Search()
        {
            try
            {
                // reset the no records flag
                _noRecords = false;

                // reset the error detail list
                _errorDetails.Clear();

                // reset the error message to an empty string
                _errorMessage = string.Empty;

                // reset the feedback message to an empty string
                _feedbackMessage = string.Empty;

                // clear the customer list before we do our search
                Customers.Clear();

                if (string.IsNullOrEmpty(_lastName))
                {
                    throw new ArgumentException("Please provide either a last name");
                }

                Customers = CustomerService.GetCustomers(_lastName);

                if (Customers.Count > 0)
                {
                    _feedbackMessage = $"Search for customer(s) was successful";
                }
                else
                {
                    _feedbackMessage = $"No customers were found for your search criteria";
                    _noRecords = true;
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
                // have a collection of errors
                // each error should be place into a separate line
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

        private void LastnameSearchClear()
        {
            _feedbackMessage = String.Empty;
            _errorMessage = string.Empty;

            _lastName = string.Empty;
            Customers.Clear();
            _feedbackMessage = $"The customer lastname has been cleared.";

        }

        private async Task SelectCustomer(int customerid)
        {
            try
            {
                if (customerid == _selectedCustomer)
                {
                    return;
                }

                if (_selectedCustomer != 0)
                {
                    bool? results = await DialogService.ShowMessageBox("Confirm Cancel", $"Do you wish to reset data on the page? All unsaved changes will be lost.", yesText: "Yes", cancelText: "No");

                    if (results == true)
                    {

                        _customerVehicle.Vin = null;
                        _serviceJobsGrid.Clear();
                        _selectedParts.Clear();
                        _coupon = new();
                        _status = CouponStatus.Default;
                        _listOfParts.Clear();
                        _category = new();
                        _standardJob = new();

                    }
                }

                _selectedCustomer = customerid;

                // reset feedback message
                _feedbackMessage = string.Empty;

                _customerVehicles = CustomerVehicleService.GetCustomerVehicles(_selectedCustomer);
                _standardJobs = StandardJobsService.GetStandardJobs();
                _listOfCategories = CategoryService.GetCategories();

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

        // add service
        private void StandardJobsOnChange(int standardJobID)
        {
            try
            {
                _standardJob.StandardJobID = standardJobID;
                StandardJobsView standardJob = StandardJobsService.GetStandardJob(_standardJob.StandardJobID);
                _standardJob.Description = standardJob.Description;
                _standardJob.StandardHours = standardJob.StandardHours;
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
                // have a collection of errors
                // each error should be place into a separate line
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

        private void ServiceReset()
        {
            StandardJobsView serviceJob = new();
            _standardJob = serviceJob;


            //_standardJob.StandardJobID = serviceJob.StandardJobID;
            //_standardJob.ServiceJobID = serviceJob.ServiceJobID;
            //_standardJob.Description = serviceJob.Description;
            //_standardJob.StandardHours = serviceJob.StandardHours;
            //_standardJob.Comments = serviceJob.Comments;
            //_standardJob.RemoveFromViewFlag = serviceJob.RemoveFromViewFlag;
            //_standardJob.StandardHours = serviceJob.ExtPrice;

        }

        private async Task AddServiceJob()
        {

            // reset feedback/error message
            _feedbackMessage = string.Empty;
            _errorMessage = string.Empty;

            try
            {
                StandardJobsView serviceJob = new();

                serviceJob.StandardJobID = _standardJob.StandardJobID;
               // serviceJob.ServiceJobID = _standardJob.ServiceJobID;
                serviceJob.Description = _standardJob.Description;
                serviceJob.StandardHours = _standardJob.StandardHours;
                serviceJob.Comments = _standardJob.Comments;
                serviceJob.RemoveFromViewFlag = _standardJob.RemoveFromViewFlag;
                serviceJob.ExtPrice = _standardJob.StandardHours * StandardJobsView.Rate;

                if (!string.IsNullOrEmpty(_standardJob.Description))
                {
                    //int maxID = _serviceJobs.Any() ? _serviceJobs.Max(x => x.ServiceJobID) + 1 : 1;
                    //serviceJob.ServiceJobID = maxID;
                    _serviceJobsGrid.Add(serviceJob);

                }
                else
                {
                    throw new ArgumentNullException($"Please select a service.");
                }

                await InvokeAsync(StateHasChanged);

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
                // have a collection of errors
                // each error should be place into a separate line
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

        private void RemoveServiceJob(StandardJobsView selectedItem)
        {
            
            if (selectedItem != null)
            {
                _serviceJobsGrid.Remove(selectedItem);
            }
        }

        private decimal ComputeServiceSubtotal()
        {
            decimal serviceTotal = 0;
            foreach (var element in _serviceJobsGrid)
            {
                var extPrice = element.StandardHours * StandardJobsView.Rate;
                if (extPrice != element.ExtPrice)
                {
                    throw new Exception("Service total calculation went wrong.");
                }

                serviceTotal += extPrice;
            }

            return serviceTotal;
        }

        // add parts

        private void PartCategoryOnChange(int categoryID)
        {
            try
            {
                _category.CategoryID = categoryID;

                _listOfParts = PartService.GetParts(_category.CategoryID);
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
                // have a collection of errors
                // each error should be place into a separate line
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

        private async Task AddPart(PartView item)
        {
            _feedbackMessage = string.Empty;
            _errorMessage = string.Empty;

            PartView partView = item.ShallowCopy();

            try
            {
                if (partView.Qty == 0)
                {
                    throw new ArgumentNullException($"Please enter part quantity greater than 0.");
                }

                _selectedParts.Add(partView);

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
                // have a collection of errors
                // each error should be place into a separate line
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

        private void DeletePart(PartView selectedItem)
        {
            if (selectedItem != null)
            {
                _selectedParts.Remove(selectedItem);
            }
        }

        private decimal ComputePartSubtotal()
        {
            decimal partsSubtotal = 0;
            foreach (var part in _selectedParts)
            {
                partsSubtotal += part.ExtPrice;

            }

            return partsSubtotal;
        }


        // Totals

        private bool VerifyCoupon()
        {
            _couponView = CouponService.GetCouponByCouponValue(_coupon.CouponIDValue);

            if (_couponView != null)
            {
                _status = CouponStatus.Valid;
                return true;
            }
            _status = CouponStatus.Invalid;
            return false;

        }

        private string CouponIcon()
        {
            switch (_status)
            {

                case CouponStatus.Valid:
                    return Icons.Material.Filled.Check;
                case CouponStatus.Invalid:
                    return @Icons.Material.Filled.Close;

                default:
                    return string.Empty;
            }

        }

        private MudBlazor.Color CouponIconColor()
        {
            switch (_status)
            {
                case CouponStatus.Default:
                    return Color.Dark;

                case CouponStatus.Valid:
                    return Color.Tertiary;

                default:
                    return Color.Error;
            }
        }

        private decimal CalculateSubtotal()
        {
            decimal subtotal = 0;
            subtotal = ComputePartSubtotal() + ComputeServiceSubtotal();
            return subtotal;
        }
        private decimal Discount()
        {
            if (_status == CouponStatus.Valid)
            {
                return _couponView.CouponDiscount / 100.00m * CalculateSubtotal();
            }
            else
            {
                return 0;
            }
        }

        private decimal CalculateGST()
        {
            return (CalculateSubtotal() - Discount()) * 0.05m;
        }

        private decimal CalculateTotal()
        {
            return CalculateSubtotal() - Discount() + CalculateGST();
        }


        private async Task Cancel()
        {
            bool? results = await DialogService.ShowMessageBox("Confirm Cancel", $"Do you wish to close the servicing page? All unsaved changes will be lost.", yesText: "Yes", cancelText: "No");

            if (results == true)
            {
                //_customerForm.ResetTouched();
                NavigationManager.NavigateTo($"/Pages/Servicing", true);
                //NavigationManager.Refresh();

            }
        }

        // Register/Save order
        private void RegisterServiceOrder()
        {
            _errorDetails.Clear();

            // reset the error message to an empty string
            _errorMessage = string.Empty;

            // reset the feedback message to an empty string
            _feedbackMessage = string.Empty;

            #region Business logic and rules

            var serviceSubtotal = ComputeServiceSubtotal();
            var partSubtotal = ComputePartSubtotal();

            var subtotal = CalculateSubtotal();
            var gst = CalculateGST();
            var total = CalculateTotal();

            if (_customerVehicles.Count == 0)
            {
                _errorDetails.Add("The customer who doesn't have vehicles cannot register service.");
            }

            if (_customerVehicle == null)
            {
                _errorDetails.Add("No vehicle was selected to register service.");
            }

            //service validation, also performs in JobService
            if (_serviceJobsGrid.Count == 0)
            {
                _errorDetails.Add("No service selected, please select at least one service to register service order.");
            }

            foreach (var service in _serviceJobsGrid)
            {
                if (service.StandardHours <= 0)
                {
                    _errorDetails.Add($"{service.Description} service hour cannot be less than or equal to 0. ");
                }

                if (service.ExtPrice <= 0)
                {
                    _errorDetails.Add($"{service.Description} service expected price cannot be less than or equal to 0. ");
                }

            }

            if (serviceSubtotal < 0)
            {
                _errorDetails.Add($"Service subtotal cannot be less than 0. ");
            }
         
            // parts validation
            if (_selectedParts.Count > 0)
            {
                foreach (var selectedPart in _selectedParts)
                {
                    var part = PartService.GetPart(selectedPart.PartID);


                    if (selectedPart.Qty > part.QuantityOnHand)
                    {
                        _errorDetails.Add($"The selected quantity of {part.Description} is greater than the quantity on hand.");
                    }

                    if (selectedPart.Qty <= 0)
                    {
                        _errorDetails.Add($"The selected {part.Description} is a negative or zero quantity.");
                    }

                    if (selectedPart.ExtPrice <= 0)
                    {
                        _errorDetails.Add($"The selected {part.Description} has a negative  expected price.");
                    }
                }

                if (partSubtotal < 0)
                {
                    _errorDetails.Add($"The parts subtotal is a negative value.");
                }
            }

            // totals validation
            if (subtotal <= 0)
            {
                _errorDetails.Add($"The subtotal is a negative or zero value.");
            }

            if (gst < 0)
            {
                _errorDetails.Add($"The GST is a negative value.");
            }

            if (total <= 0)
            {
                _errorDetails.Add($"The total is a negative or zero value.");
            }

            #endregion

            #region Data processing

            var currentDate = DateTime.Today;

            // collect view model data from front end
            JobView jobView = new();

            jobView.JobDateIn = currentDate;
            jobView.EmployeeId = userId;
            jobView.ShopRate = StandardJobsView.Rate;
            jobView.VehicleIdentification = _customerVehicle.Vin;
            jobView.RemoveFromViewFlag = false;

            if (_status == CouponStatus.Valid)
            {
                jobView.CouponId = _couponView.CouponID;
            }
            else
            {
                jobView.CouponId = null;
            }

            jobView.SelectedJobsList = _serviceJobsGrid;
            jobView.SelectedPartsList = _selectedParts;

            #endregion


            #region Call services to add new data to DB

            if (_errorDetails.Count == 0)
            {
                try
                {
                    JobService.AddServiceJob(jobView);
                    _feedbackMessage = "Data was successfully saved!";
                    _hasDataChanged = true;
                    _isFormValid = false;
                    _customerForm.ResetTouched();
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
                    // have a collection of errors
                    // each error should be place into a separate line
                    _errorMessage = BlazorHelperClass.GetInnerException(ex).Message;

                    if (!string.IsNullOrWhiteSpace(_errorMessage))
                    {
                        _errorMessage = $"{_errorMessage}{Environment.NewLine}";
                    }
                    _errorMessage = $"{_errorMessage}Unable to save for customer";
                    foreach (var error in ex.InnerExceptions)
                    {
                        _errorDetails.Add(error.Message);
                    }
                }
            }
            else
            {
                _errorMessage = "Unable to register service order, see the following concerns:";
            }
            
            #endregion

        }

        // utility function
        private string ServiceConverter(int standardJobId)
        {
            var serviceJob = _standardJobs.Find(x => x.StandardJobID.Equals(standardJobId));

            //if (serviceJobId == 0)
            //{
            //    return "";
            //}

            return serviceJob?.Description ?? string.Empty;
        }

        private string PartConverter(int categoryId)
        {
            if (categoryId == 0)
            {
                return "";
            }

            var category = _listOfCategories.Find(x => x.CategoryID.Equals(categoryId));

            return category.Description;
        }

        #endregion
    }
}
