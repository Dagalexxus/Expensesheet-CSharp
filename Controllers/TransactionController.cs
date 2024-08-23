using api.Models;
using api.Data;
using api.Mappers;
using api.Dtos.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers{
    [Route("api/[Controller]")]
    [ApiController]
    public class TransactionController : ControllerBase{
        private readonly ApplicationDBContext _context;
        public TransactionController(ApplicationDBContext applicationDBContext)
        {
            _context = applicationDBContext; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            List<Transaction> transactions = await _context.Transaction.ToListAsync();

            List<TransactionDTO> transactionDTOs = transactions.Select(t => t.toTransactionDTO())
                                                            .ToList();

            return Ok(transactionDTOs);
        }

        [HttpGet("{Id}")]
        public async  Task<IActionResult> GetById([FromRoute] int id){
            api.Models.Transaction? transaction = await _context.Transaction.FindAsync(id);

            if (transaction == null){
                return NotFound();
            }
             
            return Ok(transaction.toTransactionDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] InsertTransactionRequestDto transactionDTO){
            Transaction transactionModel = transactionDTO.ToTransactionFromInsertDto();
            await _context.Transaction.AddAsync(transactionModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = transactionModel.Id}, transactionModel.toTransactionDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTransactionRequestDto updateDto){
            Transaction? transactionModel = await _context.Transaction.FindAsync(id);

            if (transactionModel == null){
                return NotFound();
            }

            transactionModel.Amount = updateDto.Amount;
            transactionModel.Category = updateDto.Category;
            transactionModel.Comment = updateDto.Comment;
            transactionModel.PaymentProvider = updateDto.PaymentProvider;
            transactionModel.userID = updateDto.userID;

            await _context.SaveChangesAsync();
            return Ok(transactionModel.toTransactionDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async  Task<IActionResult> Delete([FromRoute] int id){
            Transaction? transactionModel = await _context.Transaction.FindAsync(id);

            if (transactionModel == null){
                return NotFound();
            }

            _context.Transaction.Remove(transactionModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}