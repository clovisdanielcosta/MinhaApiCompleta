using Microsoft.AspNetCore.Mvc.ModelBinding;
using MinhaAPICompleta.ViewModels;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CD.Api.Extensions
{
    public class ProdutoModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            var produtoImagemViewModel = JsonSerializer.Deserialize<ProdutoImagemViewModel>(bindingContext.ValueProvider.GetValue("produto").FirstOrDefault(), serializeOptions);
            produtoImagemViewModel.ImagemUpload = bindingContext.ActionContext.HttpContext.Request.Form.Files.FirstOrDefault();

            bindingContext.Result = ModelBindingResult.Success(produtoImagemViewModel);
            return Task.CompletedTask;
        }
    }
}
