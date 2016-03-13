using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SEMS.WebSite.Controllers
{
    public class ImgMgmtController : Controller
    {
        // GET: ImgMgmt
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                try
                {
                    // 文件上传后的保存路径
                    string filePath = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

                    fileData.SaveAs(filePath + saveName);

                    return Json(new { Success = true, FileName = fileName, SaveName = saveName });
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("upload"), AcceptVerbs("POST")]
        public async Task<IHttpActionResult> Upload()
        {
            // ensure that the request contains multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            if (!Directory.Exists(_sRoot)) Directory.CreateDirectory(_sRoot);
            MultipartFormDataStreamProvider provider =
                new MultipartFormDataStreamProvider(_sRoot);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                int nChunkNumber = Convert.ToInt32(provider.FormData["flowChunkNumber"]);
                int nTotalChunks = Convert.ToInt32(provider.FormData["flowTotalChunks"]);
                string sIdentifier = provider.FormData["flowIdentifier"];
                string sFileName = provider.FormData["flowFilename"];

                // rename the generated file
                MultipartFileData chunk = provider.FileData[0]; // Only one file in multipart message
                RenameChunk(chunk, nChunkNumber, sIdentifier);

                // assemble chunks into single file if they're all here
                TryAssembleFile(sIdentifier, nTotalChunks, sFileName);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private string GetChunkFileName(int chunkNumber, string identifier)
        {
            return Path.Combine(_sRoot,
                String.Format(CultureInfo.InvariantCulture, "{0}_{1}",
                    identifier, chunkNumber));
        }

        private void RenameChunk(MultipartFileData chunk, int chunkNumber, string identifier)
        {
            string sGeneratedFileName = chunk.LocalFileName;
            string sChunkFileName = GetChunkFileName(chunkNumber, identifier);
            if (File.Exists(sChunkFileName)) File.Delete(sChunkFileName);
            File.Move(sGeneratedFileName, sChunkFileName);
        }

        private string GetFileName(string identifier)
        {
            return Path.Combine(_sRoot, identifier);
        }

        private bool IsChunkHere(int chunkNumber, string identifier)
        {
            string sFileName = GetChunkFileName(chunkNumber, identifier);
            return File.Exists(sFileName);
        }

        private bool AreAllChunksHere(string identifier, int totalChunks)
        {
            for (int nChunkNumber = 1; nChunkNumber <= totalChunks; nChunkNumber++)
                if (!IsChunkHere(nChunkNumber, identifier)) return false;
            return true;
        }

        private void TryAssembleFile(string identifier, int totalChunks, string filename)
        {
            if (!AreAllChunksHere(identifier, totalChunks)) return;

            // create a single file
            string sConsolidatedFileName = GetFileName(identifier);
            using (Stream destStream = File.Create(sConsolidatedFileName, 15000))
            {
                for (int nChunkNumber = 1; nChunkNumber <= totalChunks; nChunkNumber++)
                {
                    string sChunkFileName = GetChunkFileName(nChunkNumber, identifier);
                    using (Stream sourceStream = File.OpenRead(sChunkFileName))
                    {
                        sourceStream.CopyTo(destStream);
                    }
                } //efor
                destStream.Close();
            }

            // rename consolidated with original name of upload
            // strip to filename if directory is specified (avoid cross-directory attack)
            filename = Path.GetFileName(filename);
            Debug.Assert(filename != null);

            string sRealFileName = Path.Combine(_sRoot, filename);
            if (File.Exists(filename)) File.Delete(sRealFileName);
            File.Move(sConsolidatedFileName, sRealFileName);

            // delete chunk files
            for (int nChunkNumber = 1; nChunkNumber <= totalChunks; nChunkNumber++)
            {
                string sChunkFileName = GetChunkFileName(nChunkNumber, identifier);
                File.Delete(sChunkFileName);
            } //efor
        }
    }
}