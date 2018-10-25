using Xpto.Data;

namespace Xpto.Repository
{
    public interface IProdutoRepository : IRepositoryBase<Produtos>
    {
        void SaveProductsFromFile(string content);
    }
}
