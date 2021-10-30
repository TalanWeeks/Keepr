using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _profilesRepository;

    public ProfilesService(ProfilesRepository profilesRepository)
    {
      _profilesRepository = profilesRepository;
    }

    public Profile GetProfileById(string profileId)
    {
      Profile profile =  _profilesRepository.GetProfileById(profileId);
      if (profile == null)
      {
        throw new System.Exception("Invalid Id");
      }
      return profile;
    }
  }
}