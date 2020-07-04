using Autofac;
using BlazorErp.Services.Definition.Base;
using BlazorErp.Shared.Models.Response.Upload;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Server.Common.Services
{
    public class ApplicationConfigurationService : IApplicationConfigurationService
    {
        IOptions<UploadSettings> uploadSettings;
        ILifetimeScope scope;

        public ApplicationConfigurationService(ILifetimeScope scope, IOptions<UploadSettings> uploadSettings
                                                )
        {
            this.scope = scope;
            this.uploadSettings = uploadSettings;

        }


        public UploadSettingsModel GetUploadSettings()
        {
            return new UploadSettingsModel
            {
                PutanjaFoldera = uploadSettings.Value.PutanjaFoldera,
                DozvoljeneEkstenzije = FormatirajEkstenzije(uploadSettings.Value.DozvoljeneEkstenzije),
                MaksimalnaVelicinaMB = uploadSettings.Value.MaksimalnaVelicinaMB
            };
        }

        public string[] FormatirajEkstenzije(string ekstenzije)
        {
            var formatirane = uploadSettings.Value.DozvoljeneEkstenzije.Split(',');

            for (int i = 0; i < formatirane.Length; i++)
            {
                formatirane[i] = "." + formatirane[i];
            }

            return formatirane;
        }
    }

    public class UploadSettings
    {
        public string PutanjaFoldera { get; set; }
        public string DozvoljeneEkstenzije { get; set; }
        public int MaksimalnaVelicinaMB { get; set; }
    }
}
