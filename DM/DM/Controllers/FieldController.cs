using Microsoft.AspNetCore.Mvc;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/field")]
    public class FieldController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpPost]
        public IActionResult Create(FieldModel fieldModel)
        {
            var checker = _fieldService.Create(fieldModel);

            return Ok(checker);
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpDelete]
        public IActionResult Delete(long fieldId)
        {
            var checker = _fieldService.Delete(fieldId);

            if (checker == false)
            {
                return NotFound();
            }

            return Ok(checker);
        }
    }

    [ApiController]
    [Route("api/listField")]
    public class ListFieldController : ControllerBase
    {
        private readonly IListFieldService _listFieldService;

        public ListFieldController(IListFieldService listFieldService)
        {
            _listFieldService = listFieldService;
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpPost]
        public IActionResult Create(ListFieldModel listFieldModel)
        {
            var checker = _listFieldService.Create(listFieldModel);

            return Ok(checker);
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpDelete]
        public IActionResult Delete(long listFieldId)
        {
            var checker = _listFieldService.Delete(listFieldId);

            if (checker == false)
            {
                return NotFound();
            }

            return Ok(checker);
        }
    }
}
