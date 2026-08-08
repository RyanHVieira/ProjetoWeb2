using backend.Services.equipTypes;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("equipTypes")]
public class EquipmentTypeController : ControllerBase {
     private readonly EquipTypeService _equipTypeService;
     public EquipmentTypeController(EquipTypeService equipTypeService){
        _equipTypeService = equipTypeService;
    }

    [HttpGet]
    public IActionResult GetAllEquipTypes(){
        var equipTypes = _equipTypeService.GetAllEquipTypes();
        return Ok(new{equipTypes = equipTypes});
    }

    [HttpGet("{id}")]
    public IActionResult GetEquipType(int id){
        var equipType = _equipTypeService.GetEquipTypeById(id);
        if (equipType == null) return NotFound();
        return Ok(equipType);
    }

    [HttpPost]
    public IActionResult CreateEquipType([FromBody] string name){
        var equipType = _equipTypeService.CreateEquipType(name);
        return StatusCode(201, equipType);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEquipType(int id, [FromBody] string name){
        var equipType = _equipTypeService.UpdateEquipType(id, name);
        if (equipType == null) return NotFound();
        return Ok(equipType);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEquipType(int id){
        var result = _equipTypeService.DeleteEquipType(id);
        if (!result) return NotFound();
        return Ok();
    }
}
