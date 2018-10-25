using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Xpto.Repository;

namespace XptoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        public HomeController(IClienteRepository clienteRepository, IProdutoRepository produtoRepository)
        {
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadDados()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {

            try
            {
                //  Get all files from Request object  
                HttpFileCollectionBase files = Request.Files;
                var resultado = new StringBuilder();
                
                for (int i = 0; i < files.Count; i++)
                {
                    var supportedTypes = new[] { "txt" };

                    var fileExt = Path.GetExtension(files[i].FileName).Substring(1);

                    if (!supportedTypes.Contains(fileExt))                    
                        return Json("Tipo Inválido. Arquivos suportado txt");                    

                    if (files[i].ContentLength > 0)
                    {
                        HttpPostedFileBase file = files[i];

                        BinaryReader b = new BinaryReader(file.InputStream);
                        byte[] binData = b.ReadBytes(file.ContentLength);

                        resultado.Append(Encoding.UTF8.GetString(binData));
                    }                    
                }
                return Json(resultado.ToString());
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }
    }
}