using static MathHelper.MathHelper;
namespace HomeWork6.GroupsCalculator
{
    public class LightWeightGroupsCounter : IGroupsCounter
    {
        public int CountGroups(int n)
        {
            var elderByte = CalculateNearestBinaryNum(n);
            
            return CalculateNumOfElderByte(elderByte - 1);
        }
    }
}