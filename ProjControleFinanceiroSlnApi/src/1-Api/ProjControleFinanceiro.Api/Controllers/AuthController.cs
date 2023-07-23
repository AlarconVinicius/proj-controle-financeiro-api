using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjControleFinanceiro.Api.Controllers.Configuracao;
using ProjControleFinanceiro.Domain.DTOs.Usuario;

namespace ProjControleFinanceiro.Api.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registar(UsuarioViewModel registroUsuario)
        {
            if(!ModelState.IsValid) { return CustomResponse(ModelState); }

            var user = new IdentityUser
            {
                UserName = registroUsuario.Email,
                Email = registroUsuario.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, registroUsuario.Password);

            if(result.Succeeded)
            {
                //Criar o usuario (Nova tabela chamada Usuario)

                //Identity
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(result);
            }

            return CustomResponse(registroUsuario);
            

        }

    }
}
