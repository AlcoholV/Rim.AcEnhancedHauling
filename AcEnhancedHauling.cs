using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace AlcoholV
{
    public static class AcEnhancedHauling
    {
        public const float NoneHaulDistance = 16f;

        public static Job SmartBill(Pawn p, Job job)
        {
            if (p.story.WorkTagIsDisabled(WorkTags.Hauling))
            {
                return job;
            }
            var carryCapacity = p.GetStatValue(StatDefOf.CarryingCapacity);
            for (var i = 0; i < job.targetQueueB.Count; i++)
            {
                var thing = job.targetQueueB[i].Thing;
                var storagePriority = HaulAIUtility.StoragePriorityAtFor(thing.Position, thing);
                var itemCount = job.countQueue[i];
                IntVec3 foundPos;
                if (StoreUtility.TryFindBestBetterStoreCellFor(thing, p, thing.Map,storagePriority, p.Faction, out foundPos) && carryCapacity >= itemCount)
                {
                    var thingPos = thing.Position;
                    var targetPos = job.targetA.Thing.Position;
                    if ((targetPos - thingPos).LengthHorizontalSquared > (targetPos - foundPos).LengthHorizontalSquared)
                    {
                        return HaulAIUtility.HaulMaxNumToCellJob(p, thing, foundPos, false);
                    }
                }
            }
            return job;
        }

        public static Job SmartBuild(Pawn p, Job job)
        {
            if (p.story.WorkTagIsDisabled(WorkTags.Hauling))
            {
                return job;
            }
            var carryCapacity = p.GetStatValue(StatDefOf.CarryingCapacity);
            var itemCount = job.count;
            var thing = job.targetA.Thing;
            var storagePriority = HaulAIUtility.StoragePriorityAtFor(thing.Position, thing);


            var foundPos = new IntVec3();
            if (StoreUtility.TryFindBestBetterStoreCellFor(thing, p, thing.Map, storagePriority, p.Faction, out foundPos) && carryCapacity >= itemCount)
            {

                var thingPos = thing.Position;
                var targetPos = job.targetB.Thing.Position;
                if ((targetPos - thingPos).LengthHorizontalSquared > (targetPos - foundPos).LengthHorizontalSquared)
                {
                    return HaulAIUtility.HaulMaxNumToCellJob(p, thing, foundPos, false);
                }
                return HaulAIUtility.HaulMaxNumToCellJob(p, thing, foundPos, false);
            }

            return job;
        }
    }
}
