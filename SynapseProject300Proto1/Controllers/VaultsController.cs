using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SynapseProject300Proto1.Controllers.Model;
using SynapseProject300Proto1.Models;

namespace SynapseProject300Proto1.Controllers
{
    [Route("api/[controller]")] //url for api - https://localhost:PORT/api/vaults
    [ApiController] //this class handles api requests and responses (vaults)
    public class VaultsController : ControllerBase
    {
        private readonly SynapseDbContext _context; //adds reference to db context
        public VaultsController(SynapseDbContext context)
        {
            _context = context;
            //injects db context into controller class
        }

        #region Vaults CRUD Operations
        [HttpGet] //responds to GET requests
        public async Task<ActionResult<IEnumerable<Vault>>> GetVaults()
        {
            return await _context.Vaults.ToListAsync();
            //_context.Vaults is a DbSet<Vault> representing the Vaults table in the database
        }

        [HttpPost] //posts
        public async Task<ActionResult<Vault>> CreateVault(Vault vault)
        {
            _context.Vaults.Add(vault);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVaultById), new { id = vault.VaultId }, vault);
        }

        [HttpGet("{id}")] //gets by id
        public async Task<ActionResult<Vault>> GetVaultById(int id)
        {
            var vault = await _context.Vaults.FindAsync(id);
            if (vault == null)
            {
                return NotFound();
                //returns a 404 (not found)
            }
            return Ok(vault); //returns a 200 (success) and the vault
        }

        [HttpPut("{id}")] //updates by id
        public async Task<IActionResult> UpdateVault(int id, Vault updatedVault)
        {
            var vault = await _context.Vaults.FindAsync(id);
            if (vault == null)
            {
                return NotFound(); //returns a 404 (not found)
            }
            vault.Name = updatedVault.Name;
            await _context.SaveChangesAsync();
            return NoContent(); //returns a 204 (success) with no content
        }

        [HttpDelete("{id}")] //deletes by id
        public async Task<IActionResult> DeleteVault(int id)
        {
            var vault = await _context.Vaults.FindAsync(id);
            if (vault == null)
            {
                return NotFound(); //returns a 404 (not found)
            }
            _context.Vaults.Remove(vault);
            await _context.SaveChangesAsync();
            return NoContent(); //returns a 204 (success) with no content
        }
        #endregion
    }
}