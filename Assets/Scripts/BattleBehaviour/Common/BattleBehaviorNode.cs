using System.Collections.Generic;

public class BattleBehaviorNode
{
    protected BattleBehaviorNodeState State;
    public BattleBehaviorNode Parent;
    protected readonly List<BattleBehaviorNode> Children = new();
    private readonly Dictionary<string, object> _dataContext = new();

    public BattleBehaviorNode()
    {
        Parent = null;
    }

    public BattleBehaviorNode(List<BattleBehaviorNode> children)
    {
        foreach (var child in children)
        {
            AttachNode(child);
        }
    }

    public virtual BattleBehaviorNodeState Evaluate() => BattleBehaviorNodeState.Failure;

    public void SetData(string key, object value)
    {
        _dataContext[key] = value;
        Parent?.SetData(key,value);
    }

    protected object GetData(string key)
    {
        object result = null;
        if (_dataContext.TryGetValue(key, out result))
        {
            return result;
        }

        var node = Parent;
        while (node != null)
        {
            result = node.GetData(key);
            if (result != null)
            {
                return result;
            }
            node = node.Parent;
        }
        return null;
    }

    public bool ClearData(string key)
    {
        if (_dataContext.ContainsKey(key))
        {
            _dataContext.Remove(key);
            return true;
        }

        var node = Parent;
        while (node != null)
        {
            var cleared = node.ClearData(key);
            if (cleared)
            {
                return true;
            }
            node = node.Parent;
        }
        return false;
    }

    private void AttachNode(BattleBehaviorNode child)
    {
        child.Parent = this;
        Children.Add(child);
    }
    
    

    public enum BattleBehaviorNodeState
    {
        Running,
        Success,
        Failure
    }
}