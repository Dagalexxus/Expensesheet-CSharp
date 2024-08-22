
using System.Transactions;
using api.Data;
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
            List<api.Models.Transaction> transactions = _context.Transaction.ToList();

            return Ok(transactions);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById([FromRoute] int id){
            api.Models.Transaction? transaction = _context.Transaction.Find(id);

            if (transaction == null){
                return NotFound();
            }
             
            return Ok(transaction);
        }
    }
}