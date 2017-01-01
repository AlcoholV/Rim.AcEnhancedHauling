using RimWorld;
using Verse;
using Verse.AI;

namespace AlcoholV
{
    public class WorkGiver_DoBill : RimWorld.WorkGiver_DoBill
    {
        public override Job JobOnThing(Pawn pawn, Thing thing)
        {
            var job = base.JobOnThing(pawn, thing);
            if (job == null)
            {
                return null;
            }
            if ((job.def != JobDefOf.DoBill) || job.targetQueueB[0].Thing.def.isUnfinishedThing)
            {
                return job;
            }
            return AcEnhancedHauling.SmartBill(pawn, job);
        }
    }

    public class WorkGiver_ConstructDeliverResourcesToFrames : RimWorld.WorkGiver_ConstructDeliverResourcesToFrames
    {
        public override Job JobOnThing(Pawn pawn, Thing t)
        {
            var job = base.JobOnThing(pawn, t);
            if (job == null)
            {
                return null;
            }
            if (job.def != JobDefOf.HaulToContainer)
            {
                return job;
            }
            return AcEnhancedHauling.SmartBuild(pawn, job);
        }
    }

    internal class WorkGiver_ConstructDeliverResourcesToBlueprints : RimWorld.WorkGiver_ConstructDeliverResourcesToBlueprints
    {
        public override Job JobOnThing(Pawn pawn, Thing t)
        {
            var job = base.JobOnThing(pawn, t);
            if (job == null)
            {
                return null;
            }

            if ((job.def != JobDefOf.HaulToContainer) || !(t is Blueprint))
            {
                return job;
            }
            return AcEnhancedHauling.SmartBuild(pawn, job);
        }
    }
}