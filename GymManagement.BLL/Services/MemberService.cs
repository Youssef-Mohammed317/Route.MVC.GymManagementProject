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

        public ViewResponse<MemberViewModel> CreateMember(CreateMemberViewModel createModel)
        {
            var memberModel = mapper.Map<CreateMemberViewModel, Member>(createModel);

            memberModel = unitOfWork.MemberRepository.Create(memberModel);

            if (unitOfWork.SaveChanges() > 0)
            {
                return ViewResponse<MemberViewModel>.Success(mapper.Map<MemberViewModel>(memberModel), "Member created successfully");
            }

            return ViewResponse<MemberViewModel>.Fail("Failed to create member");
        }

        public ViewResponse<MemberViewModel> GetMemberById(int id)
        {
            var memberModel = unitOfWork.MemberRepository.GetById(id);

            if (memberModel != null)
            {
                return ViewResponse<MemberViewModel>.Success(mapper.Map<MemberViewModel>(memberModel),
                    "Member Found");

            }
            return ViewResponse<MemberViewModel>.Fail("Member Not Found");
        }

        public ViewResponse<MemberViewModel> GetMemberByEmail(string email)
        {
            var memberModel = unitOfWork.MemberRepository.GetByEmail(email);

            if (memberModel != null)
            {
                return ViewResponse<MemberViewModel>.Success(mapper.Map<MemberViewModel>(memberModel),
                    "Member Found");

            }
            return ViewResponse<MemberViewModel>.Fail("Member Not Found");
        }

        public ViewResponse<MemberViewModel> GetMemberByPhone(string phone)
        {
            var memberModel = unitOfWork.MemberRepository.GetByPhone(phone);

            if (memberModel != null)
            {
                return ViewResponse<MemberViewModel>.Success(mapper.Map<MemberViewModel>(memberModel),
                    "Member Found");

            }
            return ViewResponse<MemberViewModel>.Fail("Member Not Found");
        }

        public ViewResponse<MemberViewModel> UpdateMember(int id, UpdateMemberViewModel updateModel)
        {
            var memberModel = unitOfWork.MemberRepository.GetById(id);


            if (memberModel == null)
            {
                return ViewResponse<MemberViewModel>.Fail("Member not found");
            }
            memberModel = mapper.Map(updateModel, memberModel);

            memberModel = unitOfWork.MemberRepository.Update(memberModel);
            if (unitOfWork.SaveChanges() > 0)
            {
                return ViewResponse<MemberViewModel>.Success(mapper.Map<MemberViewModel>(memberModel),
                         "Member updated successfully");
            }

            return ViewResponse<MemberViewModel>.Fail("Failed to update this mamber");
        }

        public ViewResponse<MemberViewModel> DeleteById(int id)
        {
            var memberModel = unitOfWork.MemberRepository.GetById(id);
            if (memberModel != null)
            {
                memberModel = unitOfWork.MemberRepository.Delete(memberModel);
                if (unitOfWork.SaveChanges() > 0)
                {
                    return ViewResponse<MemberViewModel>.Success(mapper.Map<MemberViewModel>(memberModel),
                             "Member deleted successfully");
                }

            }
            return ViewResponse<MemberViewModel>.Fail("Failed to delete this mamber");
        }

        public ViewResponse<UpdateMemberViewModel> GetMemberByIdForUpdate(int id)
        {
            var memberModel = unitOfWork.MemberRepository.GetById(id);
            if (memberModel != null)
            {
                return ViewResponse<UpdateMemberViewModel>.Success(
                    mapper.Map<UpdateMemberViewModel>(memberModel),
                    "Member Found");
            }
            return ViewResponse<UpdateMemberViewModel>.Fail("Member Not Found");
        }

        public ViewResponse<HealthRecordViewModel> GetHealthRecordByMemberId(int id)
        {
            var healthRecordModel = unitOfWork.MemberRepository.GetHealthRecordByMemberId(id);

            if (healthRecordModel != null)
            {
                if (healthRecordModel != null)
                {
                    return ViewResponse<HealthRecordViewModel>.Success(
                        mapper.Map<HealthRecordViewModel>(healthRecordModel),
                        "Health Record Found");
                }
            }
            return ViewResponse<HealthRecordViewModel>.Fail("Health Record Not Found");

        }
    }
}
