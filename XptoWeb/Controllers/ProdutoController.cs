using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xpto.Repository;

namespace XptoWeb.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpPost]
        public ActionResult SalvarProdutoArquivo(ArquivoViewModel c)
        {
            try
            {
                _produtoRepository.SaveProductsFromFile(c.Conteudo);

                return Json("Inclusão feita com sucesso");
            }
            catch (Exception ex) 
            {
                return Json("Ocorreu um erro ao tentar concluir a solicitação");
            }
        }
    }
}