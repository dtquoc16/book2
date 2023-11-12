using BL.BaseBL;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace book_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<T> : Controller
    {
        private readonly IBaseBL<T> _baseBL;

        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        [HttpGet("{recordId:Guid}")]
        public virtual async Task<IActionResult> GetRecordById([FromRoute] Guid recordId)
        {
            var record = await _baseBL.GetRecordByIdAsync(recordId);

            return Ok(record);

        }

        [HttpPost]
        public virtual async Task<IActionResult> InsertAsync([FromBody] T entity)
        {
            await _baseBL.InsertRecordAsync(entity);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMulti([FromBody] List<Guid> listId)
        {
            await _baseBL.DeleteMultiAsync(listId);
            return Ok();
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] T entity)
        {
            await _baseBL.UpdateRecordAsync(id, entity);
            return Ok();
        }

    }
}
