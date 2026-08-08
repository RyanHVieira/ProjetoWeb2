using backend.services.equipments;
using Backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.EquipmentController;

[ApiController]
[Route("equipments")]
public class EquipmentController : ControllerBase{
    private readonly EquipmentService _equipmentService;

    public EquipmentController(EquipmentService equipmentService){
        _equipmentService = equipmentService;
    }

    [Authorize]
    [HttpPost]
    public IActionResult AddEquipment([FromBody] EquipmentCreateDTO request){
        var equipment = _equipmentService.CreateEquipment(request.Nome,request.Tipo.Id);
        return StatusCode(201, equipment);
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult UpdateEquipment(int id,[FromBody] EquipmentUpdateDTO request){
        var result = _equipmentService.UpdateEquipment(id,request.Nome,request.Tipo.Id);
        if (!result) return NotFound();
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteEquipment(int id){
        var result = _equipmentService.DeleteEquipment(id);
        if (!result) return NotFound();
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetEquipment(int id){
        var equipment = _equipmentService.GetEquipmentById(id);
        if (equipment == null) return NotFound();
        return Ok(equipment);
    }

    [HttpGet]
    public IActionResult GetAllEquipments(){
        var equipments = _equipmentService.GetAllEquipments();
        return Ok(new{equipamentos = equipments});
    }
}

