using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TqlPoWebApi.Data;
using TqlPoWebApi.Models;

namespace TqlPoWebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PosController : ControllerBase {
        private readonly PoContext _context;

        public PosController(PoContext context) {
            _context = context;
        }

        // GET: api/Pos
        [HttpGet("reviews")]
        public async Task<ActionResult<IEnumerable<Po>>> GetPosInReview() {
            return await _context.Pos
                                .Where(p => p.Status == Po.StatusReview)
                                .Include(p => p.Employee)
                                .ToListAsync();
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> PutPoToRejected(int id) {

            var po = await _context.Pos.FindAsync(id);
            if(po == null) {
                return NotFound();
            }

            po.Status = Po.StatusRejected;

            return await PutPo(id, po);
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> PutPoToApproved(int id) {

            var po = await _context.Pos.FindAsync(id);
            if(po == null) {
                return NotFound();
            }

            po.Status = Po.StatusApproved;

            return await PutPo(id, po);
        }

        [HttpPut("{id}/review")]
        public async Task<IActionResult> PutPoToReviewOrApproved(int id) {

            var po = await _context.Pos.FindAsync(id);
            if(po == null) {
                return NotFound();
            }
            
            po.Status = (po.Total > 0 && po.Total <= 100) ? Po.StatusApproved : Po.StatusReview;

            return await PutPo(id, po);
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> PutPoToEdit(int id) {

            var po = await _context.Pos.FindAsync(id);
            if(po == null) {
                return NotFound();
            }

            po.Status = Po.StatusEdit;

            return await PutPo(id, po);
        }
        
        // GET: api/Pos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Po>>> GetPos() {
            return await _context.Pos
                                .ToListAsync();
        }

        // GET: api/Pos/empl
        [HttpGet("empl")]
        public async Task<ActionResult<IEnumerable<Po>>> GetPosWithEmpl() {
            return await _context.Pos
                                .Include(x => x.Employee)
                                .ToListAsync();
        }

        // GET: api/Pos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Po>> GetPo(int id) {
            var po = await _context.Pos
                                .FindAsync(id);

            if(po == null) {
                return NotFound();
            }

            return po;
        }
        // GET: api/Pos/5
        [HttpGet("{id}/empl")]
        public async Task<ActionResult<Po>> GetPoWithEmpl(int id) {
            var po = await _context.Pos
                                .Include(x => x.Employee)
                                .SingleOrDefaultAsync(x => x.Id == id);

            if(po == null) {
                return NotFound();
            }

            return po;
        }

        // PUT: api/Pos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPo(int id, Po po) {
            if(id != po.Id) {
                return BadRequest();
            }

            _context.Entry(po).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                if(!PoExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Po>> PostPo(Po po) {
            _context.Pos.Add(po);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPo", new { id = po.Id }, po);
        }

        // DELETE: api/Pos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Po>> DeletePo(int id) {
            var po = await _context.Pos.FindAsync(id);
            if(po == null) {
                return NotFound();
            }

            _context.Pos.Remove(po);
            await _context.SaveChangesAsync();

            return po;
        }

        private bool PoExists(int id) {
            return _context.Pos.Any(e => e.Id == id);
        }
    }
}
