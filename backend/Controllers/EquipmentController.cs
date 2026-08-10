using backend.DTOs.Equipment;
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

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult AddEquipment([FromBody] EquipmentCreateDTO request){
        var equipment = _equipmentService.CreateEquipment(request.Nome,request.TipoId);
        if (equipment == null) return NotFound("Tipo de equipamento não encontrado.");
        var result = new EquipmentResponseDTO{Id = equipment.Id,Nome = equipment.Name,Tipo = new EquipmentTypeDTO{Id = equipment.Type.Id,Nome = equipment.Type.Name}};
        return StatusCode(201, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult UpdateEquipment(int id, [FromBody] EquipmentUpdateDTO request){
        var result = _equipmentService.UpdateEquipment(id,request.Nome,request.Tipo?.Id);
        if (!result) return NotFound();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
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
        var result = new EquipmentResponseDTO{Id = equipment.Id,Nome = equipment.Name,Tipo = new EquipmentTypeDTO{Id = equipment.Type.Id,Nome = equipment.Type.Name}};
        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetAllEquipments(){
        var equipments = _equipmentService.GetAllEquipments();
        var result = equipments.Select(e => new EquipmentResponseDTO{
            Id = e.Id,
            Nome = e.Name,
            Tipo = e.Type != null ? new EquipmentTypeDTO{Id = e.Type.Id,Nome = e.Type.Name} : null
        }).ToList();
        return Ok(new { equipamentos = result });
    }
}

