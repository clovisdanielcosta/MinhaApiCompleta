using AutoMapper;
using CD.Business.Interfaces;
using CD.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using MinhaAPICompleta.ViewModels;
using System.Collections.Generic;

namespace CD.Api.Controllers
{
    [Route("api/[controller]")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(  IFornecedorRepository fornecedorRepository, 
                                        IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
            var fornecedor = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return fornecedor;
        }

        public async Task<FornecedorViewModel> ObterPorid(Guid id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(id);

            return fornecedor;
        }

        public async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }

    }

}