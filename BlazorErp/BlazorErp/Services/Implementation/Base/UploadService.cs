using Autofac;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Base;
using BlazorErp.Shared.Models.Response.Upload;
using BlazorErp.Services.Helpers;
using BlazorErp.Services.Result;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorErp.Services.Definition.Base;

namespace BlazorErp.Services.Implementation
{
    public class UploadService : Service, IUploadService
    {
        IApplicationConfigurationService applicationConfigurationService;
        Context context;

        public UploadService(ILifetimeScope scope, Context context, IApplicationConfigurationService applicationConfigurationService) : base(scope)
        {
            this.applicationConfigurationService = applicationConfigurationService;
            this.context = context;
        }

        async Task<ServiceResult<FileResult>> IUploadService.Get(int id)
        {
            var dokument = context.Dokumenti.FirstOrDefault(a => a.Id == id);

            if (dokument == null)
            {
                return Error("Dokument ne postoji");
            }

            var folderPath = applicationConfigurationService.GetUploadSettings().PutanjaFoldera;
            var localFilePath = Path.Combine(folderPath, dokument.Putanja);

            var fileName = dokument.Naziv;

            if (!File.Exists(localFilePath))
            {
                return Error("Dokument ne postoji");
            }
            else
            {
                using (var document = File.Open(localFilePath, FileMode.Open))
                {
                    //Response.Headers.Add("Content-Disposition", new Microsoft.Extensions.Primitives.StringValues("attachment"));
                    var ekstenzija = Path.GetExtension(localFilePath);

                    byte[] bytes = new byte[document.Length];
                    await document.ReadAsync(bytes, 0, bytes.Length);

                    return Ok(new FileResult
                    {
                        Bytes = bytes,
                        MimeType = MimeTypeMap.GetMimeType(ekstenzija),
                        Naziv = dokument.Naziv
                    });
                }
            }
        }

        async Task<int> IUploadService.Post(IFormFile file)
        {
            // Ovdje se doda 5 random karaktera, da se izbjegne prepisivanje ako se dokumenti zovu isto
            var fileName = String.Format("{0}_{1}", Guid.NewGuid().ToString().Substring(0, 5), file.FileName);
            var folderPath = applicationConfigurationService.GetUploadSettings().PutanjaFoldera;

            // Ako ne postoji direktorij kreiraj ga
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Posto je folder konfigurabilan mora se spojiti folder i ima fajla
            var filePath = Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Zapisi podatke o uploadovanom dokumentu
            var dokument = new Dokument
            {
                Naziv = file.FileName,
                Putanja = fileName
            };
            context.Dokumenti.Add(dokument);

            // Zato sto su dokementi autorizovani koristi autorizovani save changes
            SaveChanges(context);

            return dokument.Id;
        }
    }
}
