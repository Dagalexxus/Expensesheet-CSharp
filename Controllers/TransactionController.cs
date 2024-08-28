using api.Models;
using api.Mappers;
using api.Dtos.Transaction;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace api.Controllers{
    [Route("api/[Controller]")]
    [ApiController]
    public class TransactionController : ControllerBase{
        private readonly ITransactionRepository _transactionRepository;
        private readonly IConfiguration _configuration;
        public TransactionController(ITransactionRepository transactionRepository, IConfiguration configuration)
        {
            _transactionRepository = transactionRepository; 
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query, [FromHeader(Name = "Authorization")] string? jwt){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            jwt = jwt.Substring(7);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            User user = await Passwords.decodeToken(jwt, _configuration);

            List<Transaction> transactions = await _transactionRepository.GetAllAsync(query, user.id);

            List<TransactionDTO> transactionDTOs = transactions.Select(t => t.toTransactionDTO())
                                                            .ToList();

            return Ok(transactionDTOs);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async  Task<IActionResult> GetById([FromRoute] int id, [FromHeader(Name = "Authorization")] string? jwt){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            jwt = jwt.Substring(7);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            User user = await Passwords.decodeToken(jwt, _configuration);

            api.Models.Transaction? transaction = await _transactionRepository.GetByIdAsync(id, user.id);

            if (transaction == null){
                return NotFound();
            }
             
            return Ok(transaction.toTransactionDTO());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Insert([FromBody] InsertTransactionRequestDto transactionDTO, [FromHeader(Name = "Authorization")] string? jwt){
            if (!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            jwt = jwt.Substring(7);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            User user = await Passwords.decodeToken(jwt, _configuration);

            Transaction transactionModel = transactionDTO.ToTransactionFromInsertDto();
            transactionModel.userID = user.id;
            await _transactionRepository.InsertAsync(transactionModel);
            return CreatedAtAction(nameof(GetById), new { id = transactionModel.Id}, transactionModel.toTransactionDTO());
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTransactionRequestDto updateDto, [FromHeader(Name = "Authorization")] string? jwt){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            jwt = jwt.Substring(7);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            User user = await Passwords.decodeToken(jwt, _configuration);

            Transaction? transactionModel = await _transactionRepository.UpdateAsync(id, updateDto, user.id);

            if (transactionModel == null){
                return NotFound();
            }

            return Ok(transactionModel.toTransactionDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async  Task<IActionResult> Delete([FromRoute] int id, [FromHeader(Name = "Authorization")] string? jwt){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            jwt = jwt.Substring(7);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            User user = await Passwords.decodeToken(jwt, _configuration);

            Transaction? transactionModel = await _transactionRepository.DeleteAsync(id, user.id);

            if (transactionModel == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}