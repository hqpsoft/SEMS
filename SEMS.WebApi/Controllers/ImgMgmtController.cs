using FlowJs;
using FlowJs.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SEMS.WebApi.Controllers
{
    [RoutePrefix("api/ImgMgmt")]
    public class ImgMgmtController : BaseController
    {
        const string Folder = @"C:\Temp\PicUpload";

       
        private readonly IFlowJsRepo _flowJs;

        public ImgMgmtController()
        {
            _flowJs = new FlowJsRepo();
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IHttpActionResult> PictureUploadPost()
        {
            var request = HttpContext.Current.Request;

            var validationRules = new FlowValidationRules();
            validationRules.AcceptedExtensions.AddRange(new List<string> { "jpeg", "jpg", "png", "bmp" });
            validationRules.MaxFileSize = 5000000;

            try
            {
                var status = _flowJs.PostChunk(request, Folder, validationRules);

                if (status.Status == PostChunkStatus.Done)
                {
                    // file uploade is complete. Below is an example of further file handling
                    var filePath = Path.Combine(Folder, status.FileName);
                    var file = File.ReadAllBytes(filePath);
                    //var picture = await _fileManager.UploadPictureToS3(User.Identity.GetUserId(), file, status.FileName);
                    //File.Delete(filePath);
                    return Ok();
                }

                if (status.Status == PostChunkStatus.PartlyDone)
                {
                    return Ok();
                }

                status.ErrorMessages.ForEach(x => ModelState.AddModelError("file", x));
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                ModelState.AddModelError("file", "exception");
                return BadRequest(ModelState);
            }
        }
    }
}
