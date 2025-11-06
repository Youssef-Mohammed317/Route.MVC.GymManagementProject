using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.DAL.Entites;
using GymManagement.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private readonly IAttachmentService attachmentService;

        public MemberService(IUnitOfWork _unitOfWork, IMapper _mapper, IAttachmentService _attachmentService)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            attachmentService = _attachmentService;
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

            var responsefile = attachmentService.Upload("MembersPhotos", createModel.PhotoFile);

            if (!responsefile.IsSuccess)
            {
                return ViewResponse<MemberViewModel>.Fail("Failed to upload photo");
            }

            memberModel.Photo = responsefile?.FileName!;
            memberModel.PhotoUrl = responsefile?.FileUrl!;

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
        public ViewResponse<MemberDetailsViewModel> GetMemberDetailsById(int id)
        {
            var memberModel = unitOfWork.MemberRepository.GetById(id);

            if (memberModel != null)
            {
                var memberDetails = new MemberDetailsViewModel
                {
                    Id = memberModel.Id,
                    PhotoName = memberModel.Photo,
                    PhotoUrl = memberModel.PhotoUrl,
                    Name = memberModel.Name,
                    Email = memberModel.Email,
                    Phone = memberModel.Phone,
                    Gender = memberModel.Gender.ToString(),
                    DateOfBirth = memberModel.DateOfBirth.ToString("yyyy-MM-dd"),
                    PlanName = memberModel.MemberShips?.FirstOrDefault()?.Plan?.Name! ?? "No Plan Yet",
                    MembershipStartDate = memberModel.MemberShips?.FirstOrDefault()?.Plan?.Created_at.ToString("yyyy-MM-dd") ?? "No Membership Yet",
                    MembershipEndDate = memberModel.MemberShips?.FirstOrDefault()?.EndDate.ToString("yyyy-MM-dd") ?? "No Membership Yet",
                    Address = $"{memberModel.Adderss.BuildingNumber} - {memberModel.Adderss.Street} - {memberModel.Adderss.City}"
                };

                return ViewResponse<MemberDetailsViewModel>.Success(memberDetails,
                    "Member Found");

            }
            return ViewResponse<MemberDetailsViewModel>.Fail("Member Not Found");
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

            var photoname = memberModel.Photo;
            var photourl = memberModel.PhotoUrl;

            memberModel = mapper.Map(updateModel, memberModel);

            memberModel.PhotoUrl = photourl;
            memberModel.Photo = photoname;

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
                var updateMemberViewModel = mapper.Map<UpdateMemberViewModel>(memberModel);
                updateMemberViewModel.PhotoUrl = memberModel.PhotoUrl;
                updateMemberViewModel.PhotoName = memberModel.Photo;
                return ViewResponse<UpdateMemberViewModel>.Success(
                    updateMemberViewModel
                    ,
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
