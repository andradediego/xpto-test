using Xpto.Data;
using System;

namespace Xpto.Repository
{
    public class ProdutoRepository : RespositoryBase<Produtos>, IProdutoRepository
    {
        public void SaveProductsFromFile(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new Exception("Conteúdo vazio");

            var linhas = content.Split(';');
            if (linhas.Length == 0)
                throw new Exception("Conteúdo vazio");

            foreach (var linha in linhas)
            {
                var itens = linha.Split(',');
                if (itens.Length == 0)
                    continue;

                if (itens.Length > 0)
                {
                    var cod = itens[0].RemoverCaracteresInvalidos();
                    var produto = GetByReferenceCode(cod);
                    if (produto == null && itens.Length > 2)
                    {
                        Add(new Produtos()
                        {
                            CodReferencia = Int32.Parse(cod),
                            Nome = itens[2]
                        });
                    }
                }
            }
        }
    }
}
