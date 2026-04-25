using System.Linq.Expressions;
using InsureTrust.API.Data;
using InsureTrust.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InsureTrust.API.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate);
    Task<User?> GetLastUserAsync();
}

public interface IPolicyRepository
{
    Task<PolicyType?> GetPolicyTypeByIdAsync(int id);
    Task<IEnumerable<PolicyType>> GetAllPolicyTypesAsync();
    Task<PolicyType> AddPolicyTypeAsync(PolicyType policyType);
    Task UpdatePolicyTypeAsync(PolicyType policyType);
    Task<IEnumerable<PolicyTerm>> GetTermsByPolicyTypeAsync(int policyTypeId);
    Task<IEnumerable<PolicyRequiredField>> GetFieldsByPolicyTypeAsync(int policyTypeId);
    Task<UserPolicy?> GetUserPolicyByIdAsync(int id);
    Task<IEnumerable<UserPolicy>> GetUserPoliciesAsync(int userId);
    Task<IEnumerable<UserPolicy>> GetAllPoliciesAsync();
    Task<IEnumerable<UserPolicy>> GetPendingPoliciesAsync();
    Task<UserPolicy> AddUserPolicyAsync(UserPolicy policy);
    Task UpdateUserPolicyAsync(UserPolicy policy);
    Task DeleteUserPolicyAsync(UserPolicy policy);
}

public interface IClaimRepository
{
    Task<Claim?> GetByIdAsync(int id);
    Task<IEnumerable<Claim>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Claim>> GetAllAsync();
    Task<Claim> AddAsync(Claim claim);
    Task UpdateAsync(Claim claim);
}

public interface ISupportRepository
{
    Task<SupportQuery?> GetByIdAsync(int id);
    Task<IEnumerable<SupportQuery>> GetByUserIdAsync(int userId);
    Task<IEnumerable<SupportQuery>> GetAllAsync();
    Task<SupportQuery> AddAsync(SupportQuery query);
    Task UpdateAsync(SupportQuery query);
}

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
    Task<int> GetUnreadCountAsync(int userId);
    Task<Notification> AddAsync(Notification notification);
    Task UpdateAsync(Notification notification);
    Task MarkAllReadAsync(int userId);
}

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetByUserIdAsync(int userId);
    Task<Payment> AddAsync(Payment payment);
}

// --- Implementations ---

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) => _db = db;

    public async Task<User?> GetByIdAsync(int id) => await _db.Users.FindAsync(id);
    public async Task<User?> GetByEmailAsync(string email) => await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
    public async Task<IEnumerable<User>> GetAllAsync() => await _db.Users.OrderByDescending(u => u.CreatedAt).ToListAsync();
    public async Task<User> AddAsync(User user) { _db.Users.Add(user); await _db.SaveChangesAsync(); return user; }
    public async Task UpdateAsync(User user) { _db.Users.Update(user); await _db.SaveChangesAsync(); }
    public async Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate) => await _db.Users.AnyAsync(predicate);
    public async Task<User?> GetLastUserAsync() => await _db.Users.OrderByDescending(u => u.Id).FirstOrDefaultAsync();
}

public class PolicyRepository : IPolicyRepository
{
    private readonly AppDbContext _db;
    public PolicyRepository(AppDbContext db) => _db = db;

    public async Task<PolicyType?> GetPolicyTypeByIdAsync(int id) => await _db.PolicyTypes.Include(p => p.PolicyTerms).Include(p => p.RequiredFields).FirstOrDefaultAsync(p => p.Id == id);
    public async Task<IEnumerable<PolicyType>> GetAllPolicyTypesAsync() => await _db.PolicyTypes.Where(p => p.IsActive).OrderBy(p => p.Name).ToListAsync();
    public async Task<PolicyType> AddPolicyTypeAsync(PolicyType pt) { _db.PolicyTypes.Add(pt); await _db.SaveChangesAsync(); return pt; }
    public async Task UpdatePolicyTypeAsync(PolicyType pt) { _db.PolicyTypes.Update(pt); await _db.SaveChangesAsync(); }
    public async Task<IEnumerable<PolicyTerm>> GetTermsByPolicyTypeAsync(int id) => await _db.PolicyTerms.Where(t => t.PolicyTypeId == id).ToListAsync();
    public async Task<IEnumerable<PolicyRequiredField>> GetFieldsByPolicyTypeAsync(int id) => await _db.PolicyRequiredFields.Where(f => f.PolicyTypeId == id).ToListAsync();
    public async Task<UserPolicy?> GetUserPolicyByIdAsync(int id) => await _db.UserPolicies.Include(p => p.PolicyType).Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
    public async Task<IEnumerable<UserPolicy>> GetUserPoliciesAsync(int userId) => await _db.UserPolicies.Include(p => p.PolicyType).Where(p => p.UserId == userId).OrderByDescending(p => p.PurchaseDate).ToListAsync();
    public async Task<IEnumerable<UserPolicy>> GetAllPoliciesAsync() => await _db.UserPolicies.Include(p => p.PolicyType).Include(p => p.User).OrderByDescending(p => p.PurchaseDate).ToListAsync();
    public async Task<IEnumerable<UserPolicy>> GetPendingPoliciesAsync() => await _db.UserPolicies.Include(p => p.PolicyType).Include(p => p.User).Where(p => p.Status == "Pending").OrderBy(p => p.PurchaseDate).ToListAsync();
    public async Task<UserPolicy> AddUserPolicyAsync(UserPolicy policy) { _db.UserPolicies.Add(policy); await _db.SaveChangesAsync(); return policy; }
    public async Task UpdateUserPolicyAsync(UserPolicy policy) { _db.UserPolicies.Update(policy); await _db.SaveChangesAsync(); }
    public async Task DeleteUserPolicyAsync(UserPolicy policy) { _db.UserPolicies.Remove(policy); await _db.SaveChangesAsync(); }
}

public class ClaimRepository : IClaimRepository
{
    private readonly AppDbContext _db;
    public ClaimRepository(AppDbContext db) => _db = db;

    public async Task<Claim?> GetByIdAsync(int id) => await _db.Claims.Include(c => c.UserPolicy).ThenInclude(up => up.PolicyType).Include(c => c.UserPolicy).ThenInclude(up => up.User).FirstOrDefaultAsync(c => c.Id == id);
    public async Task<IEnumerable<Claim>> GetByUserIdAsync(int userId) => await _db.Claims.Include(c => c.UserPolicy).ThenInclude(up => up.PolicyType).Where(c => c.UserPolicy.UserId == userId).OrderByDescending(c => c.ClaimDate).ToListAsync();
    public async Task<IEnumerable<Claim>> GetAllAsync() => await _db.Claims.Include(c => c.UserPolicy).ThenInclude(up => up.PolicyType).Include(c => c.UserPolicy).ThenInclude(up => up.User).OrderByDescending(c => c.ClaimDate).ToListAsync();
    public async Task<Claim> AddAsync(Claim claim) { _db.Claims.Add(claim); await _db.SaveChangesAsync(); return claim; }
    public async Task UpdateAsync(Claim claim) { _db.Claims.Update(claim); await _db.SaveChangesAsync(); }
}

public class SupportRepository : ISupportRepository
{
    private readonly AppDbContext _db;
    public SupportRepository(AppDbContext db) => _db = db;

    public async Task<SupportQuery?> GetByIdAsync(int id) => await _db.SupportQueries.Include(s => s.User).FirstOrDefaultAsync(s => s.Id == id);
    public async Task<IEnumerable<SupportQuery>> GetByUserIdAsync(int userId) => await _db.SupportQueries.Include(s => s.User).Where(s => s.UserId == userId).OrderByDescending(s => s.CreatedAt).ToListAsync();
    public async Task<IEnumerable<SupportQuery>> GetAllAsync() => await _db.SupportQueries.Include(s => s.User).OrderByDescending(s => s.CreatedAt).ToListAsync();
    public async Task<SupportQuery> AddAsync(SupportQuery query) { _db.SupportQueries.Add(query); await _db.SaveChangesAsync(); return query; }
    public async Task UpdateAsync(SupportQuery query) { _db.SupportQueries.Update(query); await _db.SaveChangesAsync(); }
}

public class NotificationRepository : INotificationRepository
{
    private readonly AppDbContext _db;
    public NotificationRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Notification>> GetByUserIdAsync(int userId) => await _db.Notifications.Where(n => n.UserId == userId).OrderByDescending(n => n.CreatedAt).ToListAsync();
    public async Task<int> GetUnreadCountAsync(int userId) => await _db.Notifications.CountAsync(n => n.UserId == userId && !n.IsRead);
    public async Task<Notification> AddAsync(Notification n) { _db.Notifications.Add(n); await _db.SaveChangesAsync(); return n; }
    public async Task UpdateAsync(Notification n) { _db.Notifications.Update(n); await _db.SaveChangesAsync(); }
    public async Task MarkAllReadAsync(int userId) { await _db.Notifications.Where(n => n.UserId == userId && !n.IsRead).ExecuteUpdateAsync(s => s.SetProperty(n => n.IsRead, true)); }
}

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _db;
    public PaymentRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Payment>> GetByUserIdAsync(int userId) => await _db.Payments.Include(p => p.UserPolicy).ThenInclude(up => up.PolicyType).Where(p => p.UserId == userId).OrderByDescending(p => p.PaymentDate).ToListAsync();
    public async Task<Payment> AddAsync(Payment payment) { _db.Payments.Add(payment); await _db.SaveChangesAsync(); return payment; }
}
