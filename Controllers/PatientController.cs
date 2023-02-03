using ACME.Data;
using ACME.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ACME.Controllers
{
    [Route("patients")]
    public class PatientController : ControllerBase
    {
        [Route("list")]
        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetAll(
            [FromServices] DataContext context
        )
        {
            try
            {
                var patients = await context.Patients.ToListAsync();
                return Ok(patients);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possivel listar os pacientes" });
            }
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<Patient>> Register(
            [FromBody] Patient model,
            [FromServices] DataContext context
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var pacientAlreadyExists = await context.Patients.FirstOrDefaultAsync(x => x.Cpf == model.Cpf);
                if (pacientAlreadyExists != null)
                    return BadRequest(new { message = "Paciente já cadastrado" });

                context.Patients.Add(model);
                await context.SaveChangesAsync();
                return Ok(new { message = "Paciente cadastrado com sucesso" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível cadastrar o paciente" });
            }
        }

        [Route("{id:int}/update")]
        [HttpPut]
        public async Task<ActionResult<Patient>> Update(
            int id,
            [FromBody] Patient model,
            [FromServices] DataContext context
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                var patientFound = await context.Patients.FirstOrDefaultAsync(x => x.Id == id);
                if (patientFound == null)
                    return BadRequest(new { message = "Paciente não encontrado" });

                patientFound.UpdateValues(
                    model.Name,
                    model.BirthDate,
                    model.Cpf,
                    model.Gender,
                    model.AddressLine,
                    model.AddresNumber,
                    model.District,
                    model.City,
                    model.State,
                    model.ZipCode
                    );

                await context.SaveChangesAsync();
                return Ok(new { message = "Dados atualizados com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id:int}/delete")]
        [HttpDelete]
        public async Task<ActionResult<Patient>> Delete(
            int id,
            [FromServices] DataContext context
            )
        {

            try
            {
                var pacientFound = await context.Patients.FirstOrDefaultAsync(x => x.Id == id);
                if (pacientFound == null)
                    return BadRequest(new { message = "Paciente não encontrado" });

                pacientFound.Inactivate();
                await context.SaveChangesAsync();

                return Ok(new { message = "Paciente desativado com sucesso" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível desativar o paciente" });
            }

        }

        [Route("{id:int}/reactivate")]
        [HttpPut]
        public async Task<ActionResult<Patient>> Reactivate(
            int id,
            [FromServices] DataContext context
            )
        {

            try
            {
                var pacientFound = await context.Patients.FirstOrDefaultAsync(x => x.Id == id);
                if (pacientFound == null)
                    return BadRequest(new { message = "Paciente não encontrado" });

                pacientFound.Reactivate();
                await context.SaveChangesAsync();

                return Ok(new { message = "Paciente Reativado com sucesso" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível Reativar o paciente" });
            }

        }
    }
}