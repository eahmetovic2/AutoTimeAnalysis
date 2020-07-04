using BlazorErp.Shared.Models.Response.Upload;
using BlazorErp.Services.Result;
using System.IO;
using System.Threading.Tasks;

namespace BlazorErp.Services
{
    public interface IUploadService : IService
    {
        Task<ServiceResult<FileResult>> Get(int id);
        Task<int> Post(Microsoft.AspNetCore.Http.IFormFile file);
    }
}
