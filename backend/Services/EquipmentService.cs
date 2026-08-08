using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.services.equipments;

public class EquipmentService{
    private readonly AppDbContext _context;

    public EquipmentService(AppDbContext context){
        _context = context;
    }

    public Equipment? GetEquipmentById(int id){
        return _context.Equipments.Include(e => e.Type).FirstOrDefault(e => e.Id == id);
    }

    public Equipment? CreateEquipment(string name, int typeId){
        var type = _context.EquipmentTypes.FirstOrDefault(t => t.Id == typeId);
        if (type == null) return null;
        var equipment = new Equipment{Name = name,TypeID = typeId};
        _context.Equipments.Add(equipment);
        _context.SaveChanges();
        return equipment;
    }

    public List<Equipment> GetAllEquipments(){
        return _context.Equipments.Include(e => e.Type).ToList();
    }

  public bool UpdateEquipment(int id, string? name, int? typeID){
        var equipment = _context.Equipments.FirstOrDefault(e => e.Id == id);
        if (equipment == null) return false;
        if (name != null) equipment.Name = name;
        if (typeID.HasValue) equipment.TypeID = typeID.Value;
        _context.SaveChanges();
        return true;
    }

    public bool DeleteEquipment(int id){
        var equipment = _context.Equipments.FirstOrDefault(e => e.Id == id);
        if (equipment == null) return false;
        _context.Equipments.Remove(equipment);
        _context.SaveChanges();
        return true;
    }
}