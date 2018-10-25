using System.Collections.Generic;
using Xpto.Data;

namespace Xpto.Repository
{
    public interface IClienteRepository : IRepositoryBase<Clientes>
    {
        void AddClienteFromFile(string content);
        void AddProductFromFile(string content);
        void AddProductToCliente(string idCliente, Produtos produto);
        void AddProductToCliente(int idCliente, Produtos produto);
        IEnumerable<Clientes> ClienteProdutosList();
    }
}
