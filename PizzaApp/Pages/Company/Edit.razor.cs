using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Pages.Company
{
	public partial class Edit
	{
        [CascadingParameter] 
        ISnackbar Snackbar { get; set; }

        //[Inject] 
        //IDbContextFactory<PizzaContext> PizzaContext { get; set; }

        [Parameter]
        public int CompanyID { get; set; }

        //Server.DAL.Models.Company Model { get; set; } = new();

        bool BtnUpdateCompanyInfo { get; set; }
        bool IsLoading { get; set; } = true;

        protected override Task OnInitializedAsync()
        {
            if (CompanyID != 0)
            {
                // todo - fetch company from db
            }

            return base.OnInitializedAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                IsLoading = false;
                StateHasChanged();
            }

            base.OnAfterRender(firstRender);
        }

        private void SetProfilePicture(string profilePic)
        {
            //Model.CompanyLogo = profilePic;
            StateHasChanged();
        }

        private async Task UpdateCompanyInfo()
        {
            BtnUpdateCompanyInfo = true;

            //using var context = PizzaContext.CreateDbContext();
            //using var repository = new PizzaRepository(context);

            //// check if email is already in use
            //if (repository.Users.EmailAlreadyRegistered(Model.Email))
            //{
            //    Snackbar.Add("Email has already been registered. Please try again", Severity.Error);
            //}
            //else
            //{

            //    User dbUser = new()
            //    {
            //        ID = 0, // always 0 to generate new id in db
            //        Password = PasswordHelper.CreateHashPassword(NewUser.Password),
            //        CreatedDate = DateTime.Now,
            //        DateOfBirth = NewUser.DateOfBirth,
            //        Email = NewUser.Email,
            //        FirstName = NewUser.FirstName,
            //        LastName = NewUser.LastName,
            //        ModifiedDate = DateTime.Now,
            //        PhoneNumber = NewUser.PhoneNumber,
            //        ProfilePicture = NewUser.ProfilePicture,
            //    };

            //    var response = repository.Users.InsertOrUpdate(dbUser);

            //    if (response)
            //    {
            //        // navigate
            //    }
            //    else
            //    {
            //        // error
            //    }
            //}

            await Task.Delay(1000);

            BtnUpdateCompanyInfo = false;
            StateHasChanged();
        }
    }
}
