using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xpto.Repository;

namespace XptoWeb.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        // GET: Cliente
        [HttpPost]
        public ActionResult SalvarClienteArquivo(ArquivoViewModel c)
        {
            try
            {
                _clienteRepository.AddClienteFromFile(c.Conteudo);

                return Json("Inclusão feita com sucesso");
            }
            catch (Exception ex)
            {
                return Json("Ocorreu um erro ao tentar concluir a solicitação");
            }
        }

        [HttpPost]
        public ActionResult SalvarProdutosClienteArquivo(ArquivoViewModel c)
        {
            try
            {
                _clienteRepository.AddProductFromFile(c.Conteudo);

                return Json("Inclusão feita com sucesso");
            }
            catch (Exception ex)
            {
                return Json("Ocorreu um erro ao tentar concluir a solicitação");
            }
        }
    }
}