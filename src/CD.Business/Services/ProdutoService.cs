using CD.Business.Interfaces;
using CD.Business.Models;
using CD.Business.Models.Validations;

namespace CD.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUser _user;

        public ProdutoService(IProdutoRepository produtoRepository,
                              IUser user,  
                              INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _user = user;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            var user = _user.GetUserEmail(); // Exemplo para pegar dados do usuário logado

            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Atualizar(produto);
        }


        public async Task Remover(Guid id)
        {
            await _produtoRepository.Remover(id); 
        }
        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }

}
