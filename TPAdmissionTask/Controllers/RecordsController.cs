using Microsoft.AspNetCore.Mvc;
using TPAdmissionTask.Models;
using TPAdmissionTask.Interface;
using TPAdmissionTask.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TPAdmissionTask.Controllers
{
    [Route("records")]
    [ApiController]
    [Authorize]
    public class RecordsController : ControllerBase
    {
        private readonly IRecords? _IRecords;

        public RecordsController(IRecords IRecords)
        {
            _IRecords = IRecords;
        }

        // route => records/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordModel>>> Get()
        {
            if (_IRecords == null)
                return NotFound();

            return await Task.FromResult(_IRecords.GetRecords());
        }

        // route => records/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordModel>> Get(int id)
        {
            var records = await Task.FromResult(_IRecords?.GetRecord(id));
            if (records == null)
                return NotFound();

            return records;
        }

        // route => records/
        [HttpPost]
        public async Task<ActionResult<RecordModel>> Post(RecordModel record)
        {
            _IRecords?.AddRecord(record);
            return await Task.FromResult(record);
        }

        // route => records/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<RecordModel>> Put(int id, RecordModel record)
        {
            if (id != record.RecordId)
                return BadRequest();

            try
            {
                _IRecords?.UpdateRecord(record);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(id))
                    return NotFound();
                else
                    throw;
            }

            return await Task.FromResult(record);
        }

        // route => records/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<RecordModel>> Delete(int id)
        {
            var record = _IRecords?.DeleteRecord(id);

            if (record == null) return NotFound();

            return await Task.FromResult(record);
        }

        private bool RecordExists(int id)
        {
            if (_IRecords == null)
                return false;

            return _IRecords.CheckRecord(id);
        }
    }
}
