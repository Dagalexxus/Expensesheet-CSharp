using api.Models;
using api.Data;
using api.Mappers;
using api.Dtos.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            List<Transaction> transactions = await _transactionRepository.GetAllAsync();

            List<TransactionDTO> transactionDTOs = transactions.Select(t => t.toTransactionDTO())
                                                            .ToList();

            return Ok(transactionDTOs);
        }

        [HttpGet("{Id}")]
        public async  Task<IActionResult> GetById([FromRoute] int id){
            api.Models.Transaction? transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null){
                return NotFound();
            }
             
            return Ok(transaction.toTransactionDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] InsertTransactionRequestDto transactionDTO){
            Transaction transactionModel = transactionDTO.ToTransactionFromInsertDto();
            await _transactionRepository.InsertAsync(transactionModel);
            return CreatedAtAction(nameof(GetById), new { id = transactionModel.Id}, transactionModel.toTransactionDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTransactionRequestDto updateDto){
            Transaction? transactionModel = await _transactionRepository.UpdateAsync(id, updateDto);

            if (transactionModel == null){
                return NotFound();
            }

            return Ok(transactionModel.toTransactionDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async  Task<IActionResult> Delete([FromRoute] int id){
            Transaction? transactionModel = await _transactionRepository.DeleteAsync(id);

            if (transactionModel == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}