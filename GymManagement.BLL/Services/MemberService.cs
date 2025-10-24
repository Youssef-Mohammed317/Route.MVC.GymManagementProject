using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.DAL.Entites;
using GymManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemberService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }


        public ViewResponse<IEnumerable<MemberViewModel>> GetAllMembers()
        {
            var members = unitOfWork.MemberRepository.GetAll()
                .ProjectTo<MemberViewModel>(mapper.ConfigurationProvider);
            if (!members.Any())
            {
                return ViewResponse<IEnumerable<MemberViewModel>>.Fail("No members found");
            }
            return ViewResponse<IEnumerable<MemberViewModel>>.Success(members);
        }

        public ViewResponse<MemberViewModel> CreateMember(CreateMemberViewModel createMemberViewModel)
        {
            var member = mapper.Map<CreateMemberViewModel, Member>(createMemberViewModel);

            member = unitOfWork.MemberRepository.Create(member);

            if (unitOfWork.SaveChanges() > 0)
            {
                return ViewResponse<MemberViewModel>.Success(mapper.Map<MemberViewModel>(member), "Member created successfully");
            }

            return ViewResponse<MemberViewModel>.Fail("Failed to create member");
        }

        public ViewResponse<MemberViewModel> GetById(int id)
        {
            var member = unitOfWork.MemberRepository.GetById(id);

            if (member != null)
            {
                return ViewResponse<MemberViewModel>.Success(mapper.Map<MemberViewModel>(member),
                    "Member Found");

            }
            return ViewResponse<MemberViewModel>.Fail("Member Not Found");
        }

        public ViewResponse<MemberViewModel> GetByEmail(string email)
        {
            var member = unitOfWork.MemberRepository.GetByEmail(email);

            if (member != null)
            {
                return ViewResponse<MemberViewModel>.Success(mapper.Map<MemberViewModel>(member),
                    "Member Found");

            }
            return ViewResponse<MemberViewModel>.Fail("Member Not Found");
        }

        public ViewResponse<MemberViewModel> GetByPhone(string phone)
        {
            var member = unitOfWork.MemberRepository.GetByPhone(phone);

            if (member != null)
            {
                return ViewResponse<MemberViewModel>.Success(mapper.Map<MemberViewModel>(member),
                    "Member Found");

            }
            return ViewResponse<MemberViewModel>.Fail("Member Not Found");
        }

        public ViewResponse<MemberViewModel> UpdateMember(int id, UpdateMemberViewModel updateMemberViewModel)
        {
            var member = unitOfWork.MemberRepository.GetById(id);
            if (member != null)
            {
                member = mapper.Map(updateMemberViewModel, member);

                member = unitOfWork.MemberRepository.Update(member);
                if (unitOfWork.SaveChanges() > 0)
                {
                    return ViewResponse<MemberViewModel>.Success(mapper.Map<MemberViewModel>(member),
                             "Member updated successfully");
                }

            }
            return ViewResponse<MemberViewModel>.Fail("Failed to update this mamber");
        }

        public ViewResponse<MemberViewModel> DeleteById(int id)
        {
            var member = unitOfWork.MemberRepository.GetById(id);
            if (member != null)
            {
                member = unitOfWork.MemberRepository.Delete(member);
                if (unitOfWork.SaveChanges() > 0)
                {
                    return ViewResponse<MemberViewModel>.Success(mapper.Map<MemberViewModel>(member),
                             "Member deleted successfully");
                }

            }
            return ViewResponse<MemberViewModel>.Fail("Failed to delete this mamber");
        }

        public ViewResponse<UpdateMemberViewModel> GetMemberByIdForUpdate(int id)
        {
            var member = unitOfWork.MemberRepository.GetById(id);
            if (member != null)
            {
                return ViewResponse<UpdateMemberViewModel>.Success(
                    mapper.Map<UpdateMemberViewModel>(member),
                    "Member Found");
            }
            return ViewResponse<UpdateMemberViewModel>.Fail("Member Not Found");
        }

        public ViewResponse<HealthRecordViewModel> GetHealthRecordByMemberId(int id)
        {
            var healthRecord = unitOfWork.MemberRepository.GetHealthRecordByMemberId(id);

            if (healthRecord != null)
            {
                if (healthRecord != null)
                {
                    return ViewResponse<HealthRecordViewModel>.Success(
                        mapper.Map<HealthRecordViewModel>(healthRecord),
                        "Health Record Found");
                }
            }
            return ViewResponse<HealthRecordViewModel>.Fail("Health Record Not Found");

        }
    }
}
