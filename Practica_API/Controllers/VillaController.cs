using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica_API.Datos;
using Practica_API.Modelos;
using Practica_API.Modelos.DTOs;

namespace Practica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            logger.LogInformation("Obtener las villas");

            IEnumerable<Villa> villaList = await context.Villas.ToListAsync();

            return Ok(mapper.Map<IEnumerable<VillaDTO>>(villaList));
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0) { return BadRequest(); }

            var villa = await context.Villas.FirstOrDefaultAsync(x => x.Id == id);

            if (villa == null) { return NotFound(); }

            return Ok(mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CrearVilla([FromBody] VillaCreateDTO villaCreateDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (await context.Villas.FirstOrDefaultAsync(x => x.Nombre.ToLower() == villaCreateDTO.Nombre.ToLower()) !=null) 
            { 
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (villaCreateDTO == null) { return BadRequest(villaCreateDTO); }

            Villa modelo = mapper.Map<Villa>(villaCreateDTO);

            await context.Villas.AddAsync(modelo);
            await context.SaveChangesAsync();
   
            return CreatedAtRoute("GetVilla", new {id = modelo.Id}, modelo);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0) { return BadRequest(); }

            var villa = await context.Villas.FirstOrDefaultAsync(v => v.Id == id);

            if (villa == null) { return NotFound(); }

            context.Villas.Remove(villa);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaUpdateDTO)
        {
            if (villaUpdateDTO == null || id != villaUpdateDTO.Id) { return BadRequest(); }

            Villa modelo = mapper.Map<Villa>(villaUpdateDTO);
           
            context.Update(modelo);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0) { return BadRequest(); }

            var villa = await context.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            VillaUpdateDTO villaDTO = mapper.Map<VillaUpdateDTO>(villa);

            if (villa == null) return BadRequest();

            patchDTO.ApplyTo(villaDTO, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            Villa modelo = mapper.Map<Villa>(villaDTO);

            context.Villas.Update(modelo);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
