using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Library;

namespace User.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _userServices;

        /// Default controller.
        public UsersController(IUsersServices UserServices)
        {
            _userServices = UserServices;
        }

        /// <summary>
        /// Seleciona todos os usuários
        /// </summary>
        /// <returns>Lista contendo todos os usuários.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userServices.GetAll());
        }

        /// <summary>
        /// Cadastra um usuário
        /// </summary>
        /// <param name="model">Dados para cadastro do usuário</param>
        /// <returns>Resultado da operação</returns>
        [HttpPost]
        public IActionResult Register(User.Library.User model)
        {
            var result = _userServices.Register(model);

            if (result.ContainErrors)
            {
                return BadRequest(result);
            }

            return Ok("Usuário cadastrado");
        }

        /// <summary>
        /// Seleciona um usuário pelo id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Objeto Usuário</returns>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var user = _userServices.GetById(id);
            if (user == null)
            {
                return BadRequest("Usuário não encontrado");
            }
            return Ok(user);
        }

        /// <summary>
        /// Exclui usuário pelo id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Objeto usuário</returns>
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _userServices.Delete(id);

            if (result.ContainErrors)
            {
                return BadRequest(result);
            }

            return Ok("Usuário excluído");
        }

        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="model">Dados para atualizar do usuário</param>
        /// <returns>Resultado da operação</returns>
        [HttpPut]
        public IActionResult Update(User.Library.User model)
        {
            var result = _userServices.Update(model);

            if (result.ContainErrors)
            {
                return BadRequest(result);
            }

            return Ok("Usuário atualizado");
        }

    }
}