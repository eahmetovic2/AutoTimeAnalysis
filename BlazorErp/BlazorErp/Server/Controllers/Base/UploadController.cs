using BlazorErp.Entities;
using BlazorErp.Entities.Models;
using BlazorErp.Services;
//using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using BlazorErp.Services.Extensions;
using BlazorErp.Shared.Models.Response.Common;
using System.Threading.Tasks;
using BlazorErp.Services.Result;
//using Microsoft.AspNetCore.Http;
using BlazorErp.Shared.Models.Response.Upload;
using Microsoft.AspNetCore.Mvc;
using BlazorErp.Services.Definition.Base;
using BlazorErp.Services.Definition.Korisnik;

namespace BlazorErp.Server.Controllers.Base
{
    /// <summary>
    /// Kontroler za upravljanje uploadom datoteka
    /// </summary>
    [Route("upload/")]
    public class UploadController : BaseController
    {
        Context context;
        IApplicationConfigurationService applicationConfigurationService;
        IAuthService authService;
        IUploadService uploadService;

        public UploadController(Context context, IApplicationConfigurationService applicationConfigurationService, IAuthService authService, IUploadService uploadService)
        {
            this.context = context;
            this.applicationConfigurationService = applicationConfigurationService;
            this.authService = authService;
            this.uploadService = uploadService;
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int id)
        {       
            var result = await uploadService.Get(id);
            if (!result.IsOk)
            {
                return Convert(result);
            }
            return File(result.Value.Bytes, result.Value.MimeType, result.Value.Naziv);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post()
        {
            try
            {
                var httpRequest = HttpContext.Request;
                if (httpRequest.Form.Files.Count > 0)
                {
                    foreach (var file in httpRequest.Form.Files)
                    {
                        var ekstenzija = Path.GetExtension(file.FileName);

                        var settings = applicationConfigurationService.GetUploadSettings();
                        var dozvoljeneExtenzije = settings.DozvoljeneEkstenzije;
                        var dozvoljeneExtenzijeLista = new List<string>(dozvoljeneExtenzije);

                        if (!dozvoljeneExtenzijeLista.Contains(ekstenzija))
                        {
                            return BadRequest("Nije dozvoljena ekstenzija");
                        }

                        var maksimalnaVelicinaDokumenta = settings.MaksimalnaVelicinaMB;
                        var fileSizeMB = (file.Length / 1024f) / 1024f;

                        if (maksimalnaVelicinaDokumenta < fileSizeMB)
                        {
                            return BadRequest("Prevelik file");
                        }

                        var result = await uploadService.Post(file);

                        var uploadResult = new UploadResultModel
                        {
                            Options = httpRequest.Form,
                            Result = result
                        };

                        return Ok(uploadResult);
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
