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
                IntVec3 storePos;
                if (StoreUtility.TryFindBestBetterStoreCellFor(thing, p, thing.Map,storagePriority, p.Faction, out storePos) && carryCapacity >= itemCount)
                {
                    var targetPos = thing.Position;
                    var destPos = job.targetA.Thing.Position;
                    if ((targetPos - destPos).LengthHorizontalSquared > (targetPos - storePos).LengthHorizontalSquared)
                    {
                        return HaulAIUtility.HaulMaxNumToCellJob(p, thing, storePos, false);
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


            IntVec3 storePos;
            if (StoreUtility.TryFindBestBetterStoreCellFor(thing, p, thing.Map, storagePriority, p.Faction, out storePos) && carryCapacity >= itemCount)
            {

                var targetPos = thing.Position;
                var destPos = job.targetB.Thing.Position;
                if ((targetPos - destPos).LengthHorizontalSquared > (targetPos - storePos).LengthHorizontalSquared)
                {
                    return HaulAIUtility.HaulMaxNumToCellJob(p, thing, storePos, false);
                }
            }

            return job;
        }
    }
}
