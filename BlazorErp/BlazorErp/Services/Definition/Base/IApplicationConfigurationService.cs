using BlazorErp.Shared.Models.Response.Upload;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Definition.Base
{
    public interface IApplicationConfigurationService : IService
    {
        UploadSettingsModel GetUploadSettings();

    }
}
