﻿using CD.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MinhaAPICompleta.ViewModels;

namespace CD.Api.Controllers
{
    [Route("api/[controller]")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedoresController(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<ActionResult<IEnumerable<FornecedorViewModel>>> ObterTodos()
        {
            var fornecedor = await _fornecedorRepository.ObterTodos();

            return Ok(fornecedor);
        }
    }

}