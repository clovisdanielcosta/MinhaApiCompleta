using CD.Api.ViewModels;
using CD.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CD.Api.Controllers
{
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManger;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(INotificador notificador, 
                              SignInManager<IdentityUser> signInManger, 
                              UserManager<IdentityUser> userManager) : base(notificador)
        {
            _signInManger = signInManger;
            _userManager = userManager;
        }

        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _signInManger.SignInAsync(user, false);
                return CustomResponse(registerUser);
            }

            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }
            
            return CustomResponse(registerUser);
        }
    }
}
