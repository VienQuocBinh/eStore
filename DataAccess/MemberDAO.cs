using BusinessObject;

namespace DataAccess;

public class MemberDAO : BaseDAO
{
    private static MemberDAO Instance = null;
    private MemberDAO() { }
    public static MemberDAO getInstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = new MemberDAO();
            }
            return Instance;
        }
    }

    public Member Login(string username, string password)
    {
        return context.Members.Where(m => m.Email.Equals(username) && m.Password.Equals(password)).FirstOrDefault();
    }
    public List<Member> GetMembers()
    {
        List<Member> members = context.Members.ToList();
        return members;
    }

    public void AddNewMember(Member member)
    {
        context.Members.Add(member);
    }

    public void UpdateMemberInfo(Member member)
    {
        Int32 memberId = member.MemberId;
        var memberInfo = context.Members.FirstOrDefault(x => x.MemberId == memberId);
        if (memberInfo != null)
        {
            memberInfo.MemberId = memberId;
            memberInfo.Email = member.Email;
            memberInfo.CompanyName = member.CompanyName;
            memberInfo.City = member.City;
            memberInfo.Country = member.Country;
            memberInfo.Password = member.Password;
        }
        context.Members.Update(memberInfo);
    }

    public void DeleteMember(Member member)
    {
        context.Members.Remove(member);
    }

    public int GetLatestMemberId()
    {
        var result = (from member in context.Members
                      orderby member.MemberId descending
                      select member.MemberId).Take(1);
        return result.ToList().FirstOrDefault();
    }

    public Member GetMemberbyEmail(string email)
    {
        var member = context.Members.FirstOrDefault(x => x.Email == email);
        return member;
    }
}
