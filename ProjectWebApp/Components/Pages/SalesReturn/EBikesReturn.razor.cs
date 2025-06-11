using Microsoft.AspNetCore.Components;
using SalesReturnsSystem.BLL;
using SalesReturnsSystem.Entities;
using SalesReturnsSystem.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ProjectWebApp.Data;

namespace ProjectWebApp.Components.Pages.SalesReturn
{
    public partial class EBikesReturn
    {
        #region Class Fields
        private int _saleID = 0;

        private string _feedbackMsg = string.Empty;

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
        protected InvoiceServices Invoice { get; set; } = default!;

        protected List<InvoiceView> Invoices { get; set; } = new();

        [Inject]
        protected InvoiceDetailServices InvoiceDetail { get; set; } = default!;
        [Inject]
        protected UserManager<ApplicationUser> UserManager { get; set; } = default!;

        protected List<InvoiceDetailsView> InvoiceDetails { get; set; } = new ();

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

                Invoices.Clear();

                if (_saleID == 0)
                {
                    throw new ArgumentException();
                }

                Invoices = Invoice.GetInvoices(_saleID);

                if(Invoices.Count == 1)
                {
                    _feedbackMsg = $"Please search for a proper Invoice Number";
                    _noResults = true;
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentException("Please provide an invoice number");
            }
        }

        private void InvoiceClear()
        {
            _feedbackMessage = string.Empty;
            _errorMessage = string.Empty;

            _saleID = 0;
            Invoices.Clear();

            _feedbackMessage = $"The invoice number has been cleared.";
        }

        private void InvoiceInfo(int invoiceNum)
        {
            if (invoiceNum == 0)
            {
                throw new ArgumentException();
            }
            else 
            {
                InvoiceDetails = InvoiceDetail.GetInvoiceDetails(invoiceNum);
            }
        }

        #endregion
    }
}
