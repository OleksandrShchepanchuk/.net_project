using AutoMapper;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly HospitalContext _context;
        private readonly IMapper _mapper;

        public DoctorsController(HospitalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetDoctors()
        {
            //var doctors = await _context.Doctors.ToListAsync();
            var doctors = await _context.Doctors
                .Include(d => d.Reviews)
                .ToListAsync();

            foreach(var doc in doctors)
            {
                if(doc.Reviews.Count!=0)
                {
                    doc.Rating= doc.Reviews.Average(d => d.Rating);
                }
                else
                {
                    //doc.Rating = 4.5;
                    doc.Rating = 0;
                }
            }

            return _mapper.Map<List<DoctorDTO>>(doctors);
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDTO>> GetDoctor(int id)
        {
            //var doctor = await _context.Doctors.FindAsync(id);
            var doctor = await _context.Doctors.Include(d => d.Reviews).FirstOrDefaultAsync(d => d.DoctorId == id);

            if (doctor == null)
            {
                return NotFound();
            }

            if (doctor.Reviews.Count != 0)
            {
                doctor.Rating = doctor.Reviews.Average(d => d.Rating);
            }
            else
            {
                //doc.Rating = 4.5;
                doctor.Rating = 0;
            }

            return _mapper.Map<DoctorDTO>(doctor);
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, DoctorDTO doctorDTO)
        {
            //if (id != doctorDTO.DoctorId)
            //{
            //    return BadRequest();
            //}

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _mapper.Map(doctorDTO, doctor);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Doctors
        [HttpPost]
        public async Task<ActionResult<DoctorDTO>> PostDoctor(DoctorDTO doctorDTO)
        {
            var doctor = _mapper.Map<Doctor>(doctorDTO);
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.DoctorId }, doctorDTO);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorId == id);
        }
    }
}
