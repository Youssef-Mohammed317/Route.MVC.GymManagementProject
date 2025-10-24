using AutoMapper;
using GymManagement.DAL.Entites;
using GymManagement.BLL.ViewModels.SessionViewModel;
using GymManagement.DAL.Repositories.Interfaces;
using System.Linq;

public class AvailableSlotsResolver : IValueResolver<Session, SessionViewModel, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public AvailableSlotsResolver(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public int Resolve(Session src, SessionViewModel dest, int destMember, ResolutionContext context)
    {
        var bookedCount = _unitOfWork.MemberSessionRepository
            .GetAll()
            .Count(ms => ms.SessionId == src.Id);

        return src.Capacity - bookedCount;
    }
}
