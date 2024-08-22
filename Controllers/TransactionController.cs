using api.Models;
using api.Data;
using api.Mappers;
using api.Dtos.Transaction;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll(){
            List<api.Dtos.Transaction.TransactionDTO> transactions = _context.Transaction.ToList()
                                                                        .Select(t => t.toTransactionDTO())
                                                                        .ToList();

            return Ok(transactions);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById([FromRoute] int id){
            api.Models.Transaction? transaction = _context.Transaction.Find(id);

            if (transaction == null){
                return NotFound();
            }
             
            return Ok(transaction.toTransactionDTO());
        }

        [HttpPost]
        public IActionResult Insert([FromBody] InsertTransactionRequestDto transactionDTO){
            Transaction transactionModel = transactionDTO.ToTransactionFromInsertDto();
            _context.Transaction.Add(transactionModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = transactionModel.Id}, transactionModel.toTransactionDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateTransactionRequestDto updateDto){
            Transaction? transactionModel = _context.Transaction.Find(id);

            if (transactionModel == null){
                return NotFound();
            }

            transactionModel.Amount = updateDto.Amount;
            transactionModel.Category = updateDto.Category;
            transactionModel.Comment = updateDto.Comment;
            transactionModel.PaymentProvider = updateDto.PaymentProvider;
            transactionModel.userID = updateDto.userID;

            _context.SaveChanges();
            return Ok(transactionModel.toTransactionDTO());
        }
    }
}