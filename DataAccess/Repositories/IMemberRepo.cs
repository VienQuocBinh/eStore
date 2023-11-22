using BusinessObject;

namespace DataAccess.Repositories
{
    public interface IMemberRepo
    {
        public List<Member> GetMembers();
        public void AddNewMember(Member member);
        public void UpdateMemberInfo(Member member);
        public void DeleteMember(Member member);
        public int GetLatestMemberId();
        public bool SaveChanges();
        public Member GetMemberByEmail(string email);
        public Member Login(string username, string password);
    }
}
