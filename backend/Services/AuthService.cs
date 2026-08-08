using backend.Data;
using backend.Models;

namespace backend.services.authservice;

public class AuthService{
    private readonly AppDbContext _context;
    public AuthService(AppDbContext context){
        _context = context;
    }
    
    public User? GetUserById(Guid id){
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public User CreateUser(string username, string passwordHash){
        User user = new User{Username = username, PasswordHash = passwordHash, Role = "user", CreatedAt = DateTime.UtcNow};

        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public List<User> GetAllUsers(){
        return _context.Users.ToList();
    }

    public void UpdateUser(Guid id, string username, string passwordHash, string role){
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user != null){
            user.Username = username;
            user.PasswordHash = passwordHash;
            user.Role = role;
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }

    public void DeleteUser(Guid id){
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user != null){
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}