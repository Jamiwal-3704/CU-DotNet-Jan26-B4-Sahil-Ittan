using AutoMapper;
using LoanManagerAPI.Data;
using LoanManagerAPI.DTOs;
using LoanManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly LoanManagerAPIContext _context;
        private readonly IMapper _mapper;

        public LoansController(LoanManagerAPIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET ALL LOANS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanReadDTO>>> GetLoans()
        {
            var loans = await _context.Loan.ToListAsync();
            return Ok(_mapper.Map<List<LoanReadDTO>>(loans));
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanReadDTO>> GetLoan(int id)
        {
            var loan = await _context.Loan.FindAsync(id);

            if (loan == null)
                return NotFound();

            return Ok(_mapper.Map<LoanReadDTO>(loan));
        }

        // ✅ CREATE LOAN (POST)
        [HttpPost]
        public async Task<ActionResult<LoanReadDTO>> CreateLoan(LoanCreateDTO dto)
        {
            var loan = _mapper.Map<Loan>(dto);

            // default approval = false
            loan.IsApproved = false;

            _context.Loan.Add(loan);
            await _context.SaveChangesAsync();

            var readDto = _mapper.Map<LoanReadDTO>(loan);

            return CreatedAtAction(nameof(GetLoan), new { id = loan.Id }, readDto);
        }

        // ✅ UPDATE FULL LOAN (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, LoanUpdateDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var loan = await _context.Loan.FindAsync(id);

            if (loan == null)
                return NotFound();

            // AutoMapper mapping
            _mapper.Map(dto, loan);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ ADMIN APPROVAL (PATCH)
        [HttpPatch("approve")]
        public async Task<IActionResult> ApproveLoan(LoanAdminDTO dto)
        {
            var loan = await _context.Loan.FindAsync(dto.Id);

            if (loan == null)
                return NotFound();

            loan.IsApproved = dto.IsApproved;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE LOAN
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loan = await _context.Loan.FindAsync(id);

            if (loan == null)
                return NotFound();

            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}