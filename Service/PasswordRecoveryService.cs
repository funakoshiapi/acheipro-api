using AutoMapper;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared;

namespace Service;

public class PasswordRecoveryService : IPasswordRecoveryService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private  User? _user;

    public PasswordRecoveryService(UserManager<User> userManager, IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper ) 
    {
        _userManager = userManager;
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<bool> UpdatePassword(PasswordRecoveryDto passwordRecovery, Guid id)
    {
        _user = await _userManager.FindByEmailAsync( passwordRecovery.Email );
        var entity = await  _repository.PasswordRecovery.GetPasswordRecovery(id);
        
        if (_user != null && entity != null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(_user);
            var result = await _userManager.ResetPasswordAsync(_user, token, passwordRecovery.password);
            
            if(result.Succeeded)
            {
                return result.Succeeded;
            }
        }
        return false;
    }

    public async Task<PasswordRecovery?> GeneratePasswordRequest(PasswordRecoveryDto passwordRecovery)
    {

        var entityPasswordRecovery = _mapper.Map<PasswordRecovery>(passwordRecovery);
        

        _user = await _userManager.FindByEmailAsync(passwordRecovery.Email);

        if(_user != null)
        {
            _repository.PasswordRecovery.Request(entityPasswordRecovery);
            await _repository.SaveAsync();
            return entityPasswordRecovery;
        }

        else{
            return null;
        }

    }

    public async Task DeleteRequest(Guid id)
    {
        var entity = await  _repository.PasswordRecovery.GetPasswordRecovery(id);
        
        if (entity != null){
            _repository.PasswordRecovery.DeleteRequest(entity);
            await _repository.SaveAsync();
        }
                
    }
}
