﻿using AutoMapper;
using CD.Business.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MinhaAPICompleta.ViewModels;

namespace CD.Api.Controllers
{
    [Route("api/[Controller]")]
    public class ProdutoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository, 
                                 IProdutoService produtoService, 
                                 IMapper mapper, 
                                 INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores());
        }

    }
}
