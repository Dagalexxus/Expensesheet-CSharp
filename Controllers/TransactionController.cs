using api.Models;
using api.Data;
using api.Mappers;
using api.Dtos.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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
    }
}