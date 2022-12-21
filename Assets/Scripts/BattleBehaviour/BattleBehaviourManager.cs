namespace DefaultNamespace.BattleBehaviour
{
    public static class BattleBehaviourManager
    {
        public static void UpdateBattleBehaviour(UnitController unitController)
        {
            unitController.gameObject.AddComponent<PanicBehavior>();
        }
    }
}