using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace WB.EntrevistaABP.Pasajeros;

public class PasajeroManager : DomainService
{
    // private readonly UserManager<IdentityUser> _userManager;
    private readonly IPasajeroRepository? _pasajeroRepository;
    private readonly IIdentityUserRepository  _userRepository;
       private readonly IdentityUserManager _userManager;
    public PasajeroManager(
       IPasajeroRepository pasajeroRepository,
      IIdentityUserRepository  userRepository,
       IdentityUserManager userManager
        )
    {
        _pasajeroRepository = pasajeroRepository;
        _userManager = userManager;
        _userRepository= userRepository;
    }

    public async Task<Pasajero> CreateAsync(
        [NotNull] string nombre,
        [NotNull] string apellido,
        [NotNull] string dni,
        [NotNull] string email,
        [NotNull] DateTime fecha_de_nacimiento
    )
    {   
         var usuario = await _userManager.FindByEmailAsync(email);
         if(usuario != null)
         {
            throw new EmailExistingException(usuario.Email);
         }
         var dniPasajero = await _pasajeroRepository.FirstOrDefaultAsync(p => p.DNI == dni);
         
         if(dniPasajero != null){
             throw new PasajeroAlreadyExistsException(dniPasajero.DNI);
         }

        var existingPasajero = await _pasajeroRepository.FindByPasajeroAsync(nombre,apellido, dni,fecha_de_nacimiento);

        if (existingPasajero != null)
        {
            existingPasajero.IsDeleted = false; 
            await _pasajeroRepository.UpdateAsync(existingPasajero);
            return existingPasajero;
        }
        else
        {
             var user = new IdentityUser(
                GuidGenerator.Create(),
                dni,
                email);

            var passwordDefault = "1q2w3E*";
            await _userManager.CreateAsync(user, passwordDefault);
            var userId = user.Id;

            var pasajero = new Pasajero(
                GuidGenerator.Create(),
                nombre,
                apellido,
                dni,
                email,
                fecha_de_nacimiento,
                userId
                );
            return pasajero;
        }
        }
                  
    }

