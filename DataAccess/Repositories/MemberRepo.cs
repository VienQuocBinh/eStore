using BusinessObject;

namespace DataAccess.Repositories
{
    public class MemberRepo : IMemberRepo
    {
        public MemberRepo() { }

        public MemberRepo(string ConnectionString)
        {
            MemberDAO.ConnectionString = ConnectionString;
        }

        public void AddNewMember(Member member)
        {
            MemberDAO.getInstance.AddNewMember(member);
        }

        public void DeleteMember(Member member)
        {
            MemberDAO.getInstance.DeleteMember(member);
        }

        public List<Member> GetMembers()
        {
            return MemberDAO.getInstance.GetMembers();
        }

        public void UpdateMemberInfo(Member member)
        {
            MemberDAO.getInstance.UpdateMemberInfo(member);
        }

        public int GetLatestMemberId()
        {
            return MemberDAO.getInstance.GetLatestMemberId();
        }

        public bool SaveChanges() => MemberDAO.getInstance.SaveChanges();

        public Member GetMemberByEmail(string email)
        {
            return MemberDAO.getInstance.GetMemberbyEmail(email);
        }

        public Member Login(string username, string password)
        {
            return MemberDAO.getInstance.Login(username, password);
        }
    }
}
