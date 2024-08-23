using api.Models;
using api.Mappers;
using api.Dtos.Transaction;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;

namespace api.Controllers{
    [Route("api/[Controller]")]
    [ApiController]
    public class TransactionController : ControllerBase{
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            List<Transaction> transactions = await _transactionRepository.GetAllAsync();

            List<TransactionDTO> transactionDTOs = transactions.Select(t => t.toTransactionDTO())
                                                            .ToList();

            return Ok(transactionDTOs);
        }

        [HttpGet("{Id:int}")]
        public async  Task<IActionResult> GetById([FromRoute] int id){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            api.Models.Transaction? transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null){
                return NotFound();
            }
             
            return Ok(transaction.toTransactionDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] InsertTransactionRequestDto transactionDTO){
            if (!ModelState.IsValid){
                    return BadRequest(ModelState);
                }
            Transaction transactionModel = transactionDTO.ToTransactionFromInsertDto();
            await _transactionRepository.InsertAsync(transactionModel);
            return CreatedAtAction(nameof(GetById), new { id = transactionModel.Id}, transactionModel.toTransactionDTO());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTransactionRequestDto updateDto){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            Transaction? transactionModel = await _transactionRepository.UpdateAsync(id, updateDto);

            if (transactionModel == null){
                return NotFound();
            }

            return Ok(transactionModel.toTransactionDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async  Task<IActionResult> Delete([FromRoute] int id){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            Transaction? transactionModel = await _transactionRepository.DeleteAsync(id);

            if (transactionModel == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}