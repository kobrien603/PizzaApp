using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Components
{
    public partial class UploadProfilePicture
    {
        [Parameter] 
        public string Label { get; set; } = "Upload a File";

        [Parameter]
        public bool ShowProfilePicture { get; set; } = true;

        [Parameter]
        public EventCallback<string> ReturnProfilePicture { get; set; }

        [Parameter]
        public string ProfilePicture { get; set; } = string.Empty;

        public bool BtnLoadingPicture { get; set; } = false;

        /// <summary>
        /// upload file event - on file upload click
        /// </summary>
        /// <param name="file"></param>
        private async void Upload(IBrowserFile file)
        {
            BtnLoadingPicture = true;
            await ConvertImageToBase64(file);

            if (!string.IsNullOrEmpty(ProfilePicture))
            {
                await ReturnProfilePicture.InvokeAsync(ProfilePicture);
            }
            BtnLoadingPicture = false;
            StateHasChanged();
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task ConvertImageToBase64(IBrowserFile file)
        {
            try
            {
                await using MemoryStream fs = new();
                await file.OpenReadStream(maxAllowedSize: 1048576, new CancellationToken()).CopyToAsync(fs);
                byte[] somBytes = GetBytes(fs);

                ProfilePicture = "data:image/png;base64," + Convert.ToBase64String(somBytes, 0, somBytes.Length);
            }
            catch (Exception e)
            {
                // todo - add logging
                ProfilePicture = string.Empty;
                throw;
            }
        }

        private byte[] GetBytes(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.ReadAsync(bytes, 0, bytes.Length);
            stream.Dispose();
            return bytes;
        }
    }
}
