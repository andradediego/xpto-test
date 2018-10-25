using Xpto.Data;
using Xpto;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xpto.Extensions;
using System.Collections.Generic;

namespace Xpto.Repository
{
    public class ClienteRespository : RespositoryBase<Clientes>, IClienteRepository
    {
        public void AddClienteFromFile(string content)
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
                    
                    var cliente = GetByReferenceCode(itens[0].Trim().RemoverCaracteresInvalidos());
                    if (cliente == null)
                    {
                        var _cliente = SetPropertiesFromFile(itens);
                        if(_cliente != null)
                            Add(_cliente);
                    }
                }
            }
        }

        private Clientes SetPropertiesFromFile(string[] itens)
        {
            if (itens.Length == 0)
                return null;

            var cliente = new Clientes();

            if (itens.Length > 0)
            {
                string cod = itens[0].ToString().RemoverCaracteresInvalidos();

                int codReferencia = 0;
                if (Int32.TryParse(cod, out codReferencia))
                    cliente.CodReferencia = codReferencia;
                else
                    return null;
            }

            if (itens.Length > 1)            
                cliente.Nome = itens[1];            

            if (itens.Length > 2)
                cliente.Sobrenome = itens[2];

            if (itens.Length > 3)
            {
                var partData = itens[3].Split('/');
                if (partData.Length > 2)
                {
                    int dia = 0;
                    int mes = 0;
                    int ano = 0;

                    Int32.TryParse(partData[0].RemoverCaracteresInvalidos(), out dia);
                    Int32.TryParse(partData[1].RemoverCaracteresInvalidos(), out mes);
                    Int32.TryParse(partData[2].RemoverCaracteresInvalidos(), out ano);
                    if (DataValida(dia, mes, ano))
                        cliente.DataNascimento = new DateTime(ano, mes, dia);
                }
            }

            if (itens.Length > 4)
            {
                var sexo = itens[4].ToLower().ToEnum<SexoEnum>();
                cliente.Sexo = sexo;
            }

            if (itens.Length > 5 && EmailValido(itens[5]))
            {
                cliente.Email = itens[5];
            }

            if (itens.Length > 6)
            {
                bool ativo = false;
                Boolean.TryParse(itens[6], out ativo);
                cliente.Ativo = ativo;
            }

            return cliente;
        }

        private bool DataValida(int dia, int mes, int ano)
        {
            // int[] meses31 = new int[] { 1, 3, 5, 7, 8, 10, 12};
            int[] meses30 = new int[] { 4, 6, 9, 11 };
            if (ano < 1900 || ano == 0)
                return false;
            else if (mes == 0)
                return false;
            else if (dia == 0 || dia > 31)
                return false;
            else if (DateTime.IsLeapYear(ano) && mes == 2 && dia > 29)
                return false;
            else if (mes == 2 && dia > 29)
                return false;
            else if (meses30.Contains(mes) && dia > 30)
                return false;

            return true;
        }

        private bool EmailValido(string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            return rg.IsMatch(email);
        }

        public void AddProductFromFile(string content)
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
                    int id = 0;
                    Int32.TryParse(itens[0].RemoverCaracteresInvalidos(), out id);

                    var produto = db.Produtos.FirstOrDefault(x => x.CodReferencia == id);
                    if (produto == null && itens.Length > 2)
                    {
                        var p = new Produtos()
                        {
                            CodReferencia = id,
                            Nome = itens[2].RemoverCaracteresInvalidos()
                        };
                        db.Produtos.Add(p);
                        db.SaveChanges();

                        AddProductToCliente(itens[1], p);
                    }
                    else if (produto != null && itens.Length > 2)
                    {
                        AddProductToCliente(itens[1], produto);
                    }
                }
            }
        }

        public void AddProductToCliente(string idcliente, Produtos produto)
        {
            AddProductToCliente(Int32.Parse(idcliente.RemoverCaracteresInvalidos()), produto);
        }

        public void AddProductToCliente(int idcliente, Produtos produto)
        {
            var cliente = GetByReferenceCode(idcliente);

            if (cliente != null)
            {                
                if (cliente.Produtos != null)
                {
                    var produtoCadastrado = cliente.Produtos.FirstOrDefault(x => x.CodReferencia == produto.CodReferencia);
                    if (produtoCadastrado == null)
                    {
                        AddProduct(cliente, produto);
                    }                                     
                }
                else
                    AddProduct(cliente, produto);
            }
        }

        private void AddProduct(Clientes cliente, Produtos produto)
        {
            if(cliente.Produtos == null)
                cliente.Produtos = new List<Produtos>();

            cliente.Produtos.Add(produto);
            Update(cliente);
        }

        public IEnumerable<Clientes> ClienteProdutosList()
        {
            return db.Clientes.Where(x => x.Ativo).ToList();
        }
    }
}
